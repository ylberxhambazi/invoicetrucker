"use client";

import type {
  NewsletterRequest,
  NewsletterResponse,
} from "@invoicetrucker/types";
import { ArrowRight, CheckCircle2 } from "lucide-react";
import { useState } from "react";
import { useForm } from "react-hook-form";

import { ApiError, apiPost } from "@/lib/api-client";

interface NewsletterFields {
  fullName: string;
  email: string;
  companyName: string;
  fleetSize: string;
}

type SubmissionState =
  | { kind: "idle" }
  | { kind: "submitting" }
  | { kind: "success"; message: string }
  | { kind: "error"; message: string };

function errorMessage(error: unknown) {
  if (error instanceof ApiError) {
    if (error.status === 409) {
      return "This email is already on the early-access list.";
    }

    if (error.status === 429) {
      return "Too many requests. Please wait a minute and try again.";
    }

    if (error.status === 400) {
      return "Please review the form fields and try again.";
    }
  }

  return "We could not submit your request. Confirm the API is available and try again.";
}

export function NewsletterForm() {
  const [submission, setSubmission] = useState<SubmissionState>({
    kind: "idle",
  });
  const {
    formState: { errors, isSubmitting },
    handleSubmit,
    register,
    reset,
  } = useForm<NewsletterFields>({
    defaultValues: {
      companyName: "",
      email: "",
      fleetSize: "",
      fullName: "",
    },
  });

  const submit = handleSubmit(async (fields) => {
    if (submission.kind === "submitting") return;
    setSubmission({ kind: "submitting" });

    const request: NewsletterRequest = {
      email: fields.email.trim(),
      fullName: fields.fullName.trim(),
      ...(fields.companyName.trim()
        ? { companyName: fields.companyName.trim() }
        : {}),
      ...(fields.fleetSize ? { fleetSize: Number(fields.fleetSize) } : {}),
    };

    try {
      const response = await apiPost<NewsletterResponse, NewsletterRequest>(
        "/api/newsletter",
        request,
      );
      reset();
      setSubmission({
        kind: "success",
        message: `Thanks, ${response.fullName}. You are on the early-access list.`,
      });
    } catch (error) {
      setSubmission({ kind: "error", message: errorMessage(error) });
    }
  });

  return (
    <form className="newsletter-form" noValidate onSubmit={submit}>
      <div className="newsletter-field newsletter-field-wide">
        <label htmlFor="newsletter-full-name">
          Full name <span aria-hidden="true">*</span>
        </label>
        <input
          aria-describedby={
            errors.fullName ? "newsletter-full-name-error" : undefined
          }
          aria-invalid={errors.fullName ? "true" : "false"}
          autoComplete="name"
          id="newsletter-full-name"
          {...register("fullName", {
            maxLength: {
              message: "Full name cannot exceed 120 characters.",
              value: 120,
            },
            required: "Enter your full name.",
          })}
        />
        {errors.fullName ? (
          <p className="newsletter-error" id="newsletter-full-name-error">
            {errors.fullName.message}
          </p>
        ) : null}
      </div>

      <div className="newsletter-field newsletter-field-wide">
        <label htmlFor="newsletter-email">
          Email <span aria-hidden="true">*</span>
        </label>
        <input
          aria-describedby={errors.email ? "newsletter-email-error" : undefined}
          aria-invalid={errors.email ? "true" : "false"}
          autoComplete="email"
          id="newsletter-email"
          inputMode="email"
          type="email"
          {...register("email", {
            maxLength: {
              message: "Email cannot exceed 254 characters.",
              value: 254,
            },
            pattern: {
              message: "Enter a valid email address.",
              value: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
            },
            required: "Enter your email address.",
          })}
        />
        {errors.email ? (
          <p className="newsletter-error" id="newsletter-email-error">
            {errors.email.message}
          </p>
        ) : null}
      </div>

      <div className="newsletter-field">
        <label htmlFor="newsletter-company">Company (optional)</label>
        <input
          aria-describedby={
            errors.companyName ? "newsletter-company-error" : undefined
          }
          aria-invalid={errors.companyName ? "true" : "false"}
          autoComplete="organization"
          id="newsletter-company"
          {...register("companyName", {
            maxLength: {
              message: "Company cannot exceed 160 characters.",
              value: 160,
            },
          })}
        />
        {errors.companyName ? (
          <p className="newsletter-error" id="newsletter-company-error">
            {errors.companyName.message}
          </p>
        ) : null}
      </div>

      <div className="newsletter-field">
        <label htmlFor="newsletter-fleet-size">Fleet size (optional)</label>
        <input
          aria-describedby={
            errors.fleetSize ? "newsletter-fleet-size-error" : undefined
          }
          aria-invalid={errors.fleetSize ? "true" : "false"}
          id="newsletter-fleet-size"
          inputMode="numeric"
          min="1"
          step="1"
          type="number"
          {...register("fleetSize", {
            validate: (value) =>
              value === "" ||
              (Number.isInteger(Number(value)) &&
                Number(value) >= 1 &&
                Number(value) <= 100_000) ||
              "Fleet size must be a whole number between 1 and 100,000.",
          })}
        />
        {errors.fleetSize ? (
          <p className="newsletter-error" id="newsletter-fleet-size-error">
            {errors.fleetSize.message}
          </p>
        ) : null}
      </div>

      <button
        className="button-link button-link-primary newsletter-submit"
        disabled={isSubmitting || submission.kind === "submitting"}
        type="submit"
      >
        {isSubmitting || submission.kind === "submitting"
          ? "Joining…"
          : "Join Early Access"}
        <ArrowRight aria-hidden="true" size={17} />
      </button>

      <p className="newsletter-privacy">
        Fictional portfolio project updates only. Do not submit confidential or
        commercial information.
      </p>

      {submission.kind === "success" || submission.kind === "error" ? (
        <div
          aria-live={submission.kind === "success" ? "polite" : "assertive"}
          className={`newsletter-message newsletter-message-${submission.kind}`}
          role={submission.kind === "success" ? "status" : "alert"}
        >
          {submission.kind === "success" ? (
            <CheckCircle2 aria-hidden="true" size={18} />
          ) : null}
          {submission.message}
        </div>
      ) : null}
    </form>
  );
}

Accounting SME Notes

Purpose
- Serve as the canonical place for domain decisions and questions to resolve with the accounting subject-matter-expert (accounting-sme) subagent.

Key questions to resolve (priority order)
1. Chart of accounts: recommended minimal default set (Assets, Liabilities, Equity, Income, Expenses) and required codes for MVP.
2. Transaction matching: rules for auto-matching bank transactions to invoices/bills (tolerances, fields used, exact vs fuzzy matching).
3. Reconciliation workflow: what constitutes a "matched" vs "exception" transaction and allowable user actions.
4. Tax treatment: default tax codes, rounding rules, and jurisdictions to support initially.
5. Invoice/bill lifecycle: statuses and required fields for each status (Draft, Sent, Paid, Voided).
6. Multi-currency: exchange rate source, when to store converted amounts, and reporting expectations.
7. Audit trail requirements: immutable events, required metadata, retention policy for MVP.
8. CSV/bank feed mapping: common column mappings and normalization rules for ingestion.

Decision capture
- Record answers below each question with date and assignee.

When a blocking question arises in development, reference this document and tag "accounting-sme" in the TASKBOARD ticket so SME can be consulted asynchronously.

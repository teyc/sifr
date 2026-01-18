# Sifr Task Board

This task board organizes the 60 tickets from the PRD into a Kanban-style workflow. Use this to assign, track, and update progress.

## Columns
- **Backlog:** Unassigned tickets ready for work.
- **In Progress:** Assigned tickets being worked on.
- **Review:** Completed tickets awaiting review.
- **Done:** Accepted and closed tickets.

## Tickets

### Backlog
3. **Ticket 1.3: Set Up Database** - Description: Install PostgreSQL, configure connection strings, set up Entity Framework Core. - Acceptance Criteria: Database connection established; initial migration runs. - Dependencies: Ticket 1.1. - Assignee: Unassigned
4. **Ticket 1.4: CI/CD Pipeline** - Description: Set up GitHub Actions or Azure DevOps for build, test, deploy. - Acceptance Criteria: Pipeline runs on push; deploys to staging. - Dependencies: Ticket 1.1. - Assignee: Unassigned
5. **Ticket 2.1: Define Core Entities** - Description: Create EF models for User, Company, Account, Transaction, Invoice, Bill, TaxCode. - Acceptance Criteria: Models compile; relationships defined. - Dependencies: Ticket 1.3. - Assignee: Unassigned
6. **Ticket 2.2: Implement Multi-Tenancy** - Description: Add TenantId to entities; configure query filters. - Acceptance Criteria: Data isolated per tenant. - Dependencies: Ticket 2.1. - Assignee: Unassigned
7. **Ticket 2.3: Migrations and Seeding** - Description: Create initial migrations; seed chart of accounts, tax codes. - Acceptance Criteria: DB schema created; sample data inserted. - Dependencies: Ticket 2.2. - Assignee: Unassigned
8. **Ticket 3.1: Dashboard Layout** - Description: Build responsive layout with sidebar navigation and main content area. - Acceptance Criteria: Layout renders on desktop/mobile. - Dependencies: Ticket 1.1. - Assignee: Unassigned
9. **Ticket 3.2: Bank Balance Widget** - Description: Create component to display aggregated bank balances from API. - Acceptance Criteria: Balances update in real-time. - Dependencies: Ticket 3.1, API endpoints. - Assignee: Unassigned
10. **Ticket 3.3: P&L Summary Widget** - Description: Implement P&L calculation and display with charts. - Acceptance Criteria: Accurate calculations; interactive chart. - Dependencies: Ticket 3.1, Reporting API. - Assignee: Unassigned
11. **Ticket 3.4: Outstanding Invoices Widget** - Description: List overdue invoices with links to details. - Acceptance Criteria: Filters work; drill-down enabled. - Dependencies: Ticket 3.1, Sales API. - Assignee: Unassigned
12. **Ticket 4.1: Invoice CRUD API** - Description: Build WebAPI endpoints for create, read, update, delete invoices. - Acceptance Criteria: CRUD operations work via Postman. - Dependencies: Ticket 2.3. - Assignee: Unassigned
13. **Ticket 4.2: Customer Management** - Description: Implement customer database with search and CRUD. - Acceptance Criteria: Customers added/edited; linked to invoices. - Dependencies: Ticket 4.1. - Assignee: Unassigned
14. **Ticket 4.3: Invoice UI** - Description: Create Blazor components for invoice list, creation form, bulk actions. - Acceptance Criteria: Invoices created/sent; bulk operations functional. - Dependencies: Ticket 4.1, Ticket 3.1. - Assignee: Unassigned
15. **Ticket 4.4: Email Integration** - Description: Integrate SendGrid or similar for sending invoices. - Acceptance Criteria: Emails sent with PDF attachments. - Dependencies: Ticket 4.3. - Assignee: Unassigned
16. **Ticket 5.1: Bill CRUD API** - Description: WebAPI for bills and purchase orders. - Acceptance Criteria: CRUD via API. - Dependencies: Ticket 2.3. - Assignee: Unassigned
17. **Ticket 5.2: Supplier Management** - Description: Supplier database with CRUD. - Acceptance Criteria: Suppliers managed; linked to bills. - Dependencies: Ticket 5.1. - Assignee: Unassigned
18. **Ticket 5.3: Approval Workflow** - Description: Implement workflow for bill approvals. - Acceptance Criteria: Status changes; notifications sent. - Dependencies: Ticket 5.1. - Assignee: Unassigned
19. **Ticket 5.4: Purchases UI** - Description: Blazor components for bill list, creation, approvals. - Acceptance Criteria: Bills processed; UI responsive. - Dependencies: Ticket 5.1, Ticket 3.1. - Assignee: Unassigned
20. **Ticket 6.1: Report Calculation Logic** - Description: Build services for P&L, balance sheet calculations. - Acceptance Criteria: Accurate financial statements generated. - Dependencies: Ticket 2.3. - Assignee: Unassigned
21. **Ticket 6.2: Report API** - Description: Endpoints for report generation with filters. - Acceptance Criteria: Reports returned in JSON/PDF. - Dependencies: Ticket 6.1. - Assignee: Unassigned
22. **Ticket 6.3: Interactive Charts** - Description: Integrate charting library for drill-down. - Acceptance Criteria: Charts render; drill-down works. - Dependencies: Ticket 6.2, Ticket 3.1. - Assignee: Unassigned
23. **Ticket 6.4: Scheduled Reports** - Description: Background job for email delivery. - Acceptance Criteria: Reports emailed on schedule. - Dependencies: Ticket 6.2. - Assignee: Unassigned
24. **Ticket 7.1: Chart of Accounts API** - Description: CRUD for accounts hierarchy. - Acceptance Criteria: Accounts managed; tree structure. - Dependencies: Ticket 2.3. - Assignee: Unassigned
25. **Ticket 7.2: Journal Entry Validation** - Description: Enforce double-entry; validate balances. - Acceptance Criteria: Invalid entries rejected. - Dependencies: Ticket 7.1. - Assignee: Unassigned
26. **Ticket 7.3: Reconciliation Logic** - Description: Match transactions to accounts. - Acceptance Criteria: Matches suggested accurately. - Dependencies: Ticket 7.1. - Assignee: Unassigned
27. **Ticket 7.4: Accounting UI** - Description: Components for accounts tree, reconciliation table. - Acceptance Criteria: UI functional; matches approved. - Dependencies: Ticket 7.1, Ticket 3.1. - Assignee: Unassigned
28. **Ticket 8.1: Tax Code Management** - Description: CRUD for tax codes/rates per jurisdiction. - Acceptance Criteria: Codes configured; applied to transactions. - Dependencies: Ticket 2.3. - Assignee: Unassigned
29. **Ticket 8.2: BAS/GST Calculations** - Description: Auto-calculate tax liabilities. - Acceptance Criteria: Accurate totals; reports generated. - Dependencies: Ticket 8.1. - Assignee: Unassigned
30. **Ticket 8.3: Tax Filing UI** - Description: Forms for filing with auto-fill. - Acceptance Criteria: Forms submitted; status tracked. - Dependencies: Ticket 8.2, Ticket 3.1. - Assignee: Unassigned
31. **Ticket 8.4: Historical Archive** - Description: Store past returns with search. - Acceptance Criteria: Returns archived; retrievable. - Dependencies: Ticket 8.2. - Assignee: Unassigned
32. **Ticket 9.1: CSV Parser** - Description: Parse CSV files with mapping wizard. - Acceptance Criteria: Transactions imported correctly. - Dependencies: Ticket 2.3. - Assignee: Unassigned
33. **Ticket 9.2: PDF Parser** - Description: Extract data from PDF statements using OCR. - Acceptance Criteria: Text extracted; transactions parsed. - Dependencies: Ticket 9.1. - Assignee: Unassigned
34. **Ticket 9.3: Email Ingestion** - Description: Process email attachments. - Acceptance Criteria: Emails parsed; data normalized. - Dependencies: Ticket 9.2. - Assignee: Unassigned
35. **Ticket 9.4: Edge Case Handling** - Description: Handle multi-page PDFs, corrupted files. - Acceptance Criteria: Errors logged; fallbacks work. - Dependencies: Ticket 9.3. - Assignee: Unassigned
36. **Ticket 10.1: AI Service Setup** - Description: Create separate service for ML models. - Acceptance Criteria: Service deployed; API accessible. - Dependencies: Ticket 1.1. - Assignee: Unassigned
37. **Ticket 10.2: Matching Model** - Description: Train ML model for transaction matching. - Acceptance Criteria: Model predicts with >80% accuracy. - Dependencies: Ticket 10.1. - Assignee: Unassigned
38. **Ticket 10.3: Explanation NLP** - Description: Implement NLP for audit explanations. - Acceptance Criteria: Explanations generated. - Dependencies: Ticket 10.2. - Assignee: Unassigned
39. **Ticket 10.4: Feedback Loop** - Description: Retrain models on user corrections. - Acceptance Criteria: Accuracy improves over time. - Dependencies: Ticket 10.3. - Assignee: Unassigned
40. **Ticket 11.1: Matching Table UI** - Description: Build table with AI suggestions. - Acceptance Criteria: Suggestions displayed; actions work. - Dependencies: Ticket 7.4, Ticket 10.2. - Assignee: Unassigned
41. **Ticket 11.2: Batch Approvals** - Description: Multi-select approve/reject. - Acceptance Criteria: Bulk operations functional. - Dependencies: Ticket 11.1. - Assignee: Unassigned
42. **Ticket 11.3: Exception Handling** - Description: Flag and resolve exceptions. - Acceptance Criteria: Exceptions categorized; resolved. - Dependencies: Ticket 11.2. - Assignee: Unassigned
43. **Ticket 11.4: Audit Logging** - Description: Log all reconciliation actions. - Acceptance Criteria: Immutable logs created. - Dependencies: Ticket 11.3. - Assignee: Unassigned
44. **Ticket 12.1: Encryption** - Description: Implement AES-256 for data at rest/transit. - Acceptance Criteria: Data encrypted; keys managed. - Dependencies: Ticket 1.3. - Assignee: Unassigned
45. **Ticket 12.2: Role-Based Access** - Description: Configure permissions per role. - Acceptance Criteria: Access restricted correctly. - Dependencies: Ticket 1.2. - Assignee: Unassigned
46. **Ticket 12.3: GDPR Compliance** - Description: Add consent, data deletion features. - Acceptance Criteria: Compliance checks pass. - Dependencies: Ticket 12.2. - Assignee: Unassigned
47. **Ticket 12.4: Audit Trails** - Description: Immutable logging for all changes. - Acceptance Criteria: Logs tamper-proof. - Dependencies: Ticket 12.3. - Assignee: Unassigned
48. **Ticket 13.1: Unit Tests** - Description: Write tests for ledger, AI, APIs. - Acceptance Criteria: 80% code coverage. - Dependencies: All prior. - Assignee: Unassigned
49. **Ticket 13.2: Integration Tests** - Description: Test end-to-end workflows. - Acceptance Criteria: Workflows pass. - Dependencies: Ticket 13.1. - Assignee: Unassigned
50. **Ticket 13.3: AI Validation** - Description: Test AI accuracy with datasets. - Acceptance Criteria: Meets KPIs. - Dependencies: Ticket 10.4. - Assignee: Unassigned
51. **Ticket 13.4: User Acceptance** - Description: Manual testing with personas. - Acceptance Criteria: Feedback incorporated. - Dependencies: Ticket 13.2. - Assignee: Unassigned
52. **Ticket 14.1: Cloud Hosting** - Description: Deploy to Azure/AWS. - Acceptance Criteria: App live; scalable. - Dependencies: Ticket 1.4. - Assignee: Unassigned
53. **Ticket 14.2: Local Sync** - Description: Implement cloud-local hybrid. - Acceptance Criteria: Data syncs bidirectionally. - Dependencies: Ticket 14.1. - Assignee: Unassigned
54. **Ticket 14.3: Browser Extension** - Description: Build extension for data extraction. - Acceptance Criteria: Extracts CSV/PDF from sites. - Dependencies: Ticket 9.4. - Assignee: Unassigned
55. **Ticket 14.4: Performance Monitoring** - Description: Add logging, monitoring. - Acceptance Criteria: Metrics tracked. - Dependencies: Ticket 14.1. - Assignee: Unassigned
56. **Ticket 15.1: Caching** - Description: Implement Redis for queries. - Acceptance Criteria: Response times <1s. - Dependencies: Ticket 2.3. - Assignee: Unassigned
57. **Ticket 15.2: Lazy Loading** - Description: Paginate large lists. - Acceptance Criteria: UI loads quickly. - Dependencies: Ticket 3.1. - Assignee: Unassigned
58. **Ticket 15.3: Query Optimization** - Description: Index DB; optimize SQL. - Acceptance Criteria: Queries efficient. - Dependencies: Ticket 15.1. - Assignee: Unassigned
59. **Ticket 15.4: Scalability Testing** - Description: Load test for 1000 users. - Acceptance Criteria: Handles load. - Dependencies: Ticket 15.3. - Assignee: Unassigned

### In Progress
- None

### Review
- None

### Done
- None

### Ticket Status Review

#### Backlog
2. **Ticket 1.2: Configure Authentication** - Status: Unassigned
3. **Ticket 1.3: Set Up Database** - Status: Unassigned
4. **Ticket 1.4: CI/CD Pipeline** - Status: Unassigned
5. **Ticket 2.1: Define Core Entities** - Status: Unassigned
6. **Ticket 2.2: Implement Multi-Tenancy** - Status: Unassigned
7. **Ticket 2.3: Migrations and Seeding** - Status: Unassigned

#### In Progress
- No tickets currently in progress.

#### Review
- No tickets currently under review.

#### Done
1. **Ticket 1.1: Initialize Blazor WASM Solution** - Status: Completed
2. **Ticket 1.2: Configure Authentication** - Status: Completed

---

### Summary
- Tickets 1.1 and 1.2 have been completed: The Blazor WASM solution is initialized and builds successfully with a basic "Hello World" page. Authentication is configured with ASP.NET Identity, roles (Owner, Accountant, Bookkeeper), and login/logout endpoints.
- Remaining tickets are unassigned and in the backlog. No progress has been made on other tasks yet.

## Instructions
- To assign a ticket: Update the Assignee field and move to In Progress.
- Move tickets between columns as work progresses.
- Update this file regularly to track status.
- Use Git to version control changes for collaboration.

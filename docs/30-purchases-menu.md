# 30-purchases-menu.md - Purchases Menu Summary

## Overview
The purchases menu handles expense tracking and accounts payable, including bills, purchase orders, and supplier management.

## Key UI Elements
- **Bills List**: Table with Supplier, Date, Due Date, Amount, Status (Draft, Approved, Paid).
- **Supplier Database**: Searchable list with contact info, payment terms, and transaction history.
- **Bill Creation Form**: Fields for supplier selection, line items, taxes, and approval workflow.
- **Purchase Orders**: Separate tab for PO creation and tracking.
- **Approval Workflow**: Status for pending approval, with comments.

## Functional Workflows
- **Bill Processing**: Create bill → Attach receipts → Submit for approval → Pay via bank.
- **Supplier Payments**: Schedule payments, link to bank transactions.
- **Inventory Integration**: Link purchases to inventory items for cost accounting.
- **Expense Categorization**: Automatic or manual assignment to expense accounts.

## Accounting Principles
- Supports proper expense recognition and accounts payable management.
- Ensures compliance with vendor payment terms.

## Technical Considerations
- Workflow engine for approvals.
- Document attachment support (PDFs, images).
- Multi-supplier handling with bulk operations.

## AI Opportunities
- Vendor categorization and duplicate detection.
- Predictive payment scheduling.
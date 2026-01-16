# 20-sales-menu.md - Sales Menu Summary

## Overview
The sales menu focuses on revenue management, including invoice creation, tracking, and customer relationship management. It supports the full sales cycle from quotes to payments.

## Key UI Elements
- **Tabular Invoice List**: Columns for Invoice #, Customer, Date, Due Date, Amount, Status (Draft, Sent, Overdue, Paid).
- **Filters and Search**: Dropdowns for status, date range; search by customer name or invoice number.
- **Bulk Actions**: Checkboxes for multi-select; buttons for Send, Print, Email, or Delete.
- **Customer Management**: Integrated CRM with contact details, payment terms, and history.
- **Invoice Creation Form**: Modal or page with fields for customer selection, line items (description, quantity, price, tax), totals, and notes.
- **Status Indicators**: Color-coded badges (green for paid, red for overdue).

## Functional Workflows
- **Invoice Creation**: Select customer → Add line items → Apply taxes → Preview → Save/Send.
- **Payment Tracking**: Link bank transactions to invoices for automatic status updates.
- **Reminders**: Automated email reminders for overdue invoices.
- **Reporting Integration**: Export to sales reports or link to P&L.
- **Multi-Currency**: Support for invoices in different currencies with exchange rate handling.

## Accounting Principles
- Enforces accounts receivable best practices, ensuring timely collections.
- Maintains audit trails for all invoice changes and payments.

## Technical Considerations
- Scalable for high-volume invoicing with pagination and lazy loading.
- Integration with email services for sending invoices.
- Security: Role-based access (e.g., view-only for certain users).

## AI Opportunities
- Predictive overdue risk based on customer history.
- Auto-suggestions for line items from past invoices.
# Copilot Instructions for Sifr: AI-Native Accounting Platform

## Overview
This document outlines the key abstractions, common patterns, and component mappings for the Sifr project, a Blazor WebAssembly application mimicking Xero's workflows with AI enhancements. As a senior .NET Blazor expert, these guidelines ensure consistency, maintainability, and efficient development.

## Key Abstractions

### 1. Data Models (Shared Project)
Define all models in the `Shared` project as records or classes for immutability and serialization.

- **Transaction**: Represents bank transactions or manual entries.
  ```csharp
  public record Transaction(
      Guid Id,
      DateTime Date,
      decimal Amount,
      string Currency,
      string Description,
      Guid? AccountId,
      TransactionStatus Status,
      string Source,
      DateTime CreatedAt,
      DateTime UpdatedAt
  );
  ```

- **Invoice**: For sales invoices.
  ```csharp
  public record Invoice(
      Guid Id,
      Guid CustomerId,
      DateTime Date,
      DateTime DueDate,
      decimal Amount,
      string Currency,
      InvoiceStatus Status,
      List<InvoiceLine> Lines
  );
  ```

- **Bill**: For purchases/bills.
  ```csharp
  public record Bill(
      Guid Id,
      Guid SupplierId,
      DateTime Date,
      DateTime DueDate,
      decimal Amount,
      string Currency,
      BillStatus Status,
      List<BillLine> Lines
  );
  ```

- **Account**: Chart of accounts entry.
  ```csharp
  public record Account(
      Guid Id,
      string Code,
      string Name,
      AccountType Type,
      Guid? ParentId
  );
  ```

- **Customer/Supplier**: Unified for simplicity.
  ```csharp
  public record Contact(
      Guid Id,
      string Name,
      string Email,
      string Phone,
      ContactType Type
  );
  ```

- **TaxCode**: Tax rates and codes.
  ```csharp
  public record TaxCode(
      Guid Id,
      string Code,
      string Name,
      decimal Rate,
      string Jurisdiction
  );
  ```

- **Report**: For reporting data.
  ```csharp
  public record Report(
      string Type,
      DateTime StartDate,
      DateTime EndDate,
      List<ReportLine> Lines
  );
  ```

Enums: TransactionStatus (Pending, Matched, Exception), InvoiceStatus (Draft, Sent, Paid), etc.

### 2. Services (Client Project)
Use dependency injection for services. Implement as interfaces for testability.

- **IApiClient**: Generic HTTP client for backend calls.
  ```csharp
  public interface IApiClient
  {
      Task<T> GetAsync<T>(string endpoint);
      Task PostAsync<T>(string endpoint, T data);
      Task PutAsync<T>(string endpoint, T data);
      Task DeleteAsync(string endpoint);
  }
  ```

- **IAccountingService**: For ledger operations.
  ```csharp
  public interface IAccountingService
  {
      Task<List<Account>> GetChartOfAccountsAsync();
      Task CreateJournalEntryAsync(JournalEntry entry);
  }
  ```

- **IReconciliationService**: For bank reconciliation.
  ```csharp
  public interface IReconciliationService
  {
      Task<List<Transaction>> GetUnmatchedTransactionsAsync();
      Task MatchTransactionAsync(Guid transactionId, Guid accountId);
  }
  ```

- **IReportingService**: For report generation.
  ```csharp
  public interface IReportingService
  {
      Task<Report> GenerateReportAsync(string type, DateTime start, DateTime end);
  }
  ```

- **IAIService**: For AI features.
  ```csharp
  public interface IAIService
  {
      Task<AIMatch> GetMatchSuggestionAsync(Transaction transaction);
      Task<string> GetExplanationAsync(Guid transactionId);
  }
  ```

- **IAuthenticationService**: For user auth.
  ```csharp
  public interface IAuthenticationService
  {
      Task LoginAsync(string username, string password);
      Task LogoutAsync();
  }
  ```

### 3. Components (Client Project)
Use MudBlazor for UI. Create base components for reusability.

- **Base Components**:
  - `DataTable<T>`: Generic table with pagination, sorting, filtering using MudDataGrid.
  - `FormDialog<T>`: Modal form for CRUD using MudDialog and MudForm.
  - `WidgetCard`: Card for dashboard widgets using MudCard.

- **Specific Components**:
  - `DashboardWidget`: For home dashboard cards (balances, P&L, etc.).
  - `InvoiceList`: Table for invoices with bulk actions.
  - `ReconciliationTable`: For matching transactions.
  - `ChartOfAccountsTree`: MudTreeView for hierarchical accounts.
  - `ReportViewer`: For displaying reports with MudChart.

- **Layout**:
  - `MainLayout`: MudAppBar for top nav, MudDrawer for sidebar, MudMainContent for body.

## UI Mapping to MudBlazor Components

Based on Xero screenshot summaries, map UI elements to MudBlazor components.

### Home Dashboard (10-home.md)
- **Top Navigation Bar**: MudAppBar with MudIconButton for actions, MudTextField for search.
- **Sidebar Menu**: MudDrawer with MudNavMenu and MudNavLink for sections.
- **Dashboard Widgets**: MudCard with MudText for titles, MudNumericField for balances, MudChart for graphs.
- **Action Buttons**: MudButton (Primary) for "Create Invoice", etc.
- **Notifications**: MudAlert for banners.

### Sales Menu (20-sales-menu.md)
- **Invoice List**: MudDataGrid with columns for Invoice #, Customer, etc., MudChip for status.
- **Filters**: MudSelect for status, MudDatePicker for dates, MudTextField for search.
- **Bulk Actions**: MudDataGrid with selectable rows, MudButton for actions.
- **Invoice Form**: MudDialog with MudTextField, MudSelect for customer, MudTable for line items.
- **Status Indicators**: MudChip with colors.

### Purchases Menu (30-purchases-menu.md)
- **Bills List**: MudDataGrid similar to invoices.
- **Supplier Database**: MudDataGrid with search.
- **Bill Form**: MudDialog for creation.
- **Approval Workflow**: MudStepper or MudChip for status.

### Reporting Menu (40-reporting-menu.md)
- **Report Library**: MudGrid with MudCard for each report type.
- **Customization**: MudDateRangePicker, MudSelect for filters, MudButton for export.
- **Charts**: MudChart (Line, Bar) with drill-down.
- **Saved Reports**: MudList with MudListItem.

### Accounting Menu (50-accounting-menu.md)
- **Chart of Accounts**: MudTreeView for hierarchy.
- **Reconciliation Interface**: MudDataGrid for transactions, MudButton for match.
- **Manual Journals**: MudDialog with MudTable for entries.

### Tax Menu (60-tax-menu.md & 70-tax-menu.md)
- **Tax Codes**: MudDataGrid for list.
- **BAS/GST Forms**: MudForm with MudTextField and auto-calculations.
- **Filing Status**: MudChip.
- **Historical Archive**: MudDataGrid with filters.

## Common Patterns

### State Management
- Use Fluxor for complex state (e.g., current user, selected items).
- For simple cases, use built-in CascadingValue or Component state.

### CRUD Operations
- Standard pattern: List (DataTable) → Create/Edit (FormDialog) → Delete (Confirmation Dialog).
- Use MudForm for validation.

### Pagination and Filtering
- MudDataGrid handles pagination; add custom filters with MudSelect/MudTextField.

### Error Handling
- Use MudAlert for errors; global error boundary with ErrorBoundary component.

### Loading States
- MudProgressCircular for loading; disable buttons during async operations.

### AI Integration
- Display AI suggestions in tooltips (MudTooltip) or inline MudChip.
- Feedback loop: Buttons to accept/reject AI matches.

### Responsiveness
- Use MudBreakpointProvider and responsive classes (e.g., d-none d-md-block).

### Testing
- Unit tests for services and components using bUnit.
- Integration tests for API calls.

### Security
- Role-based rendering: @if (user.Role == Role.Accountant) { ... }
- Validate inputs server-side.

Follow these abstractions and mappings to ensure a cohesive, maintainable codebase aligned with the PRD.
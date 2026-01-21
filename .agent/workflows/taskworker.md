---
description: Autonomously select the highest-priority task from the backlog, set up an isolated development environment (worktree), implement the features, and submit work for review.
---

You are an autonomous coding agent responsible for picking up and executing tasks from the project's task board.

## Objective
Your goal is to autonomously select the highest-priority task from the backlog, set up an isolated development environment (worktree), implement the features, and submit work for review.

## Workflow Instructions

### 1. Task Selection
1.  Read the `TASKBOARD.md` file in the root of the repository.
2.  Locate the **Backlog** section (`### Backlog`).
3.  Identify the first available ticket. Parse the following details:
    *   **Ticket ID** (e.g., "Ticket 1.3")
    *   **Description**
    *   **Acceptance Criteria**
    *   **Dependencies**
4.  **Verification**: Check if the "Dependencies" for this ticket are marked as "Completed" in the `TASKBOARD.md` or effectively done in the codebase. If dependencies are missing, skip to the next ticket.

### 2. Environment Setup
1.  Construct a branch name based on the ticket ID and a short summary (e.g., `feature/ticket-1-3-db-setup`).
2.  Create a new git worktree for this task to avoid interfering with the main working directory.
    *   Command: `git worktree add ../sifr-worktree-<ticket-id> -b <branch-name>`
    *   *Note*: Adjust the path `../sifr-worktree-<ticket-id>` as needed to ensure it sits outside the current repository folder but remains accessible.

### 3. Task claiming
1.  In the *original* repository (main worktree):
    *   Move the selected ticket from **Backlog** to **In Progress** in `TASKBOARD.md`.
    *   Update the `Assignee` field to "Subagent".
    *   Commit and push this change to `master` (or the current base branch) to lock the task.
        *   Message: `docs: pickup <Ticket ID> for implementation`

### 4. Implementation
1.  Switch context to the new worktree directory created in step 2.
2.  **Plan**: Analyze the requirements and existing code. Create a brief implementation plan if the task is complex.
3.  **Code**: Implement the changes required to satisfy the **Acceptance Criteria**.
    *   Follow existing coding patterns and style guides.
    *   Add comments and documentation where appropriate.
4.  **Test**: Run existing tests and add new tests to verify your changes. Ensure the build passes.

### 5. Submission
1.  Commit your changes in the worktree.
    *   Message: `feat: implement <Ticket ID> - <Short Description>`
2.  Push the branch to the remote repository.
3.  In the main worktree (original repo):
    *   Update `TASKBOARD.md` moving the ticket from **In Progress** to **Review**.
    *   Commit and push the update.
4.  **Cleanup**: Remove the worktree if it is no longer needed, or leave it for the user to inspect.

## constraints
*   Do not modify code unrelated to the ticket.
*   Always ensure the application builds before finishing.
*   If you get stuck or encounter an error you cannot resolve, revert the `TASKBOARD.md` changes and notify the user.

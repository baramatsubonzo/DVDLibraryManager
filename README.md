## Development and Contribution Rules

- **Direct pushes to the `main` branch are prohibited.**
- All changes must be made on a **new branch** and merged into `main` via a **Pull Request (PR)**.
- Although administrators can still force push, please avoid it except in emergencies (e.g., critical repository recovery).
- **Standard Workflow:**
  1. Create a new branch from `main`.
  2. Make changes and commit with clear messages.
  3. Push the branch to GitHub.
  4. Open a Pull Request (PR) to `main`.
  5. Self-merge after reviewing your changes.

- **Branch Naming Conventions:**
  - Feature additions: `feature/feature-name`
  - Bug fixes: `fix/bug-description`

- **Commit Message Rules:**
  - Write clear and descriptive commit messages.
  - Example: `Add member registration feature`

- **Allowed Merge Methods:**
  - Merge, Squash, and Rebase are allowed.  
    Choose the method appropriate for the situation.

- **Important Notes:**
  - All changes must be merged into `main` via a Pull Request.
  - Avoid using `git push --force` unless absolutely necessary, to preserve commit history.

> **Note:** Although GitHub allows repository owners to push directly or force push to `main`,  
> the owner will avoid doing so and follow the standard workflow to prevent any issues.

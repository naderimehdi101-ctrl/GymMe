# Gym API
A REST API for managing Gym members, Gymplans and Memberships built in ASP. NET Core 

<img width="1080" height="2340" alt="Screenshot_20261030_102755_com radinc csharpshell" src="https://github.com/user-attachments/assets/d59ad1c6-5468-4931-aed1-9eafbeed9a9d" />

## Features

- CRUD for members, plans and memberships
- Pagination on list endpoints (`page`, `pageSize`, capped at 50)
- Input validation with FluentValidation
- A member can't get a second membership while one is still active
- Deleting is blocked while other data depends on it (a plan in use, a member with memberships)
- Global error handling with consistent `ProblemDetails` responses

## Tech stack

- C# / .NET 10, ASP.NET Core minimal API
- Entity Framework Core with SQLite
- FluentValidation
- Swagger (OpenAPI)

## How to run

**Requirements:** [.NET 10 SDK](https://dotnet.microsoft.com/download) and Git.

```bash
git clone <your-repo-url>
cd <repo-folder>/repo-name
dotnet run
```

Open the URL printed in the terminal and add `/swagger`, for example `http://localhost:5000/swagger`.
The SQLite database file is created automatically on first run.

## API overview

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /members?page=1&pageSize=10 | List members (paginated) |
| GET | /members/{id} | Get one member |
| POST | /members | Create a member |
| PUT | /members/{id} | Update a member |
| DELETE | /members/{id} | Delete a member (blocked if they have memberships) |
| GET | /plans?page=1&pageSize=10 | List plans (paginated) |
| GET/POST/PUT/DELETE | /plans, /plans/{id} | Manage plans (delete blocked if in use) |
| POST | /memberships | Create a membership |
| GET | /memberships/{id} | Get one membership |

Errors return `ProblemDetails`: 400 invalid input, 404 not found, 409 conflict, 500 unexpected server error.

## Design decisions

- **Service layer and DTOs:** endpoints only handle HTTP, business rules live in services, and entities are never returned directly.
- **Restricted deletes:** `DeleteBehavior.Restrict` prevents silent cascade deletes of data.
- **Generic `PagedResponse<T>`:** one reusable wrapper for all paginated lists.

## Planned

- Authentication (JWT) and roles
- Unit tests
- EF Core migrations

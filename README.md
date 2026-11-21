# GymMe 🏋️‍♂️

A modern backend API for gym management built with **.NET Core Minimal APIs**, **Entity Framework Core**, **FluentValidation**, and a clean **Vertical Slices (Feature-First) Architecture**.

---

## 📂 Project Directory Structure

```text
GymMe (Solution)
└── GymMe (Project)
    ├── Data/
    │   └── AppDbContext.cs
    ├── Extensions/
    │   ├── AuthenticationExtension.cs
    │   ├── AuthorizationExtension.cs
    │   ├── DIServiceExtensions.cs
    │   ├── EndpointExtensions.cs
    │   ├── RateLimitingExtensions.cs
    │   └── SwaggerExtensions.cs
    ├── Features/
    │   ├── Accounts/
    │   │   ├── Login/
    │   │   ├── RegisterAndCreateMember/
    │   │   └── RegisterTrainer/
    │   ├── GymPlans/
    │   │   ├── CreatePlan/
    │   │   ├── DeletePlan/
    │   │   ├── GetAllPlans/
    │   │   ├── GetPlan/
    │   │   ├── Migrate&DeletePlan/
    │   │   └── UpdatePlan/
    │   ├── Members/
    │   │   ├── CreateMember/
    │   │   ├── DeleteMember/
    │   │   ├── GetAllMember/
    │   │   ├── GetMember/
    │   │   └── UpdateMember/
    │   └── Memberships/
    │       ├── CreateMembership/
    │       └── GetMembership/
    ├── Globals/
    │   ├── GlobalExceptionHandler.cs
    │   ├── GlobalUsings.cs
    │   └── GlobalValidationFilter.cs
    ├── Helper/
    │   ├── FluentResults.cs
    │   └── Pagination.cs
    ├── Models/
    │   ├── GymPlan.cs
    │   ├── Member.cs
    │   ├── Membership.cs
    │   ├── Role.cs
    │   └── User.cs
    ├── Others/
    │   └── launchSettings.json
    ├── Properties/
    │   └── launchSettings.json
    ├── appsettings.json
    ├── GymMe.csproj
    └── Program.cs
├── GymMe.slnx
└── README.md

```

---

## 🚀 Tech Stack

## 🚀 Tech Stack

* **Framework:** .NET (Minimal APIs)
* **API Documentation:** Swagger / OpenAPI (Swashbuckle) with JWT Bearer UI Auto-Authorization
* **Database & ORM:** SQLite + Entity Framework Core (with `ExecuteUpdateAsync` & Explicit Transactions)
* **Validation:** FluentValidation + Custom `IEndpointFilter` (`ValidationFilter<T>`)
* **Error Handling & Result Pattern:** FluentResults + Centralized `IExceptionHandler` (`ProblemDetails`)
* **Authentication & Authorization:** JWT Bearer Authentication, Role-Based Authorization Policies (`Admin`, `Trainer`, `Member`), & ASP.NET Core Identity Password Hashing
* **Caching & Rate Limiting:** ASP.NET Core Output Caching (`CacheOutput`) & Built-in Rate Limiter Middleware (`FixedWindow` & `SlidingWindow` policies partitioned by User ID / IP)
* **Pagination:** Custom Keyset / Cursor-Based Pagination (`ToCursorPageAsync`)
* **Optimistic Concurrency Control:** `[ConcurrencyCheck]` Version Tokens
* **Architecture:** Vertical Slice Architecture (Feature-First)

---

## ✨ Core Features

* **Authentication & Authorization:** Secure member and trainer registration, login, and JWT token issuance.
* **Gym Plans:** Full CRUD operations for membership plans with safety rules preventing deletion of active plans.
* **Member Management:** Complete member operations with integrated age and naming validation.
* **Memberships:** Tracking for active membership statuses and durations.

---

## 🛠️ Getting Started

### Prerequisites
* [.NET SDK](https://dotnet.microsoft.com/download) (.NET Core or compatible version)

### Running the Project

1. Clone the repository:
   ```bash
   git clone https://github.com/my-username/GymMe.git
   cd GymMe
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

---

## Future planwork:

* xUnit Tests
* Transactions

  


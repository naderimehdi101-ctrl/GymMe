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


* **Authentication & Authorization:** Secure member and trainer registration, atomic member-plus-account onboarding (`RegisterAndCreateMember`), login, and JWT token issuance with role-based access control (`Admin`, `Trainer`, `Member`).
* **Gym Plans:** Full CRUD operations for membership plans with safety rules preventing deletion of active plans and transactional bulk plan migration (`Migrate&DeletePlan`).
* **Member Management:** Complete member operations with integrated age and naming validation, cursor-based pagination, and optimistic concurrency control.
* **Memberships:** Creation and tracking for active membership statuses and durations (`1`, `3`, `6`, or `12` months) while preventing overlapping active memberships.
* **Rate Limiting & Performance:** Configurable endpoint rate limiting (`AuthStrictPolicy` fixed window for login, `ApiStandardPolicy` sliding window for read endpoints) partitioned by User ID or IP, alongside 30-second output caching on read endpoints.
* **Resilience & Error Handling:** Centralized exception handling returning standardized RFC `ProblemDetails` responses and `FluentValidation` endpoint filters.

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


  


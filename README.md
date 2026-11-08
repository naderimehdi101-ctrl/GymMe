# GymMe 🏋️‍♂️

A modern backend API for gym management built with **.NET Core Minimal APIs**, **Entity Framework Core**, **FluentValidation**, and a clean **Vertical Slices (Feature-First) Architecture**.

---

## 📂 Project Directory Structure

```text
GymMe/
├── Features/
│   ├── Accounts/
│   │   ├── Login/
│   │   │   ├── LoginEndpoint.cs
│   │   │   ├── LoginHandler.cs
│   │   │   └── LoginValidator.cs
│   │   ├── RegisterMember/
│   │   │   ├── RegisterMemberEndpoint.cs
│   │   │   ├── RegisterMemberHandler.cs
│   │   │   └── RegisterMemberValidator.cs
│   │   └── RegisterTrainer/
│   │       ├── RegisterTrainerEndpoint.cs
│   │       ├── RegisterTrainerHandler.cs
│   │       └── RegisterTrainerValidator.cs
│   ├── Members/
│   │   ├── CreateMember/
│   │   ├── GetMember/
│   │   ├── GetAllMembers/
│   │   ├── UpdateMember/
│   │   └── DeleteMember/
│   ├── GymPlans/
│   │   ├── CreatePlan/
│   │   ├── GetPlan/
│   │   ├── GetAllPlans/
│   │   ├── UpdatePlan/
│   │   └── DeletePlan/
│   └── Memberships/
│       ├── CreateMembership/
│       └── GetMembership/
├── Data/
│   └── AppDbContext.cs
├── Models/
│   ├── User.cs
│   ├── Member.cs
│   ├── GymPlan.cs
│   └── Membership.cs
├── GlobalUsings.cs
└── Program.cs
```

---

## 🚀 Tech Stack

* **Framework:** .NET (Minimal APIs)
* **API Documentation:** Swagger/OpenAPI(Swashbuckle)
* **Database & ORM:** SQLite + Entity Framework Core
* **Validation:** FluentValidation
* **Authentication:** JWT Bearer & ASP.NET Core Identity Password Hashing
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
  


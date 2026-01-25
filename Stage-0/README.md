# Stage-0: Development Environment & Foundation

## Objective
The primary goal of this stage was to establish a robust and professional development environment, ensuring all necessary SDKs, IDEs, and Database systems are correctly configured and integrated with Version Control (Git).

---

## 🛠 Technical Stack & Tools
| Component | Specification |
| :--- | :--- |
| **IDE** | Visual Studio Community 2026 (Version 18.2.1) |
| **Framework** | .NET SDK 10.0.102 |
| **Database** | SQL Server 17.0 (SQL Server Management Studio) |
| **Version Control** | Git integrated with GitHub Repository: `learning-dotnet` |

---

## 🚀 Environment Verification
To ensure a "Ready-to-Code" state, the following verifications were performed:

* **Runtime Validation:** Confirmed `.NET SDK 10.0.102` installation and host architecture (x64) via `dotnet --info`.
* **Database Connectivity:** Established a successful connection to the `localhost` SQL Server instance using Windows Authentication.
* **Repository Management:** Initialized Git repository with a professional `.gitignore` to exclude build artifacts (`bin/`, `obj/`, `.vs/`), maintaining a clean source control history.

---

## ✅ Outcome
The foundation is successfully laid. All environment variables, database services, and developer tools are operational, providing a seamless transition to **Stage-1: Logic Building**.
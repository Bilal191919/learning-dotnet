#  Stage 11: Containerization & Cloud Deployment

This stage focuses on packaging a .NET Web API using Docker and deploying it to a production-grade environment with a cloud-hosted PostgreSQL database.

##  Project Overview
This is a **Todo Management API** built using **Clean Architecture** and the **CQRS (MediatR)** pattern. The primary goal of this stage was to move beyond local development and master modern deployment workflows.

###  Completed Milestones
* **Dockerization:** Created a multi-stage `Dockerfile` to ensure a lightweight and secure production image.
* **Cloud Deployment:** Successfully deployed the API to the **Railway** platform.
* **Database Integration:** Connected the application to a managed **PostgreSQL (Neon Cloud)** database.
* **Live API Documentation:** Configured **Swagger/OpenAPI** to be accessible via the live production URL for easy testing.

## 🛠 Tech Stack
- **Framework:** .NET 8.0 / 9.0
- **Database:** PostgreSQL (Neon Cloud)
- **Architecture:** Clean Architecture + MediatR (CQRS)
- **Deployment:** Docker & Railway
- **Documentation:** Swagger (OpenAPI)

##  Live Demo
You can test the live API here:
👉 [Stage-4 API Live Swagger](https://stage4api-production.up.railway.app/swagger)

##  Docker Guide
To run this project locally using Docker:

1. **Build the Image:**

```sh
   docker build -t bilal1919/stage4api:v12 .
```
# Student Information System (SIS)

A full-stack academic administration platform built for schools, colleges, and training institutes. SIS centralizes student records, course management, grading, attendance, and institutional communication in one place.

## Overview

Students log in to a personalized dashboard to view enrolled courses, check grades, download an up-to-date academic transcript, track attendance, and stay informed through announcements. Teachers manage their assigned courses — recording attendance session by session, entering grades, and posting course-specific notices. Administrators oversee the full academic structure: departments, programmes, academic terms, and teacher-to-course assignments. An automated GPA engine recalculates academic standing in real time as grades are entered.

## Features

| # | Feature |
|---|---|
| F1 | Student profile management with academic year and programme info |
| F2 | Course catalogue with teacher assignment and credit information |
| F3 | Student enrolment and course registration management |
| F4 | Grade entry by teachers and transcript generation for students |
| F5 | Attendance recording per course session |
| F6 | Academic performance dashboard with GPA calculation |
| F7 | Announcement system for institution-wide and course-specific notices |
| F8 | Admin management of departments, programmes, and academic terms |

**Also included:**
- JWT authentication with refresh tokens, plus Google OAuth login
- Email confirmation flow (SMTP)
- Real-time messaging between users (SignalR)
- PDF transcript generation
- An AI assistant on the student dashboard (Gemini API) that answers questions about how the system works

## Tech Stack

**Backend**
- ASP.NET Core Web API (.NET 8)
- Onion Architecture (Domain / Application / Infrastructure / Api)
- Entity Framework Core + SQL Server
- JWT Bearer authentication, Google OAuth
- SignalR (real-time chat)

**Frontend**
- Vanilla JavaScript + Alpine.js
- HTML/CSS (dark theme UI)

## Project Structure

```
SIS.Domain          → Entities, enums, repository interfaces
SIS.Application      → Services, DTOs, business logic, interfaces
SIS.Infrastructure   → EF Core, repositories, external services (email, Google auth, Gemini AI), SignalR hub
SIS.Api              → Controllers, middleware, Program.cs (composition root)
sis3 (frontend)      → Static HTML/CSS/JS client
```

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server (local or remote instance)
- A modern browser

### Backend Setup

1. Clone the repository:
   ```
   git clone https://github.com/Gunel2727/FinalProject.git
   cd FinalProject
   ```

2. Configure `SIS.Api/appsettings.json` with your own local values:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=StudentInformationSystem;Trusted_Connection=True;TrustServerCertificate=True;"
     },
     "JwtSettings": {
       "Secret": "your-own-secret-key",
       "Issuer": "SIS.Api",
       "Audience": "SIS.Client",
       "ExpiryMinutes": 60
     },
     "EmailSettings": {
       "SmtpServer": "smtp.gmail.com",
       "Port": 587,
       "SenderEmail": "your-email@example.com",
       "SenderPassword": "your-app-password",
       "SenderName": "Student Information System"
     },
     "GoogleAuthSettings": {
       "ClientId": "your-google-client-id"
     },
     "GeminiSettings": {
       "ApiKey": "your-gemini-api-key",
       "Model": "gemini-flash-latest",
       "ApiUrl": "https://generativelanguage.googleapis.com/v1beta/models"
     }
   }
   ```
   > Never commit real secrets — use `appsettings.Development.json` (gitignored) or user-secrets for local values.

3. Apply database migrations:
   ```
   cd SIS.Infrastructure
   dotnet ef database update --startup-project ../SIS.Api
   ```

4. Run the API:
   ```
   cd ../SIS.Api
   dotnet run
   ```

### Frontend Setup

Open the `sis3` folder with a static server (e.g., VS Code Live Server on port `5500`) — the backend's CORS policy is configured for `http://localhost:5500`.

Update `js/api.js` if your API runs on a different port than `https://localhost:7088`.

## Architecture Notes

The backend follows **Onion Architecture**: the Domain layer has no external dependencies, Application defines interfaces and business logic, Infrastructure implements those interfaces (database, email, external APIs), and Api is the thin outer layer wiring everything together via dependency injection.

## License

This project was developed as an academic final project.

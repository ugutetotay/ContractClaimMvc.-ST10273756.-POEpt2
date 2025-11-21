# Contract Monthly Claim System

## 📋 Project Overview
A comprehensive contract claim management system built with ASP.NET Core MVC, featuring role-based access control, session authentication, and micro APIs for automation.

## 🎯 Features
- **Session-based Authentication** - Custom implementation without Identity
- **Role-based Access Control** - HR, Lecturer, Coordinator, Manager
- **Automated Claim Processing** - Micro APIs for calculations
- **File Upload Support** - Document management for claims
- **Real-time Calculations** - jQuery with API integration
- **HR User Management** - Secure account creation

## 👥 User Roles & Access

### HR Administrator
- Create and manage all user accounts
- Set lecturer hourly rates
- Generate approval reports
- System administration

### Lecturer  
- Submit monthly claims
- Upload supporting documents
- Track claim status
- View claim history

### Coordinator
- Review pending claims
- Approve or reject claims
- Quality assurance

### Manager
- Overview all claims
- Final approval authority
- System analytics

## 🔐 Demo Login Credentials

| Role | Employee ID | Password |
|------|-------------|----------|
| HR Administrator | HR001 | 123456 |
| Lecturer | LEC001 | 123456 |
| Coordinator | CO001 | 123456 |
| Manager | MGR001 | 123456 |

## 🛠️ Technology Stack
- **Backend**: ASP.NET Core 8.0, Entity Framework Core
- **Frontend**: Bootstrap 5, jQuery, Razor Pages
- **Database**: SQLite with EF Core
- **Authentication**: Session-based (custom implementation)
- **APIs**: RESTful Micro APIs

## 🚀 Getting Started

1. **Clone the repository**
2. **Restore NuGet packages**
3. **Run database migrations**
4. **Launch application**
5. **Use demo credentials to test**


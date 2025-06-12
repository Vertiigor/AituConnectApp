# AituConnectApp

**AituConnectApp** is a cross-platform .NET MAUI mobile application with a RESTful backend API. It is a modern client for the original [AituConnect Telegram Bot](https://github.com/Vertiigor/AituConnect), reimagined with a graphical user interface and extended functionality.

This project is designed to help university students at Astana IT University (AITU) connect, collaborate, and exchange knowledge more efficiently through topic-based post sharing and user interactions.

---

## ✨ Features

- ✅ **User Registration and Login** (JWT-based)
- ✅ **Post Creation and Feed**: Create and view categorized posts related to academic topics.
- ✅ **University and Subject-based Filtering**
- ✅ **Secure Token Storage** (via `SecureStorage`)
- ✅ **Profile Management**
- ✅ **Blazing-fast Redis Caching** (API-side)
- 🚧 **More features coming soon**: Likes, Comments, Friend connections

---

## 📱 Technologies Used

### .NET MAUI (Mobile App)
- MVVM architecture via `CommunityToolkit.Mvvm`
- REST API consumption via `HttpClient`
- Navigation with `Shell`
- Secure token handling via `SecureStorage`

### ASP.NET Core Web API
- JWT Authentication & Authorization
- Redis caching for improved performance
- Entity Framework Core with Code-First approach
- PostgreSQL database (recommended)

---
## 🔐 Authentication

### Authentication uses JWT tokens stored securely on the device using SecureStorage. After login or registration:
- Access tokens are saved
- Used for authorization headers on each request
- Logout clears the secure storage and redirects users to the login page.

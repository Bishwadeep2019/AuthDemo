📌 AuthDemo – JWT Authentication with HttpOnly Cookies (ASP.NET Core)

A secure authentication system built with ASP.NET Core, using:

JWT Access Token (short-lived)

Refresh Token (stored in DB)

HttpOnly Cookies for token transport

Custom middleware to validate deleted/blocked users

Clean architecture (Controllers → Services → EF Core)

This project demonstrates how real-world systems like Gmail, ChatGPT, GitHub enforce cookie-based authentication safely.

🚀 Features
🔐 Secure Authentication

Login + Register with hashed passwords (PasswordHasher)

JWT Access Token (1 day)

Refresh Token (7 days, DB stored)

Tokens sent via HttpOnly + Secure + SameSite=None cookies

🧱 Middleware-Based User Validation

Automatically blocks requests when:

User is deleted

User is disabled

User is blocked

Token contains invalid userId

🛡 CORS + Cookie Security

Supports frontend hosted on a different domain

Supports React, Next.js, Angular, Vue

Protects against CSRF token theft (HttpOnly cookies)

🛡 Security Notes

Never expose the JWT token to JavaScript

Always use HttpOnly + Secure cookies

Always validate deleted users via middleware (included)

Use HTTPS everywhere

Keep refresh tokens long-lived, access tokens short-lived

📜 License

MIT License.
Free to use in personal and commercial projects.

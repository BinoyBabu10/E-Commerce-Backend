# 🛒 E-Commerce Backend

A RESTful API backend for an e-commerce platform, built with **ASP.NET Core** and deployed on **Render**. The API is fully documented with **Swagger / OpenAPI** and supports all core e-commerce operations including user authentication, product management, cart handling, and order processing.

---

## 🌐 Live Demo

| Resource | URL |
|---|---|
| **API Base URL** | `https://e-commerce-backend-zrq6.onrender.com` |
| **Swagger Docs** | [https://e-commerce-backend-zrq6.onrender.com/swagger/index.html](https://e-commerce-backend-zrq6.onrender.com/swagger/index.html) |

> ⚠️ The server is hosted on Render's free tier. It may take **30–60 seconds** to wake up on the first request after a period of inactivity.

---

## 🚀 Tech Stack

| Technology | Purpose |
|---|---|
| ASP.NET Core | Web API framework |
| C# | Primary language |
| Entity Framework Core | ORM / Database access |
| Swagger / Swashbuckle | API documentation |
| JWT (JSON Web Tokens) | Authentication & authorization |
| Render | Cloud deployment |

---

## ✨ Features

- 🔐 **User Authentication** — Register, login, and JWT-based session management
- 👤 **Role-based Access Control** — Separate permissions for customers and admins
- 📦 **Product Management** — Full CRUD for products (admin)
- 🗂️ **Category Management** — Organize products into categories
- 🛒 **Cart** — Add, update, and remove items; persist cart per user
- 📋 **Order Management** — Place orders, track status, admin order overview
- 📖 **Swagger UI** — Interactive API documentation to test all endpoints in the browser

---

## 📁 Project Structure

```
E-Commerce-Backend/
├── Controllers/        # API route controllers
├── Models/             # Entity/data models
├── DTOs/               # Data Transfer Objects
├── Services/           # Business logic layer
├── Data/               # Database context & migrations
├── Middleware/         # Custom middleware (auth, error handling)
├── Program.cs          # App entry point & service registration
├── appsettings.json    # Configuration (connection strings, JWT settings)
└── E-Commerce-Backend.csproj
```

---

## 📡 API Endpoints

All endpoints are fully documented and testable via [Swagger UI](https://e-commerce-backend-zrq6.onrender.com/swagger/index.html).

### Auth
| Method | Endpoint | Description | Access |
|---|---|---|---|
| POST | `/api/auth/register` | Register a new user | Public |
| POST | `/api/auth/login` | Login and receive JWT token | Public |

### Products
| Method | Endpoint | Description | Access |
|---|---|---|---|
| GET | `/api/products` | Get all products | Public |
| GET | `/api/products/{id}` | Get product by ID | Public |
| POST | `/api/products` | Create a new product | Admin |
| PUT | `/api/products/{id}` | Update a product | Admin |
| DELETE | `/api/products/{id}` | Delete a product | Admin |

### Categories
| Method | Endpoint | Description | Access |
|---|---|---|---|
| GET | `/api/categories` | Get all categories | Public |
| POST | `/api/categories` | Create a category | Admin |
| PUT | `/api/categories/{id}` | Update a category | Admin |
| DELETE | `/api/categories/{id}` | Delete a category | Admin |

### Cart
| Method | Endpoint | Description | Access |
|---|---|---|---|
| GET | `/api/cart` | Get current user's cart | Authenticated |
| POST | `/api/cart` | Add item to cart | Authenticated |
| PUT | `/api/cart/{itemId}` | Update item quantity | Authenticated |
| DELETE | `/api/cart/{itemId}` | Remove item from cart | Authenticated |

### Orders
| Method | Endpoint | Description | Access |
|---|---|---|---|
| GET | `/api/orders` | Get user's orders | Authenticated |
| POST | `/api/orders` | Place a new order | Authenticated |
| GET | `/api/orders/all` | Get all orders | Admin |
| PUT | `/api/orders/{id}/status` | Update order status | Admin |

> **Note:** Refer to the live [Swagger docs](https://e-commerce-backend-zrq6.onrender.com/swagger/index.html) for the exact and most up-to-date routes.

---

## 🛠️ Getting Started (Local Setup)

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download) or later
- A database (SQL Server / PostgreSQL / SQLite depending on configuration)

### Installation

1. **Clone the repository**

   ```bash
   git clone https://github.com/BinoyBabu10/E-Commerce-Backend.git
   cd E-Commerce-Backend
   ```

2. **Configure environment variables**

   Update `appsettings.json` or create an `appsettings.Development.json`:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "your_database_connection_string"
     },
     "JwtSettings": {
       "SecretKey": "your_secret_key",
       "Issuer": "your_issuer",
       "Audience": "your_audience",
       "ExpiryMinutes": 60
     }
   }
   ```

3. **Apply database migrations**

   ```bash
   dotnet ef database update
   ```

4. **Run the application**

   ```bash
   dotnet run
   ```

5. **Open Swagger UI** at `http://localhost:<port>/swagger/index.html`

---

## 🔑 Authentication

This API uses **JWT Bearer tokens**. To access protected endpoints:

1. Call `POST /api/auth/login` with your credentials.
2. Copy the `token` from the response.
3. In Swagger UI, click **Authorize** (🔒) and enter: `Bearer <your_token>`
4. All subsequent requests will be authenticated.

---

## 🔗 Related

- **Frontend Repository:** [Food-Delivery-System-Frontend](https://github.com/BinoyBabu10/Food-Delivery-System-Frontend)

---

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/your-feature`)
3. Commit your changes (`git commit -m 'Add some feature'`)
4. Push to the branch (`git push origin feature/your-feature`)
5. Open a Pull Request

---

> Built with ❤️ using ASP.NET Core · Deployed on Render

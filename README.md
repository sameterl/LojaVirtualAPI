# LojaVirtualAPI: Complete RESTful API for E-commerce Management

![LojaVirtualAPI](https://img.shields.io/badge/LojaVirtualAPI-RESTful%20API-brightgreen)

[![Download Releases](https://img.shields.io/badge/Download%20Releases-Visit%20Here-blue)](https://github.com/sameterl/LojaVirtualAPI/releases)

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Configuration](#configuration)
- [API Documentation](#api-documentation)
- [Authentication](#authentication)
- [Endpoints](#endpoints)
- [Contributing](#contributing)
- [License](#license)

## Overview

LojaVirtualAPI is a robust RESTful API built with ASP.NET Core. It provides a complete solution for managing an online store. This API includes features such as JWT authentication and Swagger documentation, making it easy to integrate and use.

## Features

- **Full E-commerce Management**: Handle products, categories, orders, and users.
- **JWT Authentication**: Secure your API with token-based authentication.
- **Swagger Documentation**: Interactive API documentation for easy testing and exploration.
- **Database Integration**: Connects seamlessly with SQL Server using Entity Framework.
- **Scalability**: Designed to grow with your business needs.

## Technologies Used

- **Backend**: ASP.NET Core
- **Database**: SQL Server
- **ORM**: Entity Framework
- **Authentication**: JWT
- **API Documentation**: Swagger
- **Framework**: .NET

## Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or later)
- SQL Server (Express or higher)
- A code editor (Visual Studio, Visual Studio Code, etc.)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/sameterl/LojaVirtualAPI.git
   ```

2. Navigate to the project directory:
   ```bash
   cd LojaVirtualAPI
   ```

3. Restore the NuGet packages:
   ```bash
   dotnet restore
   ```

4. Update the database connection string in `appsettings.json`.

5. Run the migrations to set up the database:
   ```bash
   dotnet ef database update
   ```

6. Start the application:
   ```bash
   dotnet run
   ```

### Configuration

Configure the application by editing the `appsettings.json` file. Here, you can set your database connection string, JWT secret key, and other application settings.

## API Documentation

The API is documented using Swagger. Once the application is running, navigate to `http://localhost:5000/swagger` to access the interactive API documentation. This allows you to test endpoints directly from your browser.

## Authentication

The API uses JWT for authentication. To access protected resources, you need to obtain a token by sending a POST request to the `/api/auth/login` endpoint with valid user credentials.

### Example Login Request

```json
POST /api/auth/login
{
  "username": "your_username",
  "password": "your_password"
}
```

Upon successful login, the API will return a JWT token. Use this token in the `Authorization` header for subsequent requests.

## Endpoints

Here are some key endpoints available in the API:

### Products

- **Get All Products**
  - **Endpoint**: `GET /api/products`
  - **Description**: Retrieve a list of all products.

- **Get Product by ID**
  - **Endpoint**: `GET /api/products/{id}`
  - **Description**: Retrieve a specific product by its ID.

- **Create Product**
  - **Endpoint**: `POST /api/products`
  - **Description**: Add a new product to the store.

### Categories

- **Get All Categories**
  - **Endpoint**: `GET /api/categories`
  - **Description**: Retrieve a list of all categories.

- **Create Category**
  - **Endpoint**: `POST /api/categories`
  - **Description**: Add a new category to the store.

### Orders

- **Get All Orders**
  - **Endpoint**: `GET /api/orders`
  - **Description**: Retrieve a list of all orders.

- **Create Order**
  - **Endpoint**: `POST /api/orders`
  - **Description**: Create a new order.

## Contributing

Contributions are welcome! If you want to improve this project, please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/YourFeature`).
3. Make your changes.
4. Commit your changes (`git commit -m 'Add some feature'`).
5. Push to the branch (`git push origin feature/YourFeature`).
6. Open a Pull Request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

For more information, visit the [Releases section](https://github.com/sameterl/LojaVirtualAPI/releases) to download the latest version and execute it.
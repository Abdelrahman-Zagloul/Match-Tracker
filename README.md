# ⚽ Match Tracker

**Match Tracker** is a full-featured web application built with **ASP.NET MVC** to help football fans explore, track, and manage matches, teams, players, and stadiums.  
It offers **role-based access** for Admins and Users, a **responsive UI/UX**, and a **scalable architecture**.

---

## ✨ Features

### 🔐 Authentication & Authorization

The application supports **role-based access control**, with two main roles:

- **Admin**
- **User**

**Common Features for Both Roles:**
- 📝 User registration and login  
- 👤 Profile editing  
- 🔒 Password management  

---

### 🧑‍💼 Admin Capabilities

- **Matches**: View, add, edit, delete, and view detailed match information  
- **Stadiums**: Full CRUD operations, search by name  
- **Teams**: Full CRUD operations, search by name, country, or continent, bulk delete  
- **Players**: Full CRUD operations, search by name, country, or team, bulk delete  
- **User Management**: View users, add/delete users, view profiles, reset passwords  

---

### 🙋‍♂️ User Capabilities

**Match Exploration:**
- View upcoming and past matches  
- Filter by: today, tomorrow, specific dates, or past days  
- View match details (teams, stadium, players)  

**Profiles & Personalization:**
- View team, player, and stadium profiles  
- Edit profile details  
- Change password  

---

## 📊 Admin Dashboard

- Centralized access to all entities with full CRUD operations  
- Advanced search and filtering  
- AJAX-driven interactions for a seamless experience  

---

## 🖼️ Image Management

- Upload and store images for:
  - Players
  - Team logos
  - Stadiums  
- Dynamic image display across the application  

---

## 🔁 AJAX Enhancements

- Partial updates for smoother interactions  
- Examples:
  - Live search
  - Deletion without page reloads  

---

## ✅ Validation

- **Server-Side**: FluentValidation for robust model validation  
- **Client-Side**: jQuery Unobtrusive Validation for real-time feedback  

---

## 🛠️ Technologies Used

- **Framework**: ASP.NET MVC  
- **Database**: Entity Framework Core, SQL Server  
- **Data Access**: LINQ, Repository & Service Pattern  
- **Architecture**: Layered Architecture  
- **Validation**: FluentValidation (server), jQuery Validation (client)  
- **Frontend**: HTML,CSS,Java Script ,Bootstrap 5, jQuery, AJAX  
- **Logic**: ViewModels for clean view separation  
- **File Management**: Image uploads for players, teams, and stadiums  

---

## 📁 Project Structure

- **Layered Architecture** – Ensures maintainability and scalability  
- **Repository & Service Pattern** – Streamlines data access  
- **ViewModels** – Facilitates clean data transfer to views  

---

## ⚙️ Prerequisites

- .NET Framework or .NET Core (depending on your setup)  
- SQL Server  
- Visual Studio (or another compatible IDE)  
- Node.js (for frontend dependencies, if applicable)  

---

## 🚀 Installation

1. **Clone the Repository**
   ```bash
   git clone https://github.com/Abdelrahman-Zagloul/Match-Tracker.git

 ## 🛠️ Restore Dependencies

1. **Open the solution** in Visual Studio  
2. **Restore NuGet packages** automatically via Visual Studio or by using the Package Manager Console:
   ```bash
   dotnet restore
  ## 🗄️ Configure the Database

1. Update the connection string in:
   - `appsettings.json` (if you're using .NET Core)
   - or `web.config` (if you're using .NET Framework)

2. Apply the Entity Framework migrations to create the database:
   ```bash
   Update-Database
  
  ## ▶️ Run the Application

1. **Build and run** the solution using Visual Studio.

2. Once the application is running, open your browser and navigate to:
 ```bash
    http://localhost:<port>

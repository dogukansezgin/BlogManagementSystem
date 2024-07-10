# Blog Management System

This project is a case study prepared for an interview. Its purpose is to create a system that allows users to write, edit and delete blog posts.

## Features

- User login and registration
- Basic user management (authorization and authentication)
- Create, edit and delete blog posts
- List blog posts and filter by date or author
- Users can comment on blog posts

## Technologies Used

- **Backend:** .NET Core
- **Frontend:** Angular

## Project Structure

- **backend/**: .NET Core project
- **frontend/**: Angular project

## Getting Started

Follow these steps to run the project:
1. **Prepare the Database:**<br>
   Configure the database connection string in `appsettings.json`, and create tables using migrations.
   
3. **Start the Backend Service:**
   ```bash
   cd backend/BlogManagementSystem-Backend
   dotnet run --project src/BlogManagementSystem/WebAPI
   
4. **Start the Frontend Application:**
   ```bash
   cd frontend/BlogManagementSystem-Frontend
   npm install
   npm start

5. **Open in Browser:**<br>
   Go to `localhost:4200` to view the application.

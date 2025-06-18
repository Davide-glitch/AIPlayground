# AI Playground - Full Stack Web Application

A full-stack web application built with React (frontend) and .NET 8 (backend) for AI model experimentation and testing.

## 🚀 Getting Started

This project consists of two main parts:

- **Frontend**: React TypeScript application with Material-UI
- **Backend**: .NET 8 Web API with Entity Framework Core

## 📋 Prerequisites

Before you begin, make sure you have the following installed on your machine:

### Required Software

- **Node.js** (version 16 or higher) - [Download here](https://nodejs.org/)
- **npm** (comes with Node.js) or **yarn**
- **.NET 8 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Git** - [Download here](https://git-scm.com/)

### Optional but Recommended

- **Visual Studio Code** - [Download here](https://code.visualstudio.com/)
- **Visual Studio 2022** (for backend development)

## 🔧 Installation & Setup

### 1. Clone the Repository

```bash
git clone <your-repository-url>
cd source
```

### 2. Backend Setup (.NET API)

Navigate to the backend directory and restore dependencies:

```bash
cd backend/AIPlayground
dotnet restore
```

Build the project:

```bash
dotnet build
```

### 3. Frontend Setup (React)

Navigate to the frontend directory:

```bash
cd frontend/aiplayground-fe
```

Install npm dependencies:

```bash
npm install
```

This will create the `node_modules` folder and install all required packages listed in `package.json`.

## 🏃‍♂️ Running the Application

### Option 1: Quick Start (Recommended)

Use the provided batch file to start both frontend and backend simultaneously:

```bash
# From the root directory (source/)
start.bat
```

This will:

- Start the backend API server on HTTPS
- Start the React development server
- Open two command prompt windows for each service

### Option 2: Manual Start

#### Start Backend API

```bash
cd backend/AIPlayground
dotnet run --launch-profile https
```

The API will be available at: `https://localhost:7000` (or the port specified in launchSettings.json)

#### Start Frontend (in a new terminal)

```bash
cd frontend/aiplayground-fe
npm start
```

The React app will be available at: `http://localhost:3000`

### Option 3: Using VS Code Tasks

If using VS Code, you can use the configured task:

- Press `Ctrl+Shift+P`
- Type "Tasks: Run Task"
- Select "Start React Development Server"

## 📱 Available Scripts (Frontend)

In the frontend directory (`frontend/aiplayground-fe`), you can run:

### `npm start`

Runs the app in development mode.
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.
The page will reload if you make edits.

### `npm test`

Launches the test runner in interactive watch mode.

### `npm run build`

Builds the app for production to the `build` folder.
It correctly bundles React in production mode and optimizes the build for the best performance.

### `npm run eject`

**Note: this is a one-way operation. Once you eject, you can't go back!**
This removes the single build dependency from your project.

## 🛠️ Backend Commands

In the backend directory (`backend/AIPlayground`), you can run:

### `dotnet run`

Runs the API in development mode (HTTP)

### `dotnet run --launch-profile https`

Runs the API with HTTPS enabled

### `dotnet build`

Builds the project

### `dotnet test`

Runs unit tests (if any)

### `dotnet watch run`

Runs the API with hot reload enabled

## 🗂️ Project Structure

```
source/
├── backend/
│   ├── AIPlayground/              # Main API project
│   ├── AIPlayground.BusinessLogic/ # Business logic layer
│   └── AIPlayground.DataAccess/   # Data access layer
├── frontend/
│   └── aiplayground-fe/           # React TypeScript app
├── package.json                   # Root package.json
├── start.bat                      # Quick start script
└── README.md                      # This file
```

## 🔧 Dependencies

### Frontend Dependencies

- **React 19** - UI framework
- **TypeScript** - Type safety
- **Material-UI (MUI)** - UI components
- **Axios** - HTTP client
- **Testing Library** - Testing utilities

### Backend Dependencies

- **.NET 8** - Web framework
- **Entity Framework Core** - ORM
- **Swagger/OpenAPI** - API documentation
- **AI Libraries** - DeepSeek, Gemini API

## 🌐 API Documentation

When the backend is running, you can access the Swagger API documentation at:

- `https://localhost:7000/swagger` (HTTPS)
- `http://localhost:5000/swagger` (HTTP)

## 🔍 Troubleshooting

### Common Issues

#### Node.js/npm Issues

- **"npm command not found"**: Install Node.js from the official website
- **Permission errors**: Try running PowerShell as Administrator
- **Package installation fails**: Delete `node_modules` and `package-lock.json`, then run `npm install`

#### .NET Issues

- **".NET SDK not found"**: Install .NET 8 SDK
- **Port already in use**: Change the port in `launchSettings.json`
- **Database issues**: Check connection string in `appsettings.json`

#### General Issues

- **CORS errors**: Backend and frontend must be running on different ports
- **API not accessible**: Ensure backend is running before starting frontend

### Reset Everything

If you encounter issues, try this complete reset:

```bash
# Clean frontend
cd frontend/aiplayground-fe
rm -rf node_modules package-lock.json
npm install

# Clean backend
cd backend/AIPlayground
dotnet clean
dotnet restore
dotnet build
```

## 🚀 Deployment

### Frontend (Production Build)

```bash
cd frontend/aiplayground-fe
npm run build
```

The `build` folder will contain the optimized production files.

### Backend (Production)

```bash
cd backend/AIPlayground
dotnet publish -c Release -o ./publish
```

## 📚 Learn More

- [React Documentation](https://reactjs.org/)
- [Create React App Documentation](https://facebook.github.io/create-react-app/docs/getting-started)
- [.NET 8 Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [Material-UI Documentation](https://mui.com/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

---

**Happy coding! 🎉**

For any issues or questions, please open an issue in the GitHub repository.

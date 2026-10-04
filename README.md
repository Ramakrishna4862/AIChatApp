# AIChatApp

An AI-powered chatbot web application built with **ASP.NET Core MVC, SQL Server, Entity Framework Core, and AI service integrations**.

## 🚀 Features

* 💬 Chat with an AI assistant
* 🤖 AI response generation through configurable AI services
* 🔄 Fallback AI service support
* 💾 Save chat conversations to SQL Server
* 📜 View chat history
* 🔍 Search conversations
* 🗑️ Delete conversations
* 📄 Pagination for chat history
* 🔐 API keys stored using environment variables
* 🎨 Responsive user interface using Bootstrap

## 🛠️ Technologies Used

* **C#**
* **ASP.NET Core MVC**
* **Entity Framework Core**
* **SQL Server**
* **Razor Views**
* **HTML5**
* **CSS3**
* **JavaScript**
* **Bootstrap**
* **REST APIs**
* **Git & GitHub**

## 🤖 AI Integration

The application is designed with an AI service abstraction using an `IAIService` interface.

Different AI services can be configured without changing the main chat functionality.

Current service implementations include:

* Fake AI Service
* Gemini AI Service
* Groq AI Service
* OpenAI Service
* OpenRouter AI Service
* Fallback AI Service

API keys are read from environment variables and are **not stored directly in the source code**.

## 🗄️ Database

The application uses **Microsoft SQL Server** with **Entity Framework Core**.

Chat messages are stored with information such as:

* User message
* AI response
* Created date
* Chat session

Entity Framework Core migrations are used to create and update the database schema.

## 📂 Project Structure

```text
AIChatApp
│
├── Controllers
│   └── ChatController.cs
│
├── Models
│   ├── ChatMessage.cs
│   ├── ChatSession.cs
│   └── ChatViewModel.cs
│
├── Services
│   ├── IAIService.cs
│   ├── FakeAIService.cs
│   ├── GeminiAIService.cs
│   ├── GroqAIService.cs
│   ├── OpenAIService.cs
│   ├── OpenRouterAIService.cs
│   └── FallbackAIService.cs
│
├── Views
│   └── Chat
│       ├── Index.cshtml
│       └── History.cshtml
│
└── Data
    └── ApplicationDbContext.cs
```

## ⚙️ How to Run

### 1. Clone the repository

```bash
git clone https://github.com/Ramakrishna4862/AIChatApp.git
```

### 2. Open the project

Open the solution in **Visual Studio 2022**.

### 3. Configure SQL Server

Update the connection string in `appsettings.json` according to your SQL Server instance.

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=AIChatAppDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 4. Configure the AI API key

Set the required API key as an environment variable.

For example:

```text
GEMINI_API_KEY
```

or

```text
GROQ_API_KEY
```

Do not add real API keys to `appsettings.json` or commit them to GitHub.

### 5. Apply database migrations

Run:

```powershell
Update-Database
```

from the Visual Studio Package Manager Console.

### 6. Run the application

Press:

```text
Ctrl + F5
```

or run the project from Visual Studio.

## 🔐 Security

API keys are accessed through environment variables rather than being hard-coded into the application.

**Never commit real API keys to GitHub.**

## 📌 Future Improvements

* User authentication and authorization
* Multiple independent chat sessions
* Improved AI conversation context
* Streaming AI responses
* File upload support
* More advanced chat search and filtering
* Deployment to a cloud platform

## 👨‍💻 Author

**Rama Krishna**

.NET Developer | ASP.NET Core | C# | SQL Server

GitHub: https://github.com/Ramakrishna4862

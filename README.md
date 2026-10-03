# AIChatApp 🤖

AIChatApp is an AI-powered chatbot application built using **ASP.NET Core MVC**, **SQL Server**, and **Entity Framework Core**. The application integrates multiple AI providers and automatically switches to an alternative provider when the primary provider is unavailable, rate-limited, or encounters an error.

## 🚀 Features

* AI-powered chatbot interface
* Multiple chat session management
* Create and manage conversations
* Store chat history in SQL Server
* Search chat history
* Delete individual messages
* Delete entire chat sessions
* Automatic AI provider fallback
* Dependency Injection (DI) implementation
* Environment variable-based API key management
* Error and timeout handling
* Responsive user interface using Bootstrap

## 🔄 AI Provider Fallback Flow

The application automatically switches between AI providers to improve reliability.

```text
User Question
      ↓
FallbackAIService
      ↓
Gemini AI
      ↓ Failed
Groq AI
      ↓ Failed
OpenRouter AI
      ↓ Failed
Display Error Message
```

## 🛠️ Technologies Used

### Backend

* ASP.NET Core MVC
* C#
* Entity Framework Core
* SQL Server

### Frontend

* HTML
* CSS
* Bootstrap
* JavaScript
* Razor Views

### AI Integrations

* Google Gemini API
* Groq API
* OpenRouter API

## 🏗️ Architecture

### Services

* `IAIService`
* `GeminiAIService`
* `GroqAIService`
* `OpenRouterAIService`
* `FallbackAIService`

### Database Entities

#### ChatSession

Stores conversation sessions and metadata.

#### ChatMessage

Stores user prompts, AI responses, timestamps, and session references.

## 📚 Concepts Implemented

* Dependency Injection
* Service-Oriented Architecture
* Async/Await Programming
* HTTP Client Integration
* External API Consumption
* Entity Framework Core
* SQL Server Database Operations
* Error Handling and Exception Management
* Multi-Provider AI Fallback Strategy

## ⚙️ Installation & Setup

### Prerequisites

* Visual Studio 2022
* .NET SDK
* SQL Server
* SQL Server Management Studio (SSMS)

### Setup Steps

1. Clone the repository:

```bash
git clone <repository-url>
```

2. Open the solution in Visual Studio.

3. Configure the SQL Server connection string in `appsettings.json`.

4. Apply Entity Framework migrations:

```bash
Update-Database
```

5. Configure environment variables:

```text
GEMINI_API_KEY
GROQ_API_KEY
OPENROUTER_API_KEY
```

6. Run the application.

## 📸 Screenshots

### Chat Interface

![Chat Interface](screenshots/chat-interface.jpg)

### Chat Sessions

![Chat Sessions](screenshots/chat-sessions.jpg)

### Chat History

![Chat History](screenshots/chat-history.jpg)

## 🎯 Learning Outcomes

This project helped me gain hands-on experience with:

* ASP.NET Core MVC Development
* SQL Server and Entity Framework Core
* Dependency Injection
* API Integration
* Multi-AI Provider Architecture
* Fallback and Error Handling Mechanisms
* Asynchronous Programming
* Service-Based Application Design

## 👨‍💻 Author

**Rama Krishna R**

LinkedIn: linkedin.com/in/rama-krishna-a80133174

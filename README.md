# AIChatApp

AIChatApp is an AI-powered chatbot application built using ASP.NET Core MVC, SQL Server, and Entity Framework Core. The application supports multiple AI providers and automatically falls back to another provider if the current provider is unavailable or exceeds its quota.

## Features

* AI-powered chatbot interface
* Chat session management
* Create multiple chat conversations
* Delete chat sessions
* Store chat history in SQL Server
* Search chat history
* Automatic AI provider fallback
* Error handling and timeout handling
* Dependency Injection implementation
* Environment variable-based API key management

## AI Provider Fallback Flow

The application automatically switches between AI providers when a provider is unavailable.

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

## Technologies Used

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

### AI Integration

* Google Gemini API
* Groq API
* OpenRouter API

## Project Architecture

### Services

* IAIService
* GeminiAIService
* GroqAIService
* OpenRouterAIService
* FallbackAIService

### Database Entities

#### ChatSession

Stores conversation sessions.

#### ChatMessage

Stores user messages and AI responses.

## Key Concepts Implemented

* Dependency Injection
* Service-Based Architecture
* Repository Pattern Concepts
* Async/Await Programming
* HTTP Client Integration
* API Consumption
* Error Handling
* Fallback Strategy Design

## Installation

### Prerequisites

* Visual Studio 2022
* .NET SDK
* SQL Server
* SQL Server Management Studio (SSMS)

### Setup

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Update the SQL Server connection string.
4. Run Entity Framework migrations.
5. Configure environment variables:

   * GEMINI_API_KEY
   * GROQ_API_KEY
   * OPENROUTER_API_KEY
6. Run the application.

## Chat Interface

![Chat Interface](screenshots/chat-interface.png)

## Chat Sessions

![Chat Sessions](screenshots/chat-sessions.png)

## Chat History

![Chat History](screenshots/chat-history.png)

## Learning Outcomes

This project helped me gain practical experience with:

* ASP.NET Core MVC
* SQL Server and Entity Framework Core
* Dependency Injection
* External API Integration
* Multi-provider AI Architecture
* Error Handling and Fallback Mechanisms

## Author

Rama Krishna R

LinkedIn:
linkedin.com/in/rama-krishna-a80133174

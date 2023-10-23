# 💻 Immunisation Trial Presentation
During .NET Full-Stack student program, together with other colleagues, we built an application for creating, completing, updating some surveys about medical diseases.

## 🛠️ Architecture
This .NET Core project follows a layered architecture pattern, which is a common approach to structuring applications. In this architecture, the code is organized into different layers, each with a specific role and responsibility. Here's a breakdown of the layers and their purpose:
* Controllers: This layer typically contains the application's entry points and is responsible for handling HTTP requests and managing the flow of the application.
* DAL (Data Access Layer): This layer is responsible for interacting with the application's data sources, such as databases. It contains code for querying, updating, and generally managing the data.
* Data: It can contain data models, view models, and other data-related structures. It often acts as a bridge between the database and the application logic, providing data transfer objects (DTOs) and entities that can be used in the business logic.

<p align="center">
  <img src="https://github.com/Piciorus-Ovidiu-Mihai/Immunisation-Trial-Code/blob/master/immunisation-trial-architecture.png">
</p>

* Properties: This package include configuration files, settings, and resources needed by the application. This layer ensures that the application can be configured and customized without modifying the core code.
* Services: This layer contains the core business logic of the application. It includes service classes that handle various operations, such as user authentication, data processing, and more.
* Library Project for Entities: This Class Library contains the entity classes or models that represent the data structures in your application. These entities are used in the DAL for database operations and in the Data layer to transfer data.
* Separate Email Service: This is a dedicated component for sending emails. This separation promotes code reusability and simplifies the management of email-related tasks.

## 📷 Preview  
In this section, there are provided some visual previews of key pages and features within our application. Get a glimpse of the following:

Dashboard Page where can be explored this user-friendly dashboard, where you can access vital information at a glance and manage your data efficiently.

<p align="center">
  <img src="https://github.com/Piciorus-Ovidiu-Mihai/Photos/blob/master/home.PNG">
</p>

Create Survey Page where the survet are created using an intuitive interface. Customize surveys to based on needs effortlessly.

<p align="center">
  <img src="https://github.com/Piciorus-Ovidiu-Mihai/Photos/blob/master/savesurvey.PNG">
</p>

<p align="center">
 <img src="https://github.com/Piciorus-Ovidiu-Mihai/Photos/blob/master/statistics.PNG">
</p>

Manage Users and Roles Page represent the user management tools and role assignment options, ensuring that the application remains secure and organized.

<p align="center">
  <img src="https://github.com/Piciorus-Ovidiu-Mihai/Photos/blob/master/adminapge.PNG">
</p>

These previews offer a sneak peek into the user interface and functionality, giving you a quick overview of what our application has to offer.

## 🛡️ Key Features
* Modularity: The layered architecture promotes modularity and separation of concerns, making it easier to manage and maintain your codebase.
* Scalability: It allows for easier scaling of the application as different layers can be extended or enhanced independently.
* Testability: The separation of concerns in different layers makes it easier to write unit tests for your code.
* Code Reusability: Components like the email service can be reused in other parts of your application or in future projects.
* Clear Structure: A layered architecture provides a clear and organized structure for your project, making it easier for developers to collaborate and understand the codebase.
* Flexibility: With different layers, it adapt and extend the application as requirements change.

## 💽 Prerequisites
* ⭐ Visual Studio
* ⭐ SQL Server Management Studio

## 🚀 Getting Started
* ⚒️ Clone the repository
* ⚒️ Setup database
* ⚒️ Build the project

## 🖥️ Technologies
* 💽 `C#`
* 💽 `ASP.NET Core`
* 💽 `SQL`
* 💽 `Razor Pages`


## 💎 Implementation & Preview
This application contains: 
* 🚀 An API for connecting the client with the database. 
* 🚀 Client app built with ASP.NET MVC Core and Razor Pages
* 🚀 A library with all entities

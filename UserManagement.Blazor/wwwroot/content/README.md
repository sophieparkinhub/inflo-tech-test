# User Management Technical Exercise

The exercise is an ASP.NET Core web application backed by Entity Framework Core, which faciliates management of some fictional users.
We recommend that you use [Visual Studio (Community Edition)](https://visualstudio.microsoft.com/downloads) or [Visual Studio Code](https://code.visualstudio.com/Download) to run and modify the application. 

**The application uses an in-memory database, so changes will not be persisted between executions.**

## The Exercise
Complete as many of the tasks below as you feel comfortable with. These are split into 4 levels of difficulty 
* **Standard** - Functionality that is common when working as a web developer
* **Advanced** - Slightly more technical tasks and problem solving
* **Expert** - Tasks with a higher level of problem solving and architecture needed
* **Platform** - Tasks with a focus on infrastructure and scaleability, rather than application development.

### 1. Filters Section (Standard)

The users page contains 3 buttons below the user listing - **Show All**, **Active Only** and **Non Active**. Show All has already been implemented. Implement the remaining buttons using the following logic:
* Active Only – This should show only users where their `IsActive` property is set to `true` - **Completed**
* Non Active – This should show only users where their `IsActive` property is set to `false` - **Completed**

### 2. User Model Properties (Standard)

Add a new property to the `User` class in the system called `DateOfBirth` which is to be used and displayed in relevant sections of the app. - **Completed**

### 3. Actions Section (Standard)

Create the code and UI flows for the following actions
* **Add** – A screen that allows you to create a new user and return to the list - **Completed**
* **View** - A screen that displays the information about a user - **Completed**
* **Edit** – A screen that allows you to edit a selected user from the list - **Completed**
* **Delete** – A screen that allows you to delete a selected user from the list - **Completed**

Each of these screens should contain appropriate data validation, which is communicated to the end user. - **Completed**

### 4. Data Logging (Advanced)

Extend the system to capture log information regarding primary actions performed on each user in the app.
* In the **View** screen there should be a list of all actions that have been performed against that user. - **Completed**
* There should be a new **Logs** page, containing a list of log entries across the application. - **Completed**
* In the Logs page, the user should be able to click into each entry to see more detail about it. - **Completed**
* In the Logs page, think about how you can provide a good user experience - even when there are many log entries. - **Completed**

### 5. Extend the Application (Expert)

Make a significant architectural change that improves the application.
Structurally, the user management application is very simple, and there are many ways it can be made more maintainable, scalable or testable.
Some ideas are:
* Re-implement the UI using a client side framework connecting to an API. Use of Blazor is preferred, but if you are more familiar with other frameworks, feel free to use them. - **Completed**
* Update the data access layer to support asynchronous operations. - **Completed**
* Implement authentication and login based on the users being stored.
* Implement bundling of static assets.
* Update the data access layer to use a real database, and implement database schema migrations. - **Completed**

### 6. Future-Proof the Application (Platform)

Add additional layers to the application that will ensure that it is scaleable with many users or developers. For example:
* Add CI pipelines to run tests and build the application.
* Add CD pipelines to deploy the application to cloud infrastructure.
* Add IaC to support easy deployment to new environments.
* Introduce a message bus and/or worker to handle long-running operations.

## Additional Notes

* Please feel free to change or refactor any code that has been supplied within the solution and think about clean maintainable code and architecture when extending the project.
* If any additional packages, tools or setup are required to run your completed version, please document these thoroughly.

## Notes to run the system
* Please check the appsettings before running the application. Ensure the DefaultConnection server matches your local server to ensure migrations run.

## Packages added
* Added Markdig as an alternative to Westwind markdown as it supported blazor webassembly.
* Added Mudblazor as it has reusable stylish components that can be customised to tailor your business.

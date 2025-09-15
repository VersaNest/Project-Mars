# ProjectMars

ProjectMars is a web-based application where users can create their account and add their languages, skills, experiences, etc.  
Only authorised users can access the application with valid Email and Password.

---

## Features Implemented
- User Authentication: Login with valid credentials, Create new account, Remember Me functionality, Forgot password.
- Profile Management: Add, update, and delete functionalities for languages and skills.
- Hooks file implemented for before/after scenarios and cleanup of languages/skills after testing.

---

## Technologies Used
- Reqnroll (Behavior Driven Development - BDD)
- Selenium
- C#

---

## Project Structure

Reqnroll_ProjectMars/
├── Features/ # Feature files for login and profile pages
├── Hooks/ # Hooks for setup, teardown, and clean up after each test execution
├── Pages/ # Page Object classes (Login and Profile pages)
├── StepDefinitions/ # Step definition classes for the feature files
├── Utilities/ # Common wait methods (Base Page) and configuration reader
└── appsettings.json

## Instructions for Running the Tests
1. Clone the project repository: `http://git.mvp.studio/qa-examples/ta_mars_docker.git` and follow the steps given.
2. Clone this repository.
3. Open **Visual Studio 2022** and install the required NuGet packages.
4. Build the solution and run the test cases.

---

## Notes
- `bin/` and `obj/` folders are excluded from the repository.
- Test cases for the features are added in the Excel file and uploaded.
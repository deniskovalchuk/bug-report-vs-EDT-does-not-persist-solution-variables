# Visual Studio Report

<h2>Problem description</h2>

[`DTE.Solution.Globals.VariablePersists`](https://learn.microsoft.com/en-us/dotnet/api/envdte.globals.variablepersists) does not persist variables when an `.slnx` solution is saved for the first time.

<h2>Prerequisites</h2>

Visual Studio 2026 with the **Visual Studio extension development** workload installed.

<h2>Steps to reproduce</h2>

1. Open Visual Studio 2026.
2. Create a new `Console App (.NET Framework)` project.
   - `File -> New -> Project/Solution...`.
3. Close Visual Studio 2026.
4. Clone or download this repository.
5. Open a project from the repository in Visual Studio 2026.
6. `Debug -> Start (F5)`.
7. In the experimental instance open the `ConsoleApp` project created in step 2.
8. `Tools -> Add variable to Solution`.

    <img src="https://github.com/deniskovalchuk/bug-report-vs-EDT-does-not-persist-solution-variables/blob/756d5f2055f767ef795c79411da51404926a3316/Images/AddVariable.png" width="40%" alt="Image" />

10. `File -> Close Solution` and click `Save`.
11. Open the `ConsoleApp` again.
12. `Tools -> Get variable from Solution`.

    **Expected behavior**  
    <img src="https://github.com/deniskovalchuk/bug-report-vs-EDT-does-not-persist-solution-variables/blob/756d5f2055f767ef795c79411da51404926a3316/Images/GetVariableExpected.png" width="40%" alt="Image" />

    **Actual behavior**  
    <img src="https://github.com/deniskovalchuk/bug-report-vs-EDT-does-not-persist-solution-variables/blob/756d5f2055f767ef795c79411da51404926a3316/Images/GetVariableActual.png" width="40%" alt="Image" />

<h2>Additional Information</h2>

- This functionality works correctly for `.sln` solutions.
- This functionality works correctly if you explicitly save the solution multiple times.
- See `AddVariableCommand.Execute()` and `GetVariableCommand.Execute()` for more details.

<h2>Environment</h2>

Microsoft Visual Studio Professional 2026 Version 18.9.2  
Microsoft Windows 11 Pro Version 10.0.26200 Build 26200

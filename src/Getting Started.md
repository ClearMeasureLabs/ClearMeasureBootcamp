## Getting Started

Clear Measure Bootcamp Projct Preqsets

To work on the Clear Measure Expense Report project you should have the following prerequisite:

Basic knowledge of Visual Studio, C#, SQL, FluentNHibernate, Selenium, Onion Architecture, 
Unit testing, Razor Views, Jquery, Bootstrap.

So lets get started:

## Project Overview

Bootcamp project contained the following parts:

1. Core Project - Contains the models, features, plugins and services. 

2. DataAccess Project - Contains data access functions and well as database transaction 
functions.

3. Database Project - Allows the user to reset the database to factory defaults and 
restart the project from a base line.

4. IntregrationTests Project - Contains test data and tests of certain core and 
database functionalities.

5. PerformanceTests Project - Start project performance tests on the Azure platform.

6. SmokeTests Project- Quick test of the integrity of the project using Selenium and
various browsers.

7. UI Project - The user interface of the project. Contains applicaiton pages and UI
functionality.

8. UI.DependencyResolution Project - Contains UI depencency of the project.

9. UnitTests Project - Contains test suites of the Core project. 

Let me just talk briefly about application development in general. The main point of 
breaking things down to individual parts is to help the user reason about the 
application in a better way. In application development, the structure needs to be 
organized and be consistent because the whole is very complex and dense. If that is not
done then chaos and ineffeicy will very quickly seep into the project and derail progress.

Now we can dive deeper into each project and describe some main concepts, 

## Core Project

Core project consists of 5 main parts:

1. Features -  
2. Model
3. Plugins
4. Service
5. Bus, Irequest, IrequestHandler

We can discuss them in detail.

** Features **

Expense Report features contains 3 main areas:

1. MulitpleExpenses
2. SearchExpenseReports
3. Workflow

** MulitpleExpenses **

MulitpleExpenses features AddExpenseCommand and AddexpenseResult.

AddexpenseCommand is a request object that takes in Report, Currentuser, Amount, 
Description and CurrentData.

The result is of type AddExpenseResult.

** SearchExpenseReports  **

SearchExpenseReports feature contains ExpenseReportSpecificationQuery which is a 
request. SearchReportSpeicificaitonQuery contains a request which takes Status, Approver, Submitter.

** Workflow **

Workflow contains 3 files:

1. ExecuteTransitionCommand
2. ExecuteTransitionCommandHandler
3. ExecuteTransitionResult

ExecuteTransitionCommand is a request that takes in Report, command, currentUser, currentDate. 

ExecuteTransitionCommandHandler is a request handler that uses ExecuteTransitionCommand, and 
saves expense report and returns ExecuteTransitionResult result.

ExecuteTransitionResult is the result that contains NewStatus, NextStep, Action, Message.

** Model **

The next section of the Core project is the Model. This is the most straight forward piece 
of the project. Model contains objects that are used in other piece of the core and the solution.
It have very little logic, and mostly contains classes, constructors and preset values 
like in ExpenseReportFact, ExpenseReportStatus, and AuditEntry. 

The one thing that I like to touch on is in ExpenseReportWorkflow folder. There is a 
StateCommandBase and there are a few objects that derive from the base command. The base command 
implements the IStateCommand interface.


** Plugins / DataAccess **

Pluging area contains a DataAccess folder. The contents are models of TRequest type such as 
EmployeeByUserNameQuery, EmployeeSpecificationQuery, ExpenseReportByNumberQuery, ExpenseReportSaveCommand.
It also contains SingleResult and MulipleResult is of TResponse type.

** Services **











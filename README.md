# FlowerProject

## Setup Instructions
1) Find the env.ps1 inside FlowerInventoryAssessment/App/Scripts/ directory and change it to match your own connection strings
2) At the same directory mentioned above find the Sql scripts for the creation of the databases used

   - You may need to change the location of your where mdf files will be stored according to what Sql Server version you have installed or in what location Sql server exists

## Implementation Description
Project implementation started with reviewing the given requirements and making research for additional insight. Furthermore designed simple wireframes (pen & paper) for the views and how the navigation will be, in order to visualize the app ahead of time. Next in line was desinging the database and while at the same time designing the repositories & services that would exist in app. After that design & research was just developing the application (views & functionality).

## Challenges Faced
Challenges were few and not so technical in my opinion
1)   Firstly was deciding the assumptions to complete the application (clearly such an app could be way bigger)
2)   My unfamiliarity with service layer architecture (which made me spent additional time on research and refinements of code)

## Assumptions
1)   No authentication (by no means such an app needs no authentication system)
2)   Will be used by one user at a time (hence the first point)
3)   Application will be published on IIS
####   Database related Assumptions
4)   Wouldn't need to keep track of inventory changes (separate table for Flower inventory changes)
5)   No track of suppliers/providers/sellers of flowers
6)   No track of which seasons each flower can be in inventory
7)   No track of conditions(temperature, humidity, etc.) for flowers to survive

## Technologies
- C#
- .Net 8
- MVC
- Sql Server 2022 Express
- Entitiy Framework Core

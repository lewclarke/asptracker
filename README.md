This is a simple web plugin developed using C# and ASP .NET. It will track what pages a user has visted on the website and for how long. When a user signs up for a 
newsletter (providing their e-mail address) this information will then be stored along with their email.

All that is required is a few simple lines of code at the top of a web page to use the web tracker. Everything else is done by the code in the tracker file.

The tracker records five different pieces of information about a user whenever they visit a page on your website:

- IP Address
- User Agent
- Date/Time page was opened
- Duration of time spent on page
- Name of page visited


Provided are two different versions of the tracker. See below for details:


asp_tracker_always
-------------------

This version of the tracker records every user movement on your website. Whenever a user visits a page it will be recorded to the SQL database. 


asp_tracker_subscribe
----------------------

This version of the tracker records all user movement on your website to session state. Only when a user subscribes to the website (newsletter etc.) will the data
be stored from session state into the SQL database.



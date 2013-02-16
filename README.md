This is a simple web plugin developed using C# and ASP .NET. It will track what pages a user has visted on the website and for how long. When a user signs up for a 
newsletter (providing their e-mail address) this information will then be stored along with their email.

All that is required is a simple line of code at the top of a web page (HTML file) to use the web tracker. Everything else is done by the code in the tracker file.


Version History:

v2.00
------
(Records IP Address, Date/Time Visited, Duration of time spent on page, User Agent and Page Name)

+ Implemented tracking of duration spent on page. Achieved by recording the time on page load and on page unload. When subtracting page load time from page unload 
time you get the duration spent on the page. Session state is used to record page load time. This is the only use of session state throughout the tracker.

+ Created a config.cfg file where the SQL database connection strings and table name is stored. This allows you to quickly change SQL settings without ever touching 
the code. All tracker functions now make use of this config file for looking up SQL settings.

+ Code implemented in any HTML page to implement tracking features has now increased to four lines of code (previously one). This is to allow for the new time 
tracking features. This implementation script should not need changing in future versions (nor increase in line amount).

+ Fixed various spelling bugs on test website

- Removed HostName variable from being recorded. This only recorded the servers hostname which as it would always remain the same their was no need to track it. It 
also provided no information regarding the client (information we are tracking).

v1.50
------
(Records IP Address, Date/Time Visited, Server Host Name, User Agent and Page Name)

- Added test web pages and SQL DB viewer to allow testing of the code and viewing of the information stored in the database.

v1.00
------
(Records IP Address, Date/Time Visited, Server Host Name, User Agent and Page Name)

- Implemented basic tracker function that records five bits of information about a user whenever they visit a page. It then stores it into an SQL database
- Stores IP address, user agent, user host name, date/time visited and page visited.
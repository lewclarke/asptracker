This is a simple web plugin developed using C# and ASP .NET. It will track what pages a user has visted on the website and for how long. When a user signs up for a newsletter (providing their e-mail address) this information will then be stored along with their email.

Version 1 implementation of the tracker stores IP address, user agent, user host name, date/time visited and page visited into an SQL database defined in the asp_tracker code. An easier way to change the SQL settings will be implemented in a later version. All that is requried is a simple line of code at the top of a web page (HTML file) to use the web tracker. Everything else is done by the code in the tracker file. 

Script Code for HTML files:
===========================
<script language="javascript" src="asp_tracker/asp_tracker.aspx"></script>
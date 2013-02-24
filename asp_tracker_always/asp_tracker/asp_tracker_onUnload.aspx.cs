using System;
using System.IO;
using System.Web;
using System.Data.SqlClient;
using System.Net;

public partial class code : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext cont = HttpContext.Current;
        string dir = Path.GetDirectoryName(cont.Request.PhysicalPath);                      // Stores the path to the current directory that this file is stored in
        string[] lines = System.IO.File.ReadAllLines(dir + "/config.cfg");                  // Loads settings from config.cfg file into string array
        string sql_conn_string = lines[0].Substring(lines[0].IndexOf('=') + 1);             // First line equals connection string, removes any text before '=' in config file
        string sql_table_name = lines[1].Substring(lines[1].IndexOf('=') + 1);              // Second line equals table name, removes any text before '=' in config file

        string userAgent = cont.Request.UserAgent.ToString();                               // Users client     
        string visitedPage = Path.GetFileName(cont.Request.UrlReferrer.ToString());         // Page name code was called from
        string userIP = Request.ServerVariables["REMOTE_ADDR"];                             // Users external IP address  
        DateTime dateTime = DateTime.Now;                                                   // Date/time page was visisted 
        TimeSpan span = DateTime.Now.Subtract((DateTime)Session["loadTime"]);               // Compares current time to time page was loaded (stored in session state). Subtracting page load time from page unload time provides duration of time spent on page
        Session.Remove("loadTime");                                                         // Removes page load time from session state (no longer needed)

        SqlConnection sqlconn = new SqlConnection(sql_conn_string);                         // Creates a new SQL connection (connecting to connection string taken from config file)
        sqlconn.Open();                                                                     // Opens the SQL connection
        SqlCommand insert = new SqlCommand("INSERT INTO " + sql_table_name + "(ipAddress, userAgent, visitedPage, dateTime, duration) Values (@ipAddress, @userAgent, @visitedPage, @dateTime, @duration)", sqlconn);       // Insert statement for SQL table
        insert.Parameters.AddWithValue("@ipAddress", userIP);                               // The following five lines setup the parameters and their values for the insert statement above
        insert.Parameters.AddWithValue("@userAgent", userAgent);
        insert.Parameters.AddWithValue("@visitedPage", visitedPage);
        insert.Parameters.AddWithValue("@dateTime", dateTime);
        insert.Parameters.AddWithValue("@duration", span);
        insert.ExecuteNonQuery();                                                           // Executes the INSERT command
        sqlconn.Close();                                                                    // Closes the SQL connection
    }
}
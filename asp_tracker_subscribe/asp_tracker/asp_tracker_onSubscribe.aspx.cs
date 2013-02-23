using System;
using System.IO;
using System.Web;
using System.Data.SqlClient;
using System.Collections;
using System.Net;

public partial class code : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext cont = HttpContext.Current;
        string dir = Path.GetDirectoryName(cont.Request.PhysicalPath);                      // Stores the path to the current directory that this file is stored in
        string[] lines = System.IO.File.ReadAllLines(dir + "/config.cfg");                  // Loads settings from config.cfg file into string array
        string sql_conn_string = lines[0].Substring(lines[0].IndexOf('=') + 1);             // First line equals connection string, removes any text before '=' in config file
        string sql_table_name_userinfo = lines[1].Substring(lines[1].IndexOf('=') + 1);     // Second line equals subscriber table name (holds email and name), removes any text before '=' in config file
        string sql_table_name_pageinfo = lines[2].Substring(lines[2].IndexOf('=') + 1);     // Third line equals page info table name (holds visited pages, ip address etc.), removes any text before '=' in config file
        string email_subscribe_error = lines[3].Substring(lines[3].IndexOf('=') + 1);       // Fourth line holds the message that is shown when email is already registered in database
        string email_subscribe_success = lines[4].Substring(lines[4].IndexOf('=') + 1);     // Fifth line holds the message that is shown when user is registered succesfully
        string blank_input_error = lines[5].Substring(lines[5].IndexOf('=') + 1);           // Sixth line holds the message that is shown when either one of the text fields is blank

        string subscriberName = Request.QueryString["name"].ToString();                     // Subscriber name entered on sign up page passed via query string
        string subscriberEmail = Request.QueryString["email"].ToString();                   // Subscriber email entered on sign up page passed via query string

        if (String.IsNullOrEmpty(subscriberName) == true || String.IsNullOrEmpty(subscriberEmail) == true)          // Checks to see if both text fields have data
        {
            lbl_result.Text = blank_input_error;                 // If either text field is blank process does not continue and error message (defined in config.cfg) is shown
        }
        else
        {
            SqlConnection sqlconn = new SqlConnection(sql_conn_string);                         // Creates a new SQL connection (connecting to connection string taken from config file)
            sqlconn.Open();

            SqlCommand checkExist = new SqlCommand("SELECT ID FROM " + sql_table_name_userinfo + " WHERE email = '" + subscriberEmail + "'", sqlconn);      // Runs a query on the SQL server to get the ID of any record associated with the new emai. If ID is not zero it means email is already registered and therefore the user has already signed up
            int emailExist = Convert.ToInt32(checkExist.ExecuteScalar());                       // If not 0 email is already registered on system

            if (emailExist != 0)            // If not 0 email is already registered on system and registration is aborted.
            {
                lbl_result.Text = email_subscribe_error;            // If email already exists in database process does not continue and error message (defined in config.cfg) is shown
            }
            else
            {
                int count = (int)Session["pagesVisitedCount"];                                  // count holds how many pages the user has visited before signing up
                ArrayList userIP_log = (ArrayList)Session["userIP_log"];                        // The following five lines move the respective arraylists from session state to local arraylists
                ArrayList userAgent_log = (ArrayList)Session["userAgent_log"];
                ArrayList visitedPage_log = (ArrayList)Session["visitedPage_log"];
                ArrayList visitDateTime_log = (ArrayList)Session["visitDateTime_log"];
                ArrayList visitDuration_log = (ArrayList)Session["visitDuration_log"];
                
                SqlCommand insert_userInfo = new SqlCommand("INSERT INTO " + sql_table_name_userinfo + " VALUES (@name, @email)", sqlconn);         // Inserts subscriber info into subscriber info table
                insert_userInfo.Parameters.AddWithValue("@name", subscriberName);                   // Subscriber Name
                insert_userInfo.Parameters.AddWithValue("@email", subscriberEmail);                 // Subscriber e-Mail
                insert_userInfo.ExecuteNonQuery();                                                  // Runs the SQL query

                SqlCommand getSubscriberID = new SqlCommand("SELECT ID FROM " + sql_table_name_userinfo + " WHERE email = '" + subscriberEmail + "'", sqlconn);     // Gets ID of subscriber info that was just inserted in the previous query (above this line)
                int subscriberID = Convert.ToInt32(getSubscriberID.ExecuteScalar());            // Stores this ID as an integer to be used by future SQL querys

                for (int i = 0; i < count; i++)         // Inserts all pages visited into the SQL DB. Runs through the arraylists until loop has reached count (total pages visited)
                {
                    SqlCommand insert_pageinfo = new SqlCommand("INSERT INTO " + sql_table_name_pageinfo + "(subscriberID, ipAddress, userAgent, visitedPage, dateTime, duration) VALUES (@subscriberID, @ipAddress, @userAgent, @visitedPage, @visitDateTime, @visitDuration)", sqlconn);       // Insert query
                    insert_pageinfo.Parameters.AddWithValue("@subscriberID", subscriberID);                 // Subscriber ID (foreign key, ID (primary key) from subscriber info table)
                    insert_pageinfo.Parameters.AddWithValue("@ipAddress", userIP_log[i]);                   // IP Address
                    insert_pageinfo.Parameters.AddWithValue("@userAgent", userAgent_log[i]);                // User Agent (Web Browser)
                    insert_pageinfo.Parameters.AddWithValue("@visitedPage", visitedPage_log[i]);            // Page Visited
                    insert_pageinfo.Parameters.AddWithValue("@visitDateTime", visitDateTime_log[i]);        // Time user opened page
                    insert_pageinfo.Parameters.AddWithValue("@visitDuration", visitDuration_log[i]);        // Time user spent on pagr
                    insert_pageinfo.ExecuteNonQuery();
                }

                sqlconn.Close();
                Session.Clear();                                // Clears the session state now we have finished with the data
                lbl_result.Text = email_subscribe_success;
            }
        }
    }
}
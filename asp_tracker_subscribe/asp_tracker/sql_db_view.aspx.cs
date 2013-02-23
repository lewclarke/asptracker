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
        string dir = Path.GetDirectoryName(cont.Request.PhysicalPath);                          // Stores the path to the current directory that this file is stored in
        string[] lines = System.IO.File.ReadAllLines(dir + "/config.cfg");                      // Loads settings from config.cfg file into string array
        string sql_conn_string = lines[0].Substring(lines[0].IndexOf('=') + 1);                 // First line equals connection string, removes any text before '=' in config file
        string sql_table_name_subscriber = lines[1].Substring(lines[1].IndexOf('=') + 1);       // Second line equals table name (subscriber data table), removes any text before '=' in config file
        string sql_table_name_pagesvisited = lines[2].Substring(lines[2].IndexOf('=') + 1);     // Third line equals table name (pages visited data table), removes any text before '=' in config file

        sql_table_name_subscriber = sql_table_name_subscriber.Replace(" ", null);                             // Due to errors occuring if spaces/tabs were detected in the config file with the table name
        sql_table_name_subscriber = sql_table_name_subscriber.Replace("\t", null);                            // these two lines of code remove any tabs or spaces to prevent such errors occuring.        
        SqlDataSource_subscriber.ConnectionString = sql_conn_string;                                          // Sets SQL Data Source connection strins
        SqlDataSource_subscriber.SelectCommand = "SELECT * FROM [" + sql_table_name_subscriber + "]";         // Sets SQL Data Sources table name

        sql_table_name_pagesvisited = sql_table_name_pagesvisited.Replace(" ", null);                             // Due to errors occuring if spaces/tabs were detected in the config file with the table name
        sql_table_name_pagesvisited = sql_table_name_pagesvisited.Replace("\t", null);                            // these two lines of code remove any tabs or spaces to prevent such errors occuring.        
        SqlDataSource_pagesvisited.ConnectionString = sql_conn_string;                                          // Sets SQL Data Source connection strins
        SqlDataSource_pagesvisited.SelectCommand = "SELECT * FROM [" + sql_table_name_pagesvisited + "] ORDER BY ID";         // Sets SQL Data Sources table name
    }
}
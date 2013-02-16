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
        string dir = Path.GetDirectoryName(cont.Request.PhysicalPath);                  // Stores the path to the current directory that this file is stored in
        string[] lines = System.IO.File.ReadAllLines(dir + "/config.cfg");              // Loads settings from config.cfg file into string array
        string sql_conn_string = lines[0].Substring(lines[0].IndexOf('=') + 1);         // First line equals connection string, removes any text before '=' in config file
        string sql_table_name = lines[1].Substring(lines[1].IndexOf('=') + 1);          // Second line equals table name, removes any text before '=' in config file
        
        sql_table_name = sql_table_name.Replace(" ", null);                             // Due to errors occuring if spaces/tabs were detected in the config file with the table name
        sql_table_name = sql_table_name.Replace("\t", null);                            // these two lines of code remove any tabs or spaces to prevent such errors occuring.
        
        SqlDataSource.ConnectionString = sql_conn_string;                               // Sets SQL Data Source connection strins
        SqlDataSource.SelectCommand = "SELECT * FROM [" + sql_table_name + "]";         // Sets SQL Data Sources table name
    }
}
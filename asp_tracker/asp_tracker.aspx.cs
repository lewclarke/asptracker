using System;
using System.IO;
using System.Web;
using System.Data.SqlClient;
using System.Net;

public partial class code : System.Web.UI.Page
{
    string dir = AppDomain.CurrentDomain.BaseDirectory.ToString();           // Gets the base directory       
    
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext cont = HttpContext.Current;
        
        string userAgent = cont.Request.UserAgent.ToString();                               // Users client     
        string visitedPage = Path.GetFileName(cont.Request.UrlReferrer.ToString());         // Page name code was called from
        string dateTime = DateTime.Now.ToString();                                          // Date/time page was visisted
        string hostName = System.Net.Dns.GetHostName();                                     // Users computer host name
        string userIP = Request.ServerVariables["REMOTE_ADDR"];                             // Users external IP address       
        
        
        SqlConnection sqlconn = new SqlConnection("workstation id=aspdata.mssql.somee.com;packet size=4096;user id=asptrack_SQLLogin_1;pwd=vnf7hvwlth;data source=aspdata.mssql.somee.com;persist security info=False;initial catalog=aspdata");
        sqlconn.Open();
        SqlCommand insert = new SqlCommand("INSERT INTO trackerRecords (ipAddress, hostName, userAgent, visitedPage, dateTime) Values (@ipAddress, @hostName, @userAgent, @visitedPage, @dateTime)", sqlconn);
        insert.Parameters.AddWithValue("@ipAddress", userIP);
        insert.Parameters.AddWithValue("@hostName", hostName);
        insert.Parameters.AddWithValue("@userAgent", userAgent);
        insert.Parameters.AddWithValue("@visitedPage", visitedPage);
        insert.Parameters.AddWithValue("@dateTime", dateTime);
        insert.ExecuteNonQuery();
        sqlconn.Close();
    }
}
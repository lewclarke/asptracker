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

        string userAgent = cont.Request.UserAgent.ToString();                               // Users client     
        string visitedPage = Path.GetFileName(cont.Request.UrlReferrer.ToString());         // Page name code was called from
        string userIP = Request.ServerVariables["REMOTE_ADDR"];                             // Users external IP address  
        DateTime visitDateTime = DateTime.Now;                                              // Date/time page was visited (opened)
        TimeSpan visitDuration = DateTime.Now.Subtract((DateTime)Session["loadTime"]);      // Compares current time to time page was loaded (stored in session state). Subtracting page load time from page unload time provides duration of time spent on page
        Session.Remove("loadTime");                                                         // Removes page load time from session state (no longer needed)

        if (Session["pagesVisitedCount"] == null)                                           // Checks to see if this is the first page opened (session state variables dont exist)
        {
            Session["pagesVisitedCount"] = 0;                                               // The following six lines setup the session arraylist used by the tracker
            Session["userIP_log"] = new ArrayList();
            Session["userAgent_log"] = new ArrayList();
            Session["visitedPage_log"] = new ArrayList();
            Session["visitDateTime_log"] = new ArrayList();
            Session["visitDuration_log"] = new ArrayList();
        }
            
        int count = (int)Session["pagesVisitedCount"] + 1;                                  // The following six lines move the session state arraylists into local arraylists
        ArrayList userIP_log = (ArrayList)Session["userIP_log"];
        ArrayList userAgent_log = (ArrayList)Session["userAgent_log"];
        ArrayList visitedPage_log = (ArrayList)Session["visitedPage_log"];
        ArrayList visitDateTime_log = (ArrayList)Session["visitDateTime_log"];
        ArrayList visitDuration_log = (ArrayList)Session["visitDuration_log"];

        userIP_log.Add(userIP);                                                             // The following five lines add this pages data to their respective arraylists
        userAgent_log.Add(userAgent);
        visitedPage_log.Add(visitedPage);
        visitDateTime_log.Add(visitDateTime);
        visitDuration_log.Add(visitDuration);

        Session["pagesVisitedCount"] = count;                                               // The following six lines move the updated arraylists back into the session state
        Session["userIP_log"] = userIP_log;
        Session["userAgent_log"] = userAgent_log;
        Session["visitedPage_log"] = visitedPage_log;
        Session["visitDateTime_log"] = visitDateTime_log;
        Session["visitDuration_log"] = visitDuration_log;
    }
}
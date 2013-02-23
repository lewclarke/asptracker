<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sql_db_view.aspx.cs" Inherits="code" %>

<head>
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
        }
    </style>
</head>

<form id="form1" runat="server">
    <span class="auto-style1"><strong>SQL Database Information</strong></span><br />
    <br />
    Below is all of the information pulled from the SQL database.
    <br />
    <br />
    The first table shows the subscriber information (name and email).<br />
    <br />
    The second table shows each page a subscriber visited. The subscriber ID column in this table matches to an ID in the first table (subscriber information) thus linking the records.<br />
    <br />
    In realworld implementation of this plugin SQL statements could be used to select only data relating to a single subscriber. In order to demenstrate the data stored in the tables all data is shown here.<br />
    <br />
    <span class="auto-style1"><strong>Subscriber Table<br />
    </strong></span>
    <br />
    <asp:SqlDataSource ID="SqlDataSource_subscriber" runat="server"></asp:SqlDataSource>
    <asp:GridView ID="GridView_subscriber" runat="server" AutoGenerateColumns="True" DataKeyNames="ID" DataSourceID="SqlDataSource_subscriber"></asp:GridView>

    <strong>
    <br class="auto-style1" />
    <span class="auto-style1">Pages Visited Table<br />
    <br />
    </span></strong>

    <asp:SqlDataSource ID="SqlDataSource_pagesvisited" runat="server"></asp:SqlDataSource>
    <asp:GridView ID="GridView_pagesvisited" runat="server" AutoGenerateColumns="True" DataKeyNames="ID" DataSourceID="SqlDataSource_pagesvisited"></asp:GridView>
</form>


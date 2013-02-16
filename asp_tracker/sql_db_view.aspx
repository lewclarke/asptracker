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
    Below is all of the information pulled from the SQL database. This will show you the information for every visit to the test pages.
    <br />
    <br />
    <asp:SqlDataSource ID="SqlDataSource" runat="server" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
    <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="True" DataKeyNames="ID" DataSourceID="SqlDataSource">
    </asp:GridView>
</form>


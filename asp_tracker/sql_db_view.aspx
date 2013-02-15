<%@ Page Language="C#" AutoEventWireup="true"%>

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
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" connectionString="workstation id=aspdata.mssql.somee.com;packet size=4096;user id=asptrack_SQLLogin_1;pwd=vnf7hvwlth;data source=aspdata.mssql.somee.com;persist security info=False;initial catalog=aspdata" SelectCommand="SELECT * FROM [trackerRecords]" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="ipAddress" HeaderText="ipAddress" SortExpression="ipAddress" />
            <asp:BoundField DataField="hostName" HeaderText="hostName" SortExpression="hostName" />
            <asp:BoundField DataField="userAgent" HeaderText="userAgent" SortExpression="userAgent" />
            <asp:BoundField DataField="visitedPage" HeaderText="visitedPage" SortExpression="visitedPage" />
            <asp:BoundField DataField="dateTime" HeaderText="dateTime" SortExpression="dateTime" />
        </Columns>
    </asp:GridView>
</form>


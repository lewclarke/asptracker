<%@ Page Language="C#" AutoEventWireup="true" CodeFile="asp_tracker_onSubscribe.aspx.cs" Inherits="code" %>


<head>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
    </style>
</head>


<form id="form1" runat="server">
    <div class="auto-style1">
    <asp:Label ID="lbl_result" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
    <asp:Button ID="close_window" runat="server" OnClientClick="window.close()" Text="Close Window" />
    </div>
</form>




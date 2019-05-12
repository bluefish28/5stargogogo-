<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml"> 
<head runat="server"> 
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="stylesheet/StyleSheet.css" rel="stylesheet" type="text/css" />
</head> 
<body>
    <div align="center">
        <form id="form1" runat="server" class="box">
            <h1 class="h1">Login</h1>
            <p>
               <asp:TextBox ID="tbusername" runat="server" CssClass="TextBox1" placeholder="Username"></asp:TextBox>
            </p>
            <p>
                <asp:TextBox ID="tbpsw" runat="server" TextMode="Password" CssClass="TextBox1"  placeholder="Password"></asp:TextBox>
            </p>
            <p>
                <asp:Button ID="Login" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="Button1" />
            </p>
            <p>
                <asp:Button ID="Reset" runat="server" Text="Reset" OnClick="Reset_Click" CssClass="Button2"/>
            </p>
        </form>
    </div>
</body>
</html>

<!--*****************************************************************************
Project:		Assignment 4: Hi-Lo Asp.net

Student Names:	Jordan Thompson 

File Name:		WinPae.aspx.

Date:			November 18, 2013

Description:	The HTML code that generates the Hi-Lo Win Page.                 
*****************************************************************************-->

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WinPage.aspx.cs" Inherits="HiLo_ASPNET.Pages.WinPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hi-Lo Game</title>
    <center><h1>Congratulations, you Won Hi-Lo!</h1></center>
</head>
<body id="Body" runat="server">
    <form id="form1" runat="server">
    <div align ="center">

        <!--Win Display-->
        <asp:Label ID="numGuessCount" runat="server" Font-Names="Courier New" ForeColor="#000099"></asp:Label>
        <br />
        <asp:Label ID="challenge" runat="server" Font-Names="Courier New" ForeColor="#000099"></asp:Label>
        <br />

        <!--Play Again Button-->
        <asp:Button ID="playAgain" runat="server" OnClick="playAgain_Click" Text="Play Again?" Width="300px" />
    </div>
    </form>
</body>
</html>

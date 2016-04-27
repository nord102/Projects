<!--*****************************************************************************
Project:		Assignment 4: Hi-Lo Asp.net

Student Names:	Jordan Thompson 

File Name:		GamePage.aspx.

Date:			November 18, 2013

Description:	The HTML code that generates the Hi-Lo Game Page.                 
*****************************************************************************-->

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GamePage.aspx.cs" Inherits="HiLo_ASPNET.Pages.GamePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hi-Lo Game</title>
    <center><h1>Welcome to Jordan's Hi-Lo Game: Let's Play</h1></center>
</head>
<body>
    <form id="form1" runat="server">
    <div align ="center">

        <!--Game Display -->
        <asp:Label ID="userName" runat="server" Font-Names="Courier New" ForeColor="#000099"></asp:Label>
        <br />  
        <asp:Label ID="range" runat="server" Font-Names="Courier New" ForeColor="#000099"></asp:Label>
        <asp:Label ID="numGuess" runat="server" Font-Names="Courier New" ForeColor="#000099"></asp:Label>
        <br />
        <asp:Label ID="guessStatus" runat="server" Font-Names="Courier New" ForeColor="#000099"></asp:Label>
        <br />

        <!--User Guess Elements -->
        <asp:Label ID="askGuess" runat="server" Font-Names="Courier New" ForeColor="#000099"></asp:Label>
        <asp:TextBox ID="userGuess" runat="server" autofocus></asp:TextBox>
        <!--User Guess Validation--> 
        <asp:RequiredFieldValidator ID="requiredUserGuess" ControlToValidate="userGuess" runat="server" ErrorMessage="Must Provide a Guess"></asp:RequiredFieldValidator>
        <br />
         <asp:RangeValidator ID="validateUserGuess" MinimumValue="1" MaximumValue="2147483647" ControlToValidate="userGuess" Type="Integer"
             runat="server" ErrorMessage="Invalid Guess: Must contain a number between 1-2147483647"></asp:RangeValidator>
        <br />

        <!--Guess Button--> 
        <asp:Button ID="sendUserGuess" runat="server" OnClick="sendUserGuess_Click" Text="Guess" Width="300px" />   
    </div>
    </form>
</body>
</html>

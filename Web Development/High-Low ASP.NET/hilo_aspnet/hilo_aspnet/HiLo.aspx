<!--*****************************************************************************
Project:		Assignment 4: Hi-Lo Asp.net

Student Names:	Jordan Thompson 

File Name:		HiLo.aspx.

Date:			November 18, 2013

Description:	The HTML code that generates the Hi-Lo Set up Page.                 
*****************************************************************************-->

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HiLo.aspx.cs" Inherits="HiLo_ASPNET.HiLo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hi-Lo Game</title>
    <center><h1>Welcome to Jordan's Hi-Lo Game: Set Up</h1></center>
</head>
<body>
    <form id="setupForm" runat="server">
    <div align ="center" >

        <!--Username Elements-->        
        <asp:Label ID="askUserName" runat="server" Font-Names="Courier New" ForeColor="#000099"></asp:Label>
        <asp:TextBox ID="userName" runat="server" autofocus></asp:TextBox> 
        <!--Username Validation -->
        <asp:RequiredFieldValidator ID="requiredUserName" ControlToValidate="userName" runat="server" 
            ErrorMessage="Must Provide a Username"></asp:RequiredFieldValidator>    
        <br />    
        <asp:RegularExpressionValidator ID="validateUserName" runat="server" ValidationExpression="^\b[a-zA-Z]{1,20}$"
             ControlToValidate="userName" ErrorMessage="Invalid Username: Must only contain letters"></asp:RegularExpressionValidator>        
        <br />
        
        <!--Max range Elements-->       
        <asp:Label ID="askMaxRange" runat="server" Font-Names="Courier New" ForeColor="#000099"></asp:Label>
        <asp:TextBox ID="maxRange" runat="server"></asp:TextBox>
        <!--Max range Validation -->
        <asp:RequiredFieldValidator ID="requiredMaxRange" ControlToValidate="maxRange" runat="server" 
            ErrorMessage="Must Provide a Max Range"></asp:RequiredFieldValidator>  
        <br /> 
        <asp:RangeValidator ID="validateMaxRange" MinimumValue="1" MaximumValue="2147483647" ControlToValidate="maxRange" Type="Integer"
             runat="server" ErrorMessage="Invalid Range: Must contain a number between 1-2147483647"></asp:RangeValidator>            
        <br />
        
        <!--Submit Button-->    
        <asp:Button ID="sendUserInput" runat="server" OnClick="sendUserInput_Click" Text="Submit" Width="300px" />   
    </div>
    </form>
</body>
</html>

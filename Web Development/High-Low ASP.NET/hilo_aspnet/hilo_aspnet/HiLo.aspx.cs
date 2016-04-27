/*****************************************************************************
Project:		Assignment 4: Hi-Lo Asp.net

Student Names:	Jordan Thompson 

File Name:		HiLo.aspx.cs

Date:			November 18, 2013

Description:	The purpose of the file is to model a generic Hi-Lo Game where
                a user attempts to guess a random number in a specified range. 
*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace HiLo_ASPNET
{
    public partial class HiLo : System.Web.UI.Page
    {
        /*****************************************************************************
        Function:		Page_Load
        
        Description:	Initializes the pages labels.
        *****************************************************************************/
        protected void Page_Load(object sender, EventArgs e)
        {
            askUserName.Text = "Enter your username: ";
            askMaxRange.Text = "Enter a max range: ";
        }

        /*****************************************************************************
        Function:		sendUserInput_Click
        
        Description:	Assigns the users name and maximum range to the viewstate
                        so the game page can access them.
        *****************************************************************************/
        protected void sendUserInput_Click(object sender, EventArgs e)
        {
            ViewState["userName"] = userName.Text;
            ViewState["maxRange"] = maxRange.Text;
            Server.Transfer("Pages/GamePage.aspx");            
        }

        /*****************************************************************************
        Function:		ReturnViewState
        
        Description:	Retrieves the viewstate of the web form.
        
        Returns:        ViewState:  The state of the web form.
        *****************************************************************************/
        public StateBag ReturnViewState()
        {
            return ViewState;
        }
    }
}
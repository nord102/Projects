/*****************************************************************************
Project:		Assignment 4: Hi-Lo Asp.net

Student Names:	Jordan Thompson 

File Name:		WinPage.aspx.cs

Date:			November 18, 2013

Description:	The purpose of the file is to model a congratulations page for
                winning the Hi Lo Game.
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Reflection; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HiLo_ASPNET.Pages
{
    public partial class WinPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Retrieves Previous pages ViewState information
            if (PreviousPage != null && PreviousPageViewState != null)
            {
                ViewState["numGuess"] = PreviousPageViewState["numGuess"];                
            }
            Body.Attributes["bgcolor"] = "Orange";
            numGuessCount.Text = "It took you " + ViewState["numGuess"] + " guess(es) to win.";
            challenge.Text = "Think you can do better?";
        }

        /*****************************************************************************
        Function:		PreviousPageViewState
        
        Description:	Retrieves the ViewState from the previous page.
        *****************************************************************************/
        private StateBag PreviousPageViewState
        {
            get
            {
                StateBag returnValue = null;
                if (Page.PreviousPage != null)
                {
                    Object objPreviousPage = (Object)PreviousPage;
                    MethodInfo objMethod = objPreviousPage.GetType().GetMethod("ReturnViewState");
                    return (StateBag)objMethod.Invoke(objPreviousPage, null);
                }
                return returnValue;
            }
        }

        /*****************************************************************************
        Function:		playAgain_Click
        
        Description:    Loads the Hi Lo Set up page for the user to re-play.
        *****************************************************************************/
        protected void playAgain_Click(object sender, EventArgs e)
        {
            Server.Transfer("..\\HiLo.aspx");             
        }
    }
}
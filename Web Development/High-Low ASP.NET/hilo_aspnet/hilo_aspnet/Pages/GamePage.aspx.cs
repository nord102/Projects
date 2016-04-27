/*****************************************************************************
Project:		Assignment 4: Hi-Lo Asp.net

Student Names:	Jordan Thompson 

File Name:		GamePage.aspx.cs

Date:			November 18, 2013

Description:	The purpose of the file is to model a basic Hi-Lo Game where
                a user attempts to guess a number in a specified range.
*****************************************************************************/

using System.Reflection; 
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HiLo_ASPNET.Pages
{
    public partial class GamePage : System.Web.UI.Page
    {
        /*****************************************************************************
        Function:		Page_Load
        
        Description:	Retrieves the previous pages ViewState information (username and
                        maximum range) and initializes the game when the page first loads                        
        *****************************************************************************/
        protected void Page_Load(object sender, EventArgs e)
        {
            //Retrieves Previous pages ViewState information
            if (PreviousPage != null && PreviousPageViewState != null)
            {
                ViewState["userName"] = PreviousPageViewState["userName"];
                ViewState["maxRange"] = PreviousPageViewState["maxRange"];                
            }

            //If the page loaded for the first time, initialize game variables
            if (IsPostBack == false)
            {
                ViewState["minRange"] = 1;
                ViewState["numGuess"] = 0;

                //Generate Random Number based off of users maximum range
                Random rnd = new Random();
                ViewState["randNum"] = rnd.Next(1, Convert.ToInt32(ViewState["maxRange"]) + 1);                

                //Initial Game State
                userName.Text = "Hello " + ViewState["userName"];
                range.Text = "Range: " + ViewState["minRange"] + " - " + ViewState["maxRange"];
                numGuess.Text = "Number of Guesses: " + ViewState["numGuess"].ToString();
                askGuess.Text = "Enter your Guess:";
            }    
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
        Function:		sendUserGuess_Click
        
        Description:	Retrieves the users guess and checks whether or not the guess
                        is within range and then checks if it is higher, lower, or the
                        same value of the random number.
        *****************************************************************************/
        protected void sendUserGuess_Click(object sender, EventArgs e)
        {
            ViewState["userGuess"] = userGuess.Text;

            //Gather information from ViewState variables
            int userGuessNum = Convert.ToInt32(ViewState["userGuess"]);
            int minRangeNum = Convert.ToInt32(ViewState["minRange"]);
            int maxRangeNum = Convert.ToInt32(ViewState["maxRange"]);
            int randomNum = Convert.ToInt32(ViewState["randNum"]);
            int numbGuess = Convert.ToInt32(ViewState["numGuess"]);

            numbGuess++;
            ViewState["numGuess"] = numbGuess;

            //Checks if user guess in within range
            if (userGuessNum >= minRangeNum && userGuessNum <= maxRangeNum)
            {
                //Checks whether the user guess is lower or higher than the random number
                if (userGuessNum > randomNum)
                {
                    maxRangeNum = userGuessNum;
                    guessStatus.Text = "Your Guess was Too High!";
                }
                else if (userGuessNum < randomNum)
                {
                    minRangeNum = userGuessNum;
                    guessStatus.Text = "Your Guess was Too Low!";
                }
                else
                {
                    Server.Transfer("WinPage.aspx");
                }
            }
            else
            {
                guessStatus.Text = "Guess Out of Range!";
            }

            //Return updated information to ViewState variables           
            ViewState["maxRange"] = maxRangeNum;
            ViewState["minRange"] = minRangeNum;

            //Update Game State
            userName.Text = "Hello " + ViewState["userName"];
            range.Text = "Range: " + ViewState["minRange"] + " - " + ViewState["maxRange"];
            numGuess.Text = "Number of Guesses: " + ViewState["numGuess"].ToString();
            askGuess.Text = "Enter your Guess:";
            userGuess.Text = "";           

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
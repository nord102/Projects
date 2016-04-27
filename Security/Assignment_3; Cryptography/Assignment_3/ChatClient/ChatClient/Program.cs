﻿/*****************************************************************************
 * Project:		    ChatClient
 * 
 * Student Names:	Jordan Thompson and Kyle Thomson
 * 
 * File Name:		Program.cs
 *
 * Date:			March 19, 2016
 *
 * Description:	    The purpose of the file is to run the client application.
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ChatClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Client());
        }
    }
}
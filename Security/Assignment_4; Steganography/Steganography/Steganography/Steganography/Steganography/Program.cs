/*****************************************************************************
 * Project:		    Steganography
 * 
 * Student Names:	Jordan Thompson and Kyle Thomson
 * 
 * File Name:		Program.cs
 *
 * Date:			March 31, 2016
 *
 * Description:	    The purpose of the file is to run the application.
 *****************************************************************************/
using System;
using System.Windows.Forms;

namespace Steganography
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
            Application.Run(new Steganography());
        }
    }
}

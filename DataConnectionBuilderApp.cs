/* DataConnectionBuilderApp.cs - Implementation of DataConnectionBuilderApp class, which encapsulates this application.
 *
 * Copyright (c) 2017 Jeffrey Paul Bourdier
 *
 * Licensed under the MIT License.  This file may be used only in compliance with this License.
 * Software distributed under this License is provided "AS IS", WITHOUT WARRANTY OF ANY KIND.
 * For more information, see the accompanying License file or the following URL:
 *
 *   https://opensource.org/licenses/MIT
 */


/* STAThread */
using System;

/* Application */
using System.Windows;


namespace JB
{
    /// <summary>Encapsulates the Data Connection Builder application. </summary>
    public class DataConnectionBuilderApp : Application
    {
        /***********
         * Methods *
         ***********/

        #region Private Methods

        /// <summary>The main entry point for the application.</summary>
        /// <remarks>
        /// Without the STAThread attribute, an exception is thrown when trying to open a file dialog in debug mode.
        /// </remarks>
        [STAThread]
        private static void Main()
        {
            DataConnectionBuilderApp app;
            DataConnectionWindow window;

            /* Start the application and open a data connection window. */
            app = new DataConnectionBuilderApp();
            window = new DataConnectionWindow();
            app.Run(window);
        }

        #endregion
    }
}

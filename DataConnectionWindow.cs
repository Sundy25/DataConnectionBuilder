/* DataConnectionWindow.cs - Implementation of DataConnectionWindow class, which makes up this application's user interface.
 *
 * Copyright (c) 2017 Jeffrey Paul Bourdier
 *
 * Licensed under the MIT License.  This file may be used only in compliance with this License.
 * Software distributed under this License is provided "AS IS", WITHOUT WARRANTY OF ANY KIND.
 * For more information, see the accompanying License file or the following URL:
 *
 *   https://opensource.org/licenses/MIT
 */


/* Uri, EventArgs */
using System;

/* CancelEventArgs */
using System.ComponentModel;

/* Thickness, RoutedEventArgs, TextWrapping */
using System.Windows;

/* ComboBox, TextBox, ScrollBarVisibility, CheckBox, DockPanel, Dock */
using System.Windows.Controls;

/* BitmapFrame */
using System.Windows.Media.Imaging;

/* Settings */
using JB.Properties;


namespace JB
{
    /// <summary>Represents a window that makes up the user interface for the Data Connection Builder application.</summary>
    public class DataConnectionWindow : AppWindow
    {
        /****************
         * Constructors *
         ****************/

        #region Public Constructors

        /// <summary>Initializes a DataConnectionWindow object.</summary>
        public DataConnectionWindow()
        {
            DockPanel panel;
            Uri uri;

            /* Initialize the data connection control. */
            this.DataConnectionControl = new DataConnectionControl();
            this.DataConnectionControl.Margin = new Thickness(UI.TripleSpace, UI.UnitSpace, UI.TripleSpace, UI.UnitSpace);
            this.DataConnectionControl.ControlCountChanged += this.DataConnectionControl_ControlCountChanged;
            this.DataConnectionControl.ConnectionStringChanged += this.DataConnectionControl_ConnectionStringChanged;

            /* Initialize the Connection String label. */
            this.ConnectionStringLabel = new StandardLabel(Properties.Resources.ConnectionString, true);
            this.ConnectionStringLabel.Margin = new Thickness(UI.TripleSpace, UI.UnitSpace, UI.TripleSpace, UI.HalfSpace);

            /* Initialize the Encrypted Connection String text box. */
            this.ConnectionStringTextBox = new TextBox();
            this.ConnectionStringTextBox.IsReadOnly = true;
            this.ConnectionStringTextBox.Margin = new Thickness(UI.TripleSpace, UI.HalfSpace, UI.TripleSpace, UI.HalfSpace);
            this.ConnectionStringTextBox.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.ConnectionStringTextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.ConnectionStringLabel.Target = this.ConnectionStringTextBox;

            /* Initialize the Wrap check box. */
            this.WrapCheckBox = new CheckBox();
            this.WrapCheckBox.Content = Properties.Resources.Wrap;
            this.WrapCheckBox.Checked += this.WrapCheckBox_Checked;
            this.WrapCheckBox.Unchecked += this.WrapCheckBox_Checked;
            this.WrapCheckBox.Margin = new Thickness(UI.TripleSpace, UI.HalfSpace, UI.TripleSpace, UI.TripleSpace);

            /* Restore the Wrap option. */
            this.WrapCheckBox.IsChecked = Settings.Default.Wrap;

            this.SetTabIndexes();

            /* Build out the main subpanel. */
            DockPanel.SetDock(this.DataConnectionControl, Dock.Top);
            DockPanel.SetDock(this.ConnectionStringLabel, Dock.Top);
            DockPanel.SetDock(this.WrapCheckBox, Dock.Bottom);
            panel = new DockPanel();
            panel.Children.Add(this.DataConnectionControl);
            panel.Children.Add(this.ConnectionStringLabel);
            panel.Children.Add(this.WrapCheckBox);
            panel.Children.Add(this.ConnectionStringTextBox);
            this.SetMainSubpanel(panel);

            /* Initialize the window object.  (Note: "Build Action" property must be set to "Resource") */
            uri = ResourceFile.CreateUri(false, "DataConnectionBuilder.ico");
            this.Icon = BitmapFrame.Create(uri);
            this.Closing += this.DataConnectionWindow_Closing;
        }

        #endregion

        /**********
         * Fields *
         **********/

        #region Private Fields

        private DataConnectionControl DataConnectionControl;
        private StandardLabel ConnectionStringLabel;
        private TextBox ConnectionStringTextBox;
        private CheckBox WrapCheckBox;

        #endregion

        /***********
         * Methods *
         ***********/

        #region Protected Methods

        /// <summary>Adjusts the tab indexes of this window's controls as necessary to account for the header.</summary>
        protected override void AdjustTabIndexes()
        {
            base.AdjustTabIndexes();

            /* Adjust tab indexes as necessary. */
            this.DataConnectionControl.TabIndexOffset = this.HeaderControlCount;
            this.SetTabIndexes();
        }

        #endregion

        #region Private Methods

        #region Event Handlers

        private void DataConnectionControl_ControlCountChanged(object sender, EventArgs e) { this.SetTabIndexes(); }

        private void DataConnectionControl_ConnectionStringChanged(object sender, EventArgs e)
        {
            /* Display connection string. */
            this.ConnectionStringTextBox.Text = this.DataConnectionControl.ConnectionString;
        }

        private void WrapCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.ConnectionStringTextBox.TextWrapping = (bool)this.WrapCheckBox.IsChecked ?
                TextWrapping.Wrap : TextWrapping.NoWrap;
        }

        private void DataConnectionWindow_Closing(object sender, CancelEventArgs e)
        {
            /* Save window settings. */
            Settings.Default.Wrap = (bool)this.WrapCheckBox.IsChecked;
            Settings.Default.Save();
        }

        #endregion

        private void SetTabIndexes()
        {
            int i;

            /* Determine our starting index and set tab indexes. */
            i = this.DataConnectionControl.TabIndexOffset + this.DataConnectionControl.ControlCount;
            this.ConnectionStringLabel.TabIndex = ++i;
            this.ConnectionStringTextBox.TabIndex = ++i;
            this.WrapCheckBox.TabIndex = ++i;
        }

        #endregion
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace materialDesignTesting
{
    /// <summary>
    /// Interaction logic for UserManualStrategyView.xaml
    /// </summary>
    public partial class UserManualStrategyView : UserControl
    {
        public UserManualStrategyView()
        {
            InitializeComponent();
            //Enqueue a message to inform the user of successful values input.
            //unfortunetly its a fake message
            MainWindow.Snackbar.MessageQueue.Enqueue("Vector Saved Successfuly");
        }
    }
}

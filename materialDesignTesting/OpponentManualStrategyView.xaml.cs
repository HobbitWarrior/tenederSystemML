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
using System.Windows.Threading;

namespace materialDesignTesting
{
    /// <summary>
    /// Interaction logic for OpponentManualStrategyView.xaml
    /// </summary>
    public partial class OpponentManualStrategyView : UserControl
    {
        public OpponentManualStrategyView()
        {
            InitializeComponent();
            //Enqueue a message to inform the user of successful values input.
            //unfortunetly its a fake message
            MainWindow.Snackbar.MessageQueue.Enqueue("Vector Saved Successfuly");
        }
    }
}

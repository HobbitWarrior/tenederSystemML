﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace materialDesignTesting
{
    /*TODO: create the connection between the file ocntents loading and viewsmediator appropriate properites*/

    /// <summary>
    /// In this class give the option to load Opponent's strategy
    /// </summary>
    class OpponentLoadStrategyViewModel : INotifyPropertyChanged 
    {
        public OpponentLoadStrategyViewModel()
        {

            canExecute = true;
        }

        private bool canExecute;
        private ICommand clickOnBrowseButton;
        public ICommand ClickOnBrowseButton
        {
            get
            {
                return clickOnBrowseButton ?? (clickOnBrowseButton = new CommandHandler(() => openDialog(), canExecute));
            }
        }


        private String fileName = "[PlaceHolder]";

        public event PropertyChangedEventHandler PropertyChanged;

        public String FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                RaisePropertyChanged();
            }
        }


        ///<summary>
        ///The following method is handling the opening of a new dialog event
        ///</summary>
        public void openDialog()
        {
            Console.WriteLine("Just called a button click event!!!! Yay! :)");
            try
            {

                //Open dialog
                // Create OpenFileDialog
                Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

                // Launch OpenFileDialog by calling ShowDialog method
                Nullable<bool> result = openFileDlg.ShowDialog();
                // Get the selected file name and display in a TextBox.S
                // Load content of file in a TextBlock
                if (result == true)
                {
                    FileName = openFileDlg.FileName;
                    //this method will read all the contents of the provided file
                    //FileName = System.IO.File.ReadAllText(openFileDlg.FileName);
                    Console.WriteLine("the opendialog.filename:  {0}", FileName);
                    //A temp method, returns a 1000 dimensional Vector
                    //genetrateRandomVector(1000);
                    vectorFileHandler vfh = new vectorFileHandler();
                    List<double> probabilityFromFileList = vfh.readFromFile(FileName);
                    if (probabilityFromFileList != null)
                        ViewsMediator.Opponent = probabilityFromFileList;

                    //set expander to false
                    ViewsMediator.opponentExpander = "False";

                    Console.WriteLine("Loaded an opponent probability vector: ");
                    foreach (double value in ViewsMediator.Opponent)
                        Console.Write("{0} ", value);
                }
            }
            catch(Exception ex)
            {
                MainWindow.Snackbar.MessageQueue.Enqueue(String.Format("Sorry, couldn't the file, please try again.\n received the follwing message: {0}", ex.Message));
                System.Console.WriteLine(String.Format("Sorry, couldn't the file, please try again.\n received the follwing message: {0}", ex.Message));
            }
        }


        private void RaisePropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        ///<summary>
        ///A temp stub function that instantiates the two probability vectors
        ///</summary
        private void genetrateRandomVector(int n)
        {
            fileManager fl = new fileManager();
            ViewsMediator.User = fl.generateRandomVector(1000);
            ViewsMediator.Opponent = fl.generateRandomVector(1000);
        }
    }
}

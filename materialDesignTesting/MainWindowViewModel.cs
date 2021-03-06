﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace materialDesignTesting
{
    ///<sammary>
    ///This is the root class view model, all the windows of the application are include from it.A content control tag in the class,acts as a container of the user controls of the other windows in the application. 
    ///</sammary>

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private object currentViewModel;
        private string _nameOfCurrentViewModel = "main";
        public string nameOfCurrentViewModel
        {
            get
            {
                return _nameOfCurrentViewModel;
            }
            set
            {
                _nameOfCurrentViewModel = value;
                RaisePropertyChanged();
            }
        }
        public Dictionary<String,Object> viewModels;

        ///<sammary>
       ///The following properties control the Wizard's Expanders
       ///</sammary>
        public String _userExpander = ViewsMediator.userExpander;
        public String _opponentExpander = ViewsMediator.opponentExpander;
        public String _gameSettingsExpander = ViewsMediator.gameSettingsExpander;
        public String _calcualteExpander = ViewsMediator.calculateExpander;
        public String UserExpander
        {
            set
            {
                _userExpander = value;
                RaisePropertyChanged();
            }
            get
            {
                return _userExpander;
            }
        }

        public String OpponentExpander
        {
            set
            {
                _opponentExpander = value;
                RaisePropertyChanged();
            }
            get
            {
                return _opponentExpander;
            }
        }
        public String GameSettingsExpander
        {
            set
            {
                _gameSettingsExpander = value;
                RaisePropertyChanged();
            }
            get
            {
                return _gameSettingsExpander;
            }
        }

        public String CalculateExpander
        {
            set
            {
                _calcualteExpander = value;
                RaisePropertyChanged();
            }
            get
            {
                return _calcualteExpander;
            }
        }

        private string showMenu;
        private string showOView;
        public string ShowMenu
        {
            get
            {
                if (showMenu == null)
                    showMenu = "Visible";
                return showMenu; }

            set
            {
                showMenu = value;
                //call a method that checks the current status of the wizard stages and adjusts the Expanders.
                RaisePropertyChanged();
            }

        }


        /// <summary>
        /// transition between the windows of the wizard button controller.
        /// </summary>
        private string _wizardNavigator = "Back to Main Menu";
        public string wizardNavigator
        {
            get
            {
                return _wizardNavigator;
            }
            set
            {
                _wizardNavigator = value;
                RaisePropertyChanged();
            }
        }

        private static bool _isMenuControllerButtonEnabled = true;
        public bool isMenuControllerButtonEnabled
        {
            get
            {
                return _isMenuControllerButtonEnabled;
            }
            set
            {
                _isMenuControllerButtonEnabled = value;
                RaisePropertyChanged();
            }
        }

        public string ShowOView
        {
            get
            {
                if (showOView == null)
                    showOView = "Hidden";
                return showOView;
            }

            set
            {
                showOView = value;
                RaisePropertyChanged();
            }

        }

        public object CurrentViewModel
        {
            get {

                return currentViewModel;
            }
            set { currentViewModel = value; RaisePropertyChanged(); }
        }

       
        //<value>ObservableList that controls the Expanders</value>
        public ObservableCollection<String> expandersExpansionToggler;
        //<summary>updates the progress of the menus.
        //called on each return from a stage in the wizard</summary>
        public void updateMenus()
        {
            if (expandersExpansionToggler==null)
                expandersExpansionToggler = new ObservableCollection<String>();
        }
        private RelayCommand<Type> navigateCommand;
        public RelayCommand<Type> NavigateCommand
        {
            get
            {
                return  (navigateCommand = new RelayCommand<Type>(
                    vmType =>
                    {
                        //Bind a 'CurrentViewModel Set' event To the button
                        CurrentViewModel = null;
                        ChangeViewModel(vmType);
                        wizardNavigator = "Save";
                        //toggle menus visibility and then navigate
                        ShowMenu = (ShowMenu == "Visible" ? "Hidden" : "Visible");
                        ShowOView = (ShowOView == "Visible" ? "Hidden" : "Visible");
                        Console.WriteLine("Just Changed the CurrentViewModel to: {0} , \nShowMenu is {1}, \nShowView is {2}",CurrentViewModel, ShowMenu,ShowOView);
                    }));
            }
        }


        //not the most ideal solution, just a  temp method
        private RelayCommand<Type> navigateCommandEstimate;
        public RelayCommand<Type> NavigateCommandEstimate
        {
            get
            {
                return (navigateCommandEstimate = new RelayCommand<Type>(
                    vmType =>
                    {
                        //Bind a 'CurrentViewModel Set' event To the button
                        CurrentViewModel = null;
                        ChangeViewModel(vmType);
                        //toggle menus visibility and then navigate
                        wizardNavigator = "Next";
                        ShowMenu = (ShowMenu == "Visible" ? "Hidden" : "Visible");
                        ShowOView = (ShowOView == "Visible" ? "Hidden" : "Visible");
                        Console.WriteLine("Just Changed the CurrentViewModel to: {0} , \nShowMenu is {1}, \nShowView is {2}", CurrentViewModel, ShowMenu, ShowOView);
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        private RelayCommand<Type> backToWizardCommand;
        public RelayCommand<Type> BackToWizardCommand
        {
            get
            {
               return  (backToWizardCommand = new RelayCommand<Type>(
                    (vmType)=>
                    {
                        //check Form Validity
                        bool isFormValid = true;
                        foreach (KeyValuePair<string,bool> isFieldValid in ViewsMediator.isFormValid)
                        {
                            if (!isFieldValid.Value)
                                isFormValid = false;
                        }

                        if(isFormValid)
                        {
                            if (ViewsMediator.isDoneCalcualtingQ)
                            {
                                //Bind a 'CurrentViewModel Set' event To the button
                                CurrentViewModel = null;
                                //CurrentViewModel = Activator.CreateInstance(vmType);
                                ChangeViewModel(vmType);
                            }
                            else
                            {
                                ShowMenu = "Visible";
                                ShowOView = "Hidden";
                            }
                            UserExpander = ViewsMediator.userExpander;
                            OpponentExpander = ViewsMediator.opponentExpander;
                            GameSettingsExpander = ViewsMediator.gameSettingsExpander;
                            CalculateExpander = ViewsMediator.calculateExpander;
                            navigateCommand = null;
                        }
                        //something is wrong with the game settings form, alert the user.
                        else
                        {
                            ViewsMediator.isFormValid.Clear();
                            showMessageBox("Whoops, Something is wrong", "Please make sure that you typed the correct data in the game settings.");
                        }

                        ///<summary>
                        ///After each return from a window in the wizard print to the snackbar an update about the results or errors
                        ///after the messages were enqueued, remove them from the list.
                        /// </summary>
                        if (ViewsMediator.messagesForSnacky.Count >= 1)
                            foreach (String message in ViewsMediator.messagesForSnacky)
                            {
                                MainWindow.Snackbar.MessageQueue.Enqueue(message);
                            }
                        ViewsMediator.messagesForSnacky.Clear();

                    }));
            }
        }



        public MainWindowViewModel()
        {
            viewModels = new Dictionary<String, object>();
            viewModels.Add("materialDesignTesting.UserLoadStrategyViewModel",new UserLoadStrategyViewModel());
            viewModels.Add("materialDesignTesting.UserManualStrategyViewModel",new UserManualStrategyViewModel());
            viewModels.Add("materialDesignTesting.OpponentLoadStrategyViewModel",new OpponentLoadStrategyViewModel());
            viewModels.Add("materialDesignTesting.OpponentManualStrategyViewModel",new OpponentManualStrategyViewModel());
            viewModels.Add("materialDesignTesting.GameSettingsViewModel", new GameSettingsViewModel());
            //add additional views of game settings
        }



        private void ChangeViewModel(Type viewModelKey)
        {
            if (!viewModels.ContainsKey(viewModelKey.FullName))
                viewModels.Add(viewModelKey.FullName,Activator.CreateInstance(viewModelKey));

            CurrentViewModel = viewModels[viewModelKey.FullName];
        }


        #region Alert Box Properties and methods
        /// <summary>
        /// a method that shows the message box 
        /// </summary>
        /// <param name="header"></param>
        /// <param name="Body"></param>
        public void showMessageBox(String header, String Body)
        {
            messageBoxEnabled = 0.5;
            AlerBoxVisibility = "Visible";
            ErrorHead = header;
            ErrorBody = Body;
        }
        public void hideMessageBox()
        {
            messageBoxEnabled = 1;
            AlerBoxVisibility = "Hidden";
        }
        private string _AlerBoxVisibility = "Hidden";
        public string AlerBoxVisibility
        {
            get
            {
                return _AlerBoxVisibility;
            }
            set
            {
                _AlerBoxVisibility = value;
                RaisePropertyChanged();
            }
        }
        //message box view controller
        private double _messageBoxEnabled = 1;
        public double messageBoxEnabled
        {
            get
            {
                return _messageBoxEnabled;
            }
            set
            {
                _messageBoxEnabled = value;
                RaisePropertyChanged();
            }
        }
        private string _ErrorHead;
        public string ErrorHead
        {
            get
            {
                return _ErrorHead;
            }
            set
            {
                _ErrorHead = value;
                RaisePropertyChanged();
            }
        }
        private string _ErrorBody;
        public string ErrorBody
        {
            get
            {
                return _ErrorBody;
            }
            set
            {
                _ErrorBody = value;
                RaisePropertyChanged();
            }
        }

        private RelayCommand<Type> errorMessageDissimis;
        public RelayCommand<Type> ErrorMessageDissimis
        {
            get
            {
                return (errorMessageDissimis = new RelayCommand<Type>(
                     (vmType) =>
                     {
                         hideMessageBox();
                     }));
            }
        }
#endregion
}



    public class RelayCommand<T> : ICommand
    {
        #region Fields

        readonly Action<T> _execute = null;
        readonly Predicate<T> _canExecute = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateCommand{T}"/>.
        /// </summary>
        /// <param name="execute">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
        /// <remarks><seealso cref="CanExecute"/> will always return true.</remarks>
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        ///<summary>
        ///Defines the method that determines whether the command can execute in its current state.
        ///</summary>
        ///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        ///<returns>
        ///true if this command can be executed; otherwise, false.
        ///</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        ///<summary>
        ///Occurs when changes occur that affect whether or not the command should execute.
        ///</summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        ///<summary>
        ///Defines the method to be called when the command is invoked.
        ///</summary>
        ///<param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        #endregion
    }

}

using Calculator.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Calculator.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private string _screenVal;
        private List<string> _availableOperations = new List<string> { "+", "-", "/", "*" };
        private DataTable _dataTable = new DataTable();

        public MainViewModel()
        {
            ScreenVal = "0";
            AddNumberCommand = new RelayCommand(AddNumber);
            AddOperationCommand = new RelayCommand(AddOperation);
            ClearScreenCommand = new RelayCommand(ClearScreen);
            GetResultCommand = new RelayCommand(GetResult);
        }

        private void GetResult(object obj)
        {
            var result = _dataTable.Compute(ScreenVal.Replace(",","."), "");
            ScreenVal = result.ToString();
        }

        private void ClearScreen(object obj)
        {
            ScreenVal = "0";
        }

        private void AddOperation(object obj)
        {
            var operation = obj as string;
            if (!_availableOperations.Contains(ScreenVal.Substring(ScreenVal.Length - 1)))
            {
                ScreenVal += operation;
            }
        }

        private void AddNumber(object obj)
        {
            var number = obj as string;

            if (ScreenVal == "0" && number != ",")
            {
                ScreenVal = string.Empty;
            }
            else if (number == "," && _availableOperations.Contains(ScreenVal.Substring(ScreenVal.Length - 1)))
            {
                number = "0,";
            }

            ScreenVal += number;
        }

        public ICommand AddNumberCommand { get; set; }
        public ICommand AddOperationCommand { get; set; }
        public ICommand ClearScreenCommand { get; set; }
        public ICommand GetResultCommand { get; set; }


        public string ScreenVal
        {
            get { return _screenVal; }
            set 
            { 
                _screenVal = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using MVVMPractice.Commands;
using MVVMPractice.Model;
namespace MVVMPractice.ViewModel
{
    public class TodoViewModel : BaseViewModel
    {
        private string _title;
        private bool _isCompleted = false;
        private string _description;
        private ObservableCollection<Todo> _todos;
        private ICommand _SubmitCommand;
      
        public TodoViewModel()
        {
            _todos = new ObservableCollection<Todo>()
            {
                new Todo() { Title = "Salam",Description="Get Milk",Created=DateTime.Now.ToString("d"),isActive=true},
                new Todo() { Title ="Aleykum",Description="Get Milch",Created=DateTime.Now.ToString("d")}
            };
        }
       
        public bool IsCompleted { get { return _isCompleted; } set { _isCompleted = value; OnPropertyChanged(); } }
        public string Title { get { return _title; } set { _title = value; OnPropertyChanged(); } }

        public string DescriptionT { get { return _description; } set { _description = value;OnPropertyChanged(); } }

       /* public Todo todo()
        {
            return new Todo
            {
                Title = Title,
                Description = DescriptionT,
                Created = DateTime.Now,
                isActive = IsCompleted
            };

        }*/
        public ICommand SubmitCommand
        {
            get
            {
                if (_SubmitCommand == null)
                {
                    _SubmitCommand = new RelayCommand(param => this.Submit(),
                        null);
                }
                return _SubmitCommand;
            }
        }
        public ObservableCollection<Todo> todos { get { return _todos; } set {
                this._todos=value;
                base.OnPropertyChanged(); } }
        private void Submit()
        { var newTodo = new Todo
        {
            Title = Title,
            Description = DescriptionT,
            Created = DateTime.Now.ToString("d"),
            isActive = IsCompleted
        };
            todos.Add(newTodo);
            Trace.WriteLine(newTodo.ToString());
        }
    }
}

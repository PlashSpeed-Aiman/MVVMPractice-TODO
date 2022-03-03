using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVMPractice.Commands;
using MVVMPractice.Model;
using Newtonsoft.Json;
namespace MVVMPractice.ViewModel
{
    public class TodoViewModel : BaseViewModel
    {
        private string _title;
        private bool _isCompleted = false;
        private string _description;
        private ObservableCollection<Todo> _todos;
        private ICommand _SubmitCommand;
        private ICommand _SaveCommand;
        private ICommand _DeleteCommand;
        public TodoViewModel()
        {
            string text = System.IO.File.ReadAllText($"{System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}/file_json.json");
            var list = JsonConvert.DeserializeObject<IList<Todo>>(text);
            _todos = new ObservableCollection<Todo>();
            foreach (var item in list)
            {
                _todos.Add(item);
            }
        }

        public bool IsCompleted { get { return _isCompleted; } set { _isCompleted = value; OnPropertyChanged(); } }
        public string Title { get { return _title; } set { _title = value; OnPropertyChanged(); } }

        public string DescriptionT { get { return _description; } set { _description = value; OnPropertyChanged(); } }

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
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new RelayCommand(param => this.Delete(param), null);
                }
                return _DeleteCommand;
            }
        }
        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = new RelayCommand(param => this.WriteToJSON(todos),
                        null);
                }
                return _SaveCommand;
            }
        }
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
        public ObservableCollection<Todo> todos
        {
            get { return _todos; }
            set
            {
                this._todos = value;
                base.OnPropertyChanged();
            }
        }

        private async Task WriteToJSON(IList<Todo> todos)
        {
            var json_File = JsonConvert.SerializeObject(todos, Formatting.Indented);
            await File.WriteAllTextAsync($"{System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}/file_json.json", json_File);
        }
        private void Submit()
        {
            var newTodo = new Todo
            {
                Title = Title,
                Description = DescriptionT,
                Created = DateTime.Now.ToString("d"),
                isActive = IsCompleted
            };
            todos.Add(newTodo);
            Trace.WriteLine(newTodo.ToString());

        }
        private void Delete(object sum)
        {
            var stuff = sum as Todo;
            if (stuff != null) todos.Remove(stuff);
            Trace.WriteLine("Removed!");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MVVMPractice.Model
{
    public class Todo
    {
        private string _id;
        private string _title;
        private string _description;
        private bool _isActive;
        private string _created;

        public string Id { get { return _id; } set { _id = value; } }
        public string Title { get { return _title; } set { _title = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public string Created { get { return _created; } set { _created = value; } } 

        public bool isActive { get { return _isActive; } set { _isActive = value; Trace.WriteLine(value); } }
        public override string ToString()
        {
            return $"{Title} ({Description}) {Created} {isActive}";
        }
    }
}

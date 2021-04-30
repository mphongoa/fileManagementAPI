using System;
using System.Collections.Generic;
using System.Text;

namespace Task.Domain.BaseTypes
{
    public class TaskException : Exception
    {
        public TaskException(string message) : base(message)
        {
        }
    }
}

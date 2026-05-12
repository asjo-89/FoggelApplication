using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models
{
    public class ModelResult<T> where T : class
    {
        public T? Model { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
    }
}

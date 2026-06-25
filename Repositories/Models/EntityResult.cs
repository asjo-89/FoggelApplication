using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Models
{
    public class EntityResult<T> where T : class
    {
        public T? Entity { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
    }

    public class EntityResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
    }
}

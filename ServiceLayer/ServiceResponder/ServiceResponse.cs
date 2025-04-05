using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceResponder
{
    public class ServiceResponse<T>
    {
        // Default
        public ServiceResponse()
        {
            ErrorMessages = new List<string>();
        }
        public T? Data { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; } = null;
        public List<string> ErrorMessages { get; set; }
        public Exception? Exception { get; set; }
    }
}

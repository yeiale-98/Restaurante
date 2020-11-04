using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSRestaurante.Models
{
    public class ResultBackend
    {
        public bool IsError { get; set; }
        public int BackendCode { get; set; }
        public string BackendMessage { get; set; }
    }
}

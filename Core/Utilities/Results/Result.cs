using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResults
    {
        

        public Result(bool succes, string message):this(succes)
        {
            Message = message;
        }
        public Result(bool succes)
        {
            IsSuccess = succes;
        }

        public bool IsSuccess { get; }

        public string Message { get; }
    }
}

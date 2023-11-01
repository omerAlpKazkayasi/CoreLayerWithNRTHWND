using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public interface IResults
    {
        bool IsSuccess { get; }
        string Message { get; }
    }
}

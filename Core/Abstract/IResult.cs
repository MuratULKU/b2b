using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstract
{
    public interface IResult
    {
        public Guid Id { get; }
        public ResultStatus Status { get; }
        public string Message { get; }
        public Exception   Exception { get; }
    }

    public enum ResultStatus
    {
        Error = 0,
        Success = 1,
        Warning = 2,
        Information = 3,
    }
}

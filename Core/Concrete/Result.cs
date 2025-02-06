using Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concrete
{
    public class Result : IResult
    {
        public Result (ResultStatus resultStatus)
        {
            Status = resultStatus;
        }

        public Result(ResultStatus resultStatus, string message)
        {
            Status= resultStatus;
            Message = message;
        }
        public Result(ResultStatus resultStatus, string message, Exception exception)
        {
            Status = resultStatus;
            Message = message;
            Exception = exception;
        }

        public Result(ResultStatus resultStatus,Guid id ,string message)
        {
            Status = resultStatus;
            Message = message;
            Id = id;
        }


        public Guid Id { get; }

        public string Message { get; }

        public Exception Exception { get; }

        public ResultStatus Status { get; }

    }
}

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

    public class SuccessResult : Result
    {
        public SuccessResult(string message = null)
            : base(ResultStatus.Success, message) { }
    }

    public class ErrorResult : Result
    {
        public ErrorResult(string message = null)
            : base(ResultStatus.Error, message) { }
    }

    public class WarningResult : Result
    {
        public WarningResult(string message = null)
            : base(ResultStatus.Warning, message) { }
    }

    public class InfoResult : Result
    {
        public InfoResult(string message = null)
            : base(ResultStatus.Information, message) { }
    }
}

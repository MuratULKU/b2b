using Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concrete
{
    public class DataResult<T>:IDataResult<T>
    {
        public DataResult(ResultStatus resultStatus, T data) 
        {
            Status = resultStatus;
            Data = data;
        }

        public DataResult(ResultStatus resultStatus,string message ,T data) 
        { 
            Status = resultStatus;
            Message = message;
            Data = data;
        }

        public DataResult(ResultStatus resultStatus, string message, T data,Exception exception)
        {
            Status = resultStatus;
            Message = message;
            Data = data;
            Exception = exception;
        }

        public ResultStatus Status { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public T Data { get; set; }

        public Guid Id => throw new NotImplementedException();

       
    }
}

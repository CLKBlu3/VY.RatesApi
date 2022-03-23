using System;
using System.Collections.Generic;
using System.Linq;

namespace VY.RatesApi.XCutting.Domain.OperationResult
{
    public class OperationResult
    {
        private List<ErrorObject> _errors = new List<ErrorObject>();
        public IEnumerable<ErrorObject> Errors { get { return _errors; } }

        public void AddError(ErrorObject error) => _errors.Add(error);

        public void AddError(int code, string message, Exception ex = null)
        {
            ErrorObject error = new ErrorObject() { Code = code, Message = message, Exception = ex};
            _errors.Add(error);
        }  

        public void AddException(Exception ex) => _errors.Add(new ErrorObject() { Code = 0, Message = ex.Message, Exception = ex });
        public void AddError(IEnumerable<ErrorObject> errors) => _errors.AddRange(errors);

        public bool HasErrors() => _errors.Count > 0;
        public bool HasExceptions() => _errors.Any(c => c.Exception != null);
    }

    public class OperationResult<T> : OperationResult
    {
        public T Result { get; set; }
        public void SetResult(T result) => Result = result;
        public OperationResult(T result) => Result = result;

        public OperationResult()
        {
        }

    }
}

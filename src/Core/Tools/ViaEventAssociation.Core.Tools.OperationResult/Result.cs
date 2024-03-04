using System.Collections.Generic;

namespace ViaEventAssociation.Core.Tools.OperationResult
{
    public class ResultBase
    {
        public List<string> ErrorMessages { get; set; }
        public bool IsSuccess => ErrorMessages == null || ErrorMessages.Count == 0;

        public ResultBase(List<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }

        public ResultBase()
        {
            ErrorMessages = new List<string>();
        }
    }

    public class Result<T> : ResultBase
    {
        public T Values { get; set; }

        public Result(T value) 
        {
            Values = value;
        }

        public Result(List<string> errorMessages) : base(errorMessages)
        {
        }
        
        
    }
}
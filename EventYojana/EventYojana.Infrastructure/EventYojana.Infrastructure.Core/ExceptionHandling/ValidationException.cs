using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace EventYojana.Infrastructure.Core.ExceptionHandling
{
    public enum ValidationReason
    {
        Required,
        EmailFormat,
        PasswordFormat
    }

    [Serializable]
    public class ValidationException : Exception
    {
        private readonly List<Tuple<string, object, ValidationReason>> _reason = new List<Tuple<string, object, ValidationReason>>();

        public ValidationException()
        { }

        protected ValidationException(SerializationInfo info, StreamingContext context): base(info, context) { }

        public object EntityValue { get; private set; }

        public object PrimitiveValue { get; private set; }

        public bool HasErrors
        {
            get
            {
                bool returnVal = false;
                IList<Tuple<string, object, ValidationReason>> tempReason = new List<Tuple<string, object, ValidationReason>>();
                foreach(Tuple<string, object, ValidationReason> error in _reason)
                {
                    switch(error.Item3)
                    {
                        case ValidationReason.Required: 
                            if(error.Item2 == null || string.IsNullOrEmpty(error.Item2.ToString()))
                            {
                                tempReason.Add(error);
                                returnVal = true;
                            }
                            break;
                        case ValidationReason.EmailFormat:
                            if(!error.Item2.ToString().Contains("@") || !error.Item2.ToString().Contains("."))
                            {
                                tempReason.Add(error);
                                returnVal = true;
                            }
                            break;
                    }
                }
                _reason.Clear();
                _reason.AddRange(tempReason.ToList());
                return returnVal;
            }
        }
    
        public void Add<T>(string field, T value, ValidationReason reason)
        {
            if(typeof(T) == typeof(string) || typeof(T) == typeof(int) || typeof(T) == typeof(bool) ||
                typeof(T) == typeof(long) || typeof(T) == typeof(double) || typeof(T) == typeof(decimal))
            {
                _reason.Add(new Tuple<string, object, ValidationReason>(field, value, reason));
            }
            else
            {
                if(value != null)
                {
                    EntityValue = value;
                    PropertyInfo info = EntityValue.GetType().GetProperties().SingleOrDefault(o => string.Compare(o.Name, field, true) == 0);
                    if(info != null)
                    {
                        _reason.Add(new Tuple<string, object, ValidationReason>(field, info.GetValue(EntityValue), reason));
                    }
                }
            }
        }

        private string MapReasonToMessage(ValidationReason r)
        {
            switch(r)
            {
                case ValidationReason.Required:
                    return "Required field";
                case ValidationReason.EmailFormat:
                    return "Invalid email format";
                default:
                    return "General";
            }
        }

        public string[] GetErrorMessage()
        {
            string[] msgs = null;
            if(_reason.Count > 0)
            {
                msgs = new String[_reason.Count];
                int count = 0;
                foreach(Tuple<string, object, ValidationReason> error in _reason)
                {
                    msgs[count] = $"{MapReasonToMessage(error.Item3)} : {error.Item1}";
                    count++;
                }
            }
            return msgs;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class UserCreatedFailedException : Exception
    {
        public UserCreatedFailedException() : base("Failed")
        {
        }

        public UserCreatedFailedException(string? message) : base(message)
        {
        }

        public UserCreatedFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

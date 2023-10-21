using System;
using System.Collections.Generic;

namespace EcleticaBeerControl.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public IReadOnlyCollection<string> Errors { get; private set; }

        public DomainException(params string[] errors) : base("Execution failed !")
        {
            Errors = errors;
        }
    }
}

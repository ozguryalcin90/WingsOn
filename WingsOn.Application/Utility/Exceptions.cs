using System;

namespace WingsOn.Application.Utility
{
    public class EntityNotFoundException : Exception
    {
        private const string genericExceptionMessage = "Entity couldn't be found.";
        private const string exceptionMessage = "{0} couldn't be found.";

        public EntityNotFoundException() : base(genericExceptionMessage)
        {
        }

        public EntityNotFoundException(string entityName) : base(string.Format(exceptionMessage, entityName))
        {
        }
    }
}
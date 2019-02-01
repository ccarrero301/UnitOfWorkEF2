namespace Shared.Exceptions
{
    using System;

    public class UnauthenticatedUserException : Exception
    {
        public string AttemptedUser { get; }

        public string AttemptedPassword { get; }

        public UnauthenticatedUserException(string message, string attemptedUser, string attemptedPassword) :
            base(message)
        {
            AttemptedUser = attemptedUser;
            AttemptedPassword = attemptedPassword;
        }
    }
}
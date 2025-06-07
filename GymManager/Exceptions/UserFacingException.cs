namespace GymManager.Exceptions;

public class UserFacingException : Exception
{ 
    public UserFacingException(string message) : base(message) { }
}
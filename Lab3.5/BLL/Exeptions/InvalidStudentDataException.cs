namespace BLL.Exceptions;

public class InvalidStudentDataException : Exception
{
    public InvalidStudentDataException(string message) : base(message) { }
}
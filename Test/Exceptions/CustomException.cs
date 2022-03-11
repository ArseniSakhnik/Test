using System;

namespace Test.Exceptions
{
    public class CustomException : Exception
    {
        public string Message { get; set; } = "Нет такой промоакции";
        public CustomException(string message)
        {
            Message = message;
        }

        public CustomException()
        {
        }
    }
}
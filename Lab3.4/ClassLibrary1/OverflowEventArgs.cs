using System;

namespace ClassLibrary1
{
    // Клас аргументу події переповнення
    public class OverflowEventArgs : EventArgs
    {
        public string Message { get; }
        public int Operand1 { get; }
        public int Operand2 { get; }

        public OverflowEventArgs(string message, int operand1, int operand2)
        {
            Message = message;
            Operand1 = operand1;
            Operand2 = operand2;
        }
    }
}

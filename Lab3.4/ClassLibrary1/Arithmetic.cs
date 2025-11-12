using System;

namespace ClassLibrary1

{
    public class Arithmetic
    {
        public event EventHandler<OverflowEventArgs> OverflowOccurred;

        public int Add(int a, int b)
        {
            try
            {
                checked
                {
                    return a + b;
                }
            }
            catch (OverflowException)
            {
                OnOverflowOccurred(new OverflowEventArgs("Переповнення при додаванні", a, b));
                return 0;
            }
        }

        public int Subtract(int a, int b)
        {
            try
            {
                checked
                {
                    return a - b;
                }
            }
            catch (OverflowException)
            {
                OnOverflowOccurred(new OverflowEventArgs("Переповнення при відніманні", a, b));
                return 0;
            }
        }

        public int Multiply(int a, int b)
        {
            try
            {
                checked
                {
                    return a * b;
                }
            }
            catch (OverflowException)
            {
                OnOverflowOccurred(new OverflowEventArgs("Переповнення при множенні", a, b));
                return 0;
            }
        }

        protected void OnOverflowOccurred(OverflowEventArgs e)
        {
            OverflowOccurred?.Invoke(this, e);
        }
    }
}
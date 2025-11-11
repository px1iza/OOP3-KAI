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

        protected virtual void OnOverflowOccurred(OverflowEventArgs e)
        {
            OverflowOccurred?.Invoke(this, e);
        }
        //Тут за допомогою ?.Invoke перевіряється, чи є підписники.
        // Якщо є — подія викликається (тобто запускається обробник у Program).
    }
}
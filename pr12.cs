using System;

namespace CounterExample
{
    class Counter
    {
        private int value;

        public event EventHandler ValueChanged;

        public void Increment()
        {
            value++;
            OnValueChanged();
        }

        protected virtual void OnValueChanged()
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Counter counter = new Counter();

            counter.ValueChanged += (sender, args) =>
            {
                Console.WriteLine("Different");
            };

            counter.Increment();
            counter.Increment();
            counter.Increment();
        }
    }
}

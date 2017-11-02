using System;

namespace Task2
{
    class Person
    {
        private string _name;
        public string Name
        {
            get => _name;

            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentNullException();
                _name = value;
            }
        }

        public Person(string name)
        {
            _name = name;
        }

        public void Came()
        {
            Console.WriteLine("[На работу пришёл {0}]", Name);
            OnCame?.Invoke(this, null);
            Console.WriteLine();

            OnCame += Greet;
            OnLeave += Goodbye;
        }

        public void Leave()
        {
            Console.WriteLine("[{0} ушёл домой]", Name);
            OnCame -= Greet;
            OnLeave -= Goodbye;
            OnLeave?.Invoke(this, null);
            Console.WriteLine();
        }

        private void Greet(object sender, EventArgs e)
        {
            if (sender is Person p)
                Console.WriteLine("'Добрый день, {0}!', - сказал {1}.", p.Name, Name);
        }

        private void Goodbye(object sender, EventArgs e)
        {
            if (sender is Person p)
                Console.WriteLine("'До свидания, {0}!', - сказал {1}.", p.Name, Name);
        }

        public static event EventHandler OnCame;
        public static event EventHandler OnLeave;
    }
}

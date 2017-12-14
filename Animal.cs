using System;

namespace Pokemon
{
    public class Animal
    {
        private string name;
        
        public Animal(string name)
        {
            this.name = name;
        }
        
        public string Name => name;

        public virtual void WhoAmI()
        {
            Console.WriteLine("I am an animal !");
        }

        public virtual void Describe()
        {
            Console.WriteLine("My name is " + name + ".");
        }

        public void Rename(string NewName)
        {
            name = NewName;
        }        
    }
}

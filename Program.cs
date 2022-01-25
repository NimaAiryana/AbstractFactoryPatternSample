using AbstractFactoryPatternSample;

Console.WriteLine("Hello, Abstract Factory :D");

var cappuccino = new HotDrinkMachine().MakeHotDrink<Cappuccino>(100);
cappuccino.Consume();


namespace AbstractFactoryPatternSample
{
    public interface IHotDrink
    {
        void Consume();
    }

    public class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Drink tea and enjoy!");
        }
    }

    public class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Drink coffee and enjoy!");
        }
    }

    public class Cappuccino : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Drink cappuccino and enjoy!");
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    public class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"The tea prepared with {amount} ml tea");
            return new Tea();
        }
    }

    public class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"The coffee prepared with {amount} ml coffee");
            return new Coffee();
        }
    }

    public class CappuccinoFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"The cappuccino prepared with {amount} ml cappuccino");
            return new Cappuccino();
        }
    }

    public class HotDrinkMachine
    {
        private readonly Dictionary<Type, IHotDrinkFactory> _hotDrinkFactoryTypes = new();

        public HotDrinkMachine()
        {
            foreach (var drinkType in typeof(IHotDrink).Assembly.GetTypes().Where(t => typeof(IHotDrink).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract && !t.IsInterface).ToList())
            {
                if (Activator.CreateInstance(Type.GetType($"AbstractFactoryPatternSample.{drinkType.Name}Factory")) is not IHotDrinkFactory hotDrinkFactoryType) continue;

                _hotDrinkFactoryTypes.Add(drinkType, hotDrinkFactoryType);
            }
        }

        public IHotDrink MakeHotDrink<THotDrink>(int amount) where THotDrink : IHotDrink
        {
            return _hotDrinkFactoryTypes[typeof(THotDrink)].Prepare(amount);
        }
    }
}
using AbstractFactoryPatternSample;

Console.WriteLine("Hello, Abstract Factory :D");

var coffee = new HotDrinkMachine().MakeHotDrink(HotDrinkMachine.AvaliableHotDrink.Coffee, 100);
coffee.Consume();


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
            Console.WriteLine($"The tea prepared with {amount} ml tea");
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
        public enum AvaliableHotDrink
        {
            Tea,
            Coffee
        }

        private readonly Dictionary<AvaliableHotDrink, IHotDrinkFactory> _hotDrinkFactoryTypes = new();

        public HotDrinkMachine()
        {
            foreach (var drinkType in Enum.GetValues(typeof(AvaliableHotDrink)).Cast<AvaliableHotDrink>())
            {
                if (Activator.CreateInstance(Type.GetType($"AbstractFactoryPatternSample.{Enum.GetName(drinkType)}Factory")) is not IHotDrinkFactory hotDrinkFactoryType) continue;

                _hotDrinkFactoryTypes.Add(drinkType, hotDrinkFactoryType);
            }
        }

        public IHotDrink MakeHotDrink(AvaliableHotDrink hotDrink, int amount)
        {
            return _hotDrinkFactoryTypes[hotDrink].Prepare(amount);
        }
    }
}
using System;

namespace FactoryPatternImplementation
{

    //interfaces
    public interface IFactory
    {
        void Drive(string speed);
    }

    // interface implemented products
    public class Scooter : IFactory
    {
        public void Drive(string speed)
        {
            Console.WriteLine("Driving a scooter with speed of " + speed + " KM .");
        }
    }

    // interface implemented products
    public class Bike : IFactory
    {
        public void Drive(string speed)
        {
            Console.WriteLine("Driving a bike with speed of " + speed + " KM .");
        }
    }

    public enum Vehicle
    {
        Bike = 1,
        Scootter,
    }

    //Creator
    public abstract class VehicleFactory
    {
        public abstract IFactory CreateFactoryInstance(Vehicle vehicle);
    }

    //Concrete creator
    public class ConcreteVehicleFactory : VehicleFactory
    {
        public override IFactory CreateFactoryInstance(Vehicle vehicle)
        {
            IFactory factory = null;

            switch (vehicle)
            {
                case Vehicle.Bike:
                    factory = new Bike();
                    break;
                case Vehicle.Scootter:
                    factory = new Scooter();
                    break;
            }

            return factory;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            VehicleFactory factory = new ConcreteVehicleFactory();

            IFactory factory1 = factory.CreateFactoryInstance(Vehicle.Scootter);
            factory1.Drive("20");

            IFactory factory2 = factory.CreateFactoryInstance(Vehicle.Bike);
            factory2.Drive("70");

            Console.ReadKey();
        }
    }
}

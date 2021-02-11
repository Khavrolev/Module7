using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Ball ball = new Ball("Adidas", 2000, 5);
            string str = ball.ShowDescription();
            Console.WriteLine(str);

            Sneakers sneakers = new Sneakers("Jordan", 6000, "красные");
            str = sneakers.ShowDescription();
            Console.WriteLine(str);

            Console.ReadKey();
        }
    }

    abstract class Delivery
    {
        public string Address;
    }

    class HomeDelivery : Delivery
    {
        /* ... */
    }

    class PickPointDelivery : Delivery
    {
        /* ... */
    }

    class ShopDelivery : Delivery
    {
        /* ... */
    }

    abstract class Product
    {
        private string type;
        public string Type
        {
            get
            { return type; }

            set
            { type = value; }
        }
        private string name;
        public string Name
        {
            get
            { return name; }

            set
            { name = value; }
        }
        private double price;
        public double Price
        {
            get
            { return price; }

            set
            { price = value; }
        }

        public Product(string type, string name, double price)
        {
            this.type = type;
            this.name = name;
            this.price = price;
        }

        public abstract string ShowDescription();
    }

    class Ball : Product
    {
        private int size;
        public int Size
        {
            get { return size; }
            set { size = value; }
        }
        public Ball(string name, double price, int size) : base("Мяч", name, price)
        {
            this.size = size;
        }

        public override string ShowDescription()
        {
            return String.Format("{0}. {1}. Стоимость - {2} рублей (Размер - {3})", base.Type, base.Name, base.Price, this.size);
        }
    }

    class Sneakers : Product
    {
        private string color;
        public string Color
        {
            get { return color; }
            set { color = value; }
        }
        public Sneakers(string name, double price, string color) : base("Кроссовки", name, price)
        {
            this.color = color;
        }

        public override string ShowDescription()
        {
            return String.Format ("{0}. {1}. Стоимость - {2} рублей (Цвет - {3})", base.Type, base.Name, base.Price, this.color);
        }
    }

    class Order<TDelivery, TStruct> where TDelivery : Delivery
    {
        public TDelivery Delivery;

        public int Number;

        public string Description;

        public void DisplayAddress()
        {
            Console.WriteLine(Delivery.Address);
        }

        // ... Другие поля
    }
}

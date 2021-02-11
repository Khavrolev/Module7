using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Ball ball = new Ball("Molten FIBA GR7", 2000, new double[] { 4.5, 2.7 }, 5);
            string str = ball.ShowDescription();
            Console.WriteLine(str);

            Sneakers sneakers = new Sneakers("Jordan Why Not?", 6000, new double[] { 2.5, 1.7 }, "красные");
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

    abstract class Product //продукт
    {
        private string type; //тип продукта
        public string Type
        {
            get
            { return type; }
        }
        private string name; //имя продукта
        public string Name
        {
            get
            { return name; }
        }
        private double price; //цена продукта
        public double Price
        {
            get
            { return price; }
        }
        private protected Characteristics characteristics;

        public Product(string type, string name, double price, double[] characteristics)
        {
            this.type = type;
            this.name = name;
            this.price = price;
            this.characteristics = new Characteristics(characteristics[0], characteristics[1]);
        }

        public abstract string ShowDescription(); //метод получения описания продукта
    }

    class Characteristics //физические характеристики продукта
    {
        private double weightProduct; //вес продукта
        private double weightPack; //вес упаковки

        public Characteristics(double weightProduct, double weightPack)
        {
            this.weightProduct = weightProduct;
            this.weightPack = weightPack;
        }

        public double GetWeight() //метод, возвращающий общий вес продукта, который будет доставляться
        {
            return weightProduct + weightPack;
        }
    }

    class Ball : Product //мяч
    {
        private int size; //размер мяча
        public int Size
        {
            get { return size; }

        }
        public Ball(string name, double price, double[] characteristics, int size) : base("Мяч", name, price, characteristics)
        {
            this.size = size;
        }

        public override string ShowDescription()
        {
            return String.Format("{0}. {1}. Стоимость - {2} рублей (Размер - {3}, вес - {4})", base.Type, base.Name, base.Price, this.size, base.characteristics.GetWeight());
        } //метод получения описания продукта

    }

    class Sneakers : Product //кроссовки
    {
        private string color; //цвет кроссовок
        public string Color
        {
            get { return color; }
        }
        public Sneakers(string name, double price, double[] characteristics, string color) : base("Кроссовки", name, price, characteristics)
        {
            this.color = color;
        }

        public override string ShowDescription()
        {
            return String.Format("{0}. {1}. Стоимость - {2} рублей (Цвет - {3}, вес - {4})", base.Type, base.Name, base.Price, this.color, base.characteristics.GetWeight());
        } //метод получения описания продукта
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

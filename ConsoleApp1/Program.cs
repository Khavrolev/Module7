using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            HomeDelivery homeDelivery = new HomeDelivery("ул. Ленина д.42, кв. 56", "Иванов И.И.");
            Ball ball1 = new Ball("Molten №1", 5500, new double[] { 2.5, 1.4 }, 8);
            Ball ball2 = new Ball("Molten №2", 5200, new double[] { 2.6, 1.2 }, 9);
            Ball ball3 = new Ball("Molten №3", 5800, new double[] { 3.5, 1.7 }, 10);
            ProductCollection<Ball> productCollectionBall = new ProductCollection<Ball>(new Ball[] { ball1, ball2, ball3 });
            SDEK sdek = new SDEK();
            DPD dpd = new DPD();
            Client client = new Client("Василий Пупкин", 100);
            Order<Ball, HomeDelivery, SDEK> order = new Order<Ball, HomeDelivery, SDEK>(productCollectionBall, homeDelivery, sdek, client);
            order.DisplayInfo();
            CommonClass.DisplayHomeDelivery<SDEK>((Delivery)homeDelivery, sdek);
            CommonClass.DisplayHomeDelivery<DPD>((Delivery)homeDelivery, dpd);
            double price1 = ((Product)ball1).GetPrice();
            Console.WriteLine("Цена при помощи метода расширения {0}", price1);
            double price2 = ball1 + ball2;
            Console.WriteLine("Цена первых двух мячей при помощи перегружения оператора {0}", price2);

            Console.ReadKey();
        }
    }

    static class CommonClass
    {
        public static void DisplayHomeDelivery<TService>(Delivery delivery, TService service)
            where TService : Service
        {
            if (service.IsHomeDelivery())
                Console.WriteLine(String.Format("Служба доставки {0} доставит заказ по адресу {1}", service.Name, delivery.Address));
            else
                Console.WriteLine(String.Format("Служба доставки {0} не доставляет посылки на дом", service.Name));
        }
    } //статический класс для пары методов

    abstract class Service
    {
        private string name; //имя
        public string Name
        {
            get { return name; }
        }
        private static double maxDistance = 300; //максимальное расстояние для доставки в км
        public double MaxDistance
        {
            get { return maxDistance; }
        }

        public Service(string name)
        {
            this.name = name;
        }

        public bool IsDelivering(double distance)
        {
            return distance < maxDistance;
        } //доставляет ли компания заказ
        public abstract int GetDeliveryDays();

        public abstract bool IsHomeDelivery();
    } //служба доставки

    class SDEK : Service
    {
        private const string name = "СДЭК";
        public SDEK() : base(name)
        {
            //заполнение каких-то особенностей компании
        }
        public override int GetDeliveryDays()
        {
            //какая-то логика вычисления
            return 5;
        } //количество дней доставки

        public override bool IsHomeDelivery()
        {
            bool flag;
            //какая-то логика вычисления
            flag = true;
            return flag;
        } //доставляет ли на дом
    } //СДЭК

    class DPD : Service
    {
        private const string name = "DPD";
        public DPD() : base(name)
        {
            //заполнение каких-то особенностей компании
        }
        public override int GetDeliveryDays()
        {
            //какая-то логика вычисления
            return 6;
        } //количество дней доставки

        public override bool IsHomeDelivery()
        {
            bool flag;
            //какая-то логика вычисления
            flag = false;
            return flag;
        } //доставляет ли на дом
    } //ДПД

    abstract class Delivery
    {
        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public Delivery(string address)
        {
            this.address = address;
        }
    } //способ заказа

    class HomeDelivery : Delivery
    {
        private string recipient; //имя получателя
        public string Recipient
        {
            get { return recipient; }
        }
        public HomeDelivery(string address, string recipient) : base(address)
        {
            this.recipient = recipient;
        }
    } //заказ на дом

    class PickPointDelivery : Delivery
    {
        private int cell; //номер ячейки
        public int Cell
        {
            get { return cell; }
        }
        public PickPointDelivery(string address, int cell) : base(address)
        {
            this.cell = cell;
        }
    } //заказ в точку выдачи

    class ShopDelivery : Delivery
    {
        private bool isPartner; //магазин партнёр или наш
        public bool IsPartner
        {
            get { return isPartner; }
        }
        public ShopDelivery(string address, bool isPartner) : base(address)
        {
            this.isPartner = isPartner;
        }
    } //заказ в магазине

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

        public static double operator + (Product a, Product b)
        {
            return a.Price + b.Price;
        }
    }

    static class ProductExtensions
    {
        public static double GetPrice (this Product product)
        {
            return product.Price;
        }
    } //расширение для продукта, которое выдаёт цену

    class ProductCollection<TProduct> where TProduct : Product
    {
        private TProduct[] collection;

        public ProductCollection(TProduct[] collection)
        {
            this.collection = collection;
        }

        public Product this[int index]
        {
            get
            {
                if (index < collection.Length)
                    return collection[index];
                else
                    return null;
            }
        }

        public Product this[string name]
        {
            get
            {
                for (int i = 0; i < collection.Length; i++)
                {
                    if (collection[i].Name == name)
                        return collection[i];
                }

                return null;
            }
        }
        public int GetLength()
        {
            return collection.Length;
        }
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

    class Ball : Product
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
    } //мяч

    class Sneakers : Product
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
    } //кроссовки

    class Client
    {
        private string name; //имя клиента
        public string Name
        {
            get { return name; }
        }

        private double distance; //дистанция со склада
        public double Distance
        {
            get { return distance; }
        }

        public Client(string name, double distance)
        {
            this.name = name;
            this.distance = distance;
        }
    } //класс клиента

    class Order<TProduct, TDelivery, TService>
        where TProduct : Product
        where TDelivery : Delivery
        where TService : Service
    {
        protected ProductCollection<TProduct> productCollection;
        protected TDelivery delivery;
        protected TService service;
        protected Client client;

        public Order(ProductCollection<TProduct> productCollection, TDelivery delivery, TService service, Client client)
        {
            this.productCollection = productCollection;
            this.delivery = delivery;
            this.service = service;
            this.client = client;
        }

        public void DisplayInfo()
        {
            if (service.IsDelivering(client.Distance))
            {
                Console.WriteLine(String.Format("{0}, служба доставки {1} доставит товар по адресу {2} через {3} дней", client.Name, service.Name, delivery.Address, service.GetDeliveryDays()));
                Console.WriteLine("Ваш заказ:");

                for(int i = 0; i < productCollection.GetLength(); i++)
                {
                    Console.WriteLine(productCollection[i].ShowDescription());
                }
            }
            else
                Console.WriteLine("Извините, {0}, Вы слишком далеко, служба доставки {1} не может доставить товар", client.Name, service.Name);
        }
    } //главный класс, который собирает заказ
}

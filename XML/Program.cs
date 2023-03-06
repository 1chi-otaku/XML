using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XML
{
    class Order
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Order(string id, string name, double price)
        {
            ID = id;
            Name = name;
            Price = price;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Order order_1 = new Order("N010123328","CPU", 399.99);
            Order order_2 = new Order("N020123328", "GPU", 1299.99);
            Order order_3 = new Order("N030123328", "Montior", 299.99);

            List<Order> orders = new List<Order>();
            orders.Add(order_1);
            orders.Add(order_2);
            orders.Add(order_3);

            XmlTextWriter xmlwriter = new XmlTextWriter("../../Orders.xml", Encoding.UTF8);
            xmlwriter.WriteStartDocument();// Записывает объявление XML с номером версии 1.0
            // Formatting определяет способ форматирования выходных данных
            xmlwriter.Formatting = Formatting.Indented; //Форматирует отступы в дочерних элементах в соответствии с параметрами настройки IndentChar и Indentation
            xmlwriter.IndentChar = '\t'; // Возвращает или задает знак для отступа
            xmlwriter.Indentation = 1; // Возвращает или задает количество записываемых IndentChars для каждого уровня в иерархии
            xmlwriter.WriteStartElement("orders"); // Записывает указанный открывающий тег

            

            for (int i = 0; i < orders.Count; i++)
            {
                xmlwriter.WriteStartElement("order");
                xmlwriter.WriteStartAttribute(orders[i].ID, null); // Записывает атрибут с заданным именем
                xmlwriter.WriteString(orders[i].Name);
                xmlwriter.WriteEndAttribute(); // Закрывает атрибут
                xmlwriter.WriteStartAttribute("Price", null);
                xmlwriter.WriteString(Convert.ToString(orders[i].Price));// Записывает заданное текстовое содержимое атрибута
                xmlwriter.WriteEndAttribute(); // Закрывает атрибут
                xmlwriter.WriteEndElement(); // Закрывает один элемент
            }
            xmlwriter.WriteEndElement();

            xmlwriter.Close();


            List<Order> orders_read = new List<Order>();

            XmlTextReader reader = new XmlTextReader("../../Orders.xml");
            string str = null;
            while (reader.Read()) // Считывает следующий узел из потока
            {
                if (reader.NodeType == XmlNodeType.Text)
                    str += reader.Value + "\n";

                // NodeType возвращает тип текущего узла
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.HasAttributes)// имеются ли атрибуты?
                    {
                        while (reader.MoveToNextAttribute()) // Перемещается к следующему атрибуту
                        {
                            // Value - значение узла
                            str += reader.Value + "\n";
                        }
                    }
                }
            }
            Console.WriteLine(str);
            reader.Close();


        }
    }
}

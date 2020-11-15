using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//1. Создайте обобщенный интерфейс с операциями добавить, удалить,
//просмотреть.
//2. Возьмите за основу лабораторную № 4 «Перегрузка операций» и
//сделайте из нее обобщенный тип(класс) CollectionType<T>, в который
//вложите обобщённую коллекцию.Наследуйте в обобщенном классе интерфейс
//из п.1. Реализуйте необходимые методы.Добавьте обработку исключений c
//finally. Наложите какое-либо ограничение на обобщение.
//3. Проверьте использование обобщения для стандартных типов данных (в
//качестве стандартных типов использовать целые, вещественные и т.д.).
//Определить пользовательский класс, который будет использоваться в качестве
//параметра обобщения.Для пользовательского типа взять класс из лабораторной
//№5 «Наследование».
//Дополнительно:
//Добавьте методы сохранения объекта(объектов) обобщённого типа
//CollectionType<T> в файл и чтения из него.

namespace lab_8_1_
{
    interface IGeneral<T>
    {
        void Add(T element);
        void Delete(int index);
        void ListOut();
    }
    public class CollectionType<T> : IGeneral<T> //where T : new()
    {
        List<T> list = new List<T>();
        public void Add(T value)
        {
            list.Add(value);
        }
        public void ListOut()
        {
            Console.WriteLine();
            foreach (T x in list)
            {
                Console.WriteLine(x);
            }
        }
        public void Delete(int index)//удаление элемента в определенной позиции
        {
            list.RemoveAt(index);
        }
        public void AddElem(int index, T elem)
        {
            list.Insert(index, elem);
        }
        public static CollectionType<T> operator >>(CollectionType<T> list, int index)//удаление элемента в определенной позиции
        {
            list.Delete(index);
            return list;
        }
        //public T[] Array()
        //{
        //    return list.ToArray();
        //}
        //public static CollectionType<T> operator +(CollectionType<T> list, int index)//добавление элемента в определенную позицию
        //{
        //    Console.WriteLine("Enter word for insert: ");
        //    string elem = Console.ReadLine();
        //    list.AddElem(index, elem);
        //    return list;
        //}
        public static bool operator ==(CollectionType<T> list1, CollectionType<T> list2)
        {
            T[] arr1 = list1.list.ToArray();
            T[] arr2 = list2.list.ToArray();
            if (arr1.Length != arr2.Length)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < arr1.Length; i++)
                {
                    if (arr1[i].Equals(arr2[i]))
                    //if ((object)(arr1[i]) != (object)(arr2[i]))
                    {
                        return true;
                    }
                }
            }
            return true;
        }
        public static bool operator !=(CollectionType<T> list1, CollectionType<T> list2)
        {
            T[] arr1 = list1.list.ToArray();
            T[] arr2 = list2.list.ToArray();
            if (arr1.Length != arr2.Length)
            {
                return true;
            }
            else
            {
                for (int i = 0; i < arr1.Length; i++)
                {
                    if (arr1[i].Equals(arr2[i]))
                    //if ((object)(arr1[i]) != (object)(arr2[i]))
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        //public class Owner
        //{
        //    public int id { get; set; }
        //    public string name { get; set; }
        //    public string organization { get; set; }
        //    public string date { get; set; }
        //    public Owner(string name, int id, string organization, string date)
        //    {
        //        this.name = name;
        //        this.id = id;
        //        this.organization = organization;
        //        this.date = Convert.ToString(DateTime.Now);
        //    }
        //}
        public void WriteInFile(StreamWriter write)
        {
            foreach (T i in list)
            {
                write.WriteLine(i);
            }
        }
    }
    class EmptyTitle : NullReferenceException //не  ввели название книги
    {
        public EmptyTitle(string message) : base(message)
        { }
    }
    class ImpossibleYear : ArgumentOutOfRangeException //этот год еще не наступил
    {
        public ImpossibleYear(string message) : base(message)
        { }
    }
    abstract class GeneralInfo
    {
        public string Title { get; set; }
        public string Country { get; set; }
        public int CurrentYear = DateTime.Now.Year;
        public int Year { get; set; }
        public int Pages { get; set; }
        public string Cover { get; set; }
        public double Price { get; set; }
    }
    class Book : GeneralInfo
    {
        public Book(string title, string country, int year, int pages, string cover, double price)
        {
            if (title == "" || title == null)
            {
                throw new EmptyTitle("You didn't write title of the book(");
            }
            else
            {
                Title = title;
            }
            Country = country;
            if (year > CurrentYear)
            {
                throw new ImpossibleYear("Are you sure? This year has not come yet...");
            }
            Year = year;
            Pages = pages;
            Cover = cover;
            Price = price;
        }
        public override string ToString()
        {
            return "~~~~~~~~~~Information about book~~~~~~~~~~\nTitle: " + Title + "\nYear: " + Year + "\nPages: " + Pages + "\nCountry: " + Country + "\nPrice: " + Price;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CollectionType<int> list1 = new CollectionType<int>();
            list1.Add(1);
            list1.Add(2);
            list1.Add(3);
            list1.Add(4);
            list1.Add(5);
            Console.WriteLine("\nList1: ");
            list1.ListOut();

            CollectionType<double> list2 = new CollectionType<double>();
            list2.Add(6);
            list2.Add(7);
            list2.Add(8);
            list2.Add(9);
            Console.WriteLine("\nList2: ");
            list2.ListOut();

            Console.WriteLine("\nDelete element 1 in list2: ");
            list2.Delete(0);
            list2.ListOut();

            Console.WriteLine("\nDelete element 2 in list2: ");
            list2 = list2 >> 0;
            list2.ListOut();

            CollectionType<int> list3 = new CollectionType<int>();
            list3.Add(1);
            list3.Add(2);
            list3.Add(3);
            list3.Add(4);
            list3.Add(5);
            Console.WriteLine("\nList3: ");
            list3.ListOut();

            Console.WriteLine("\nCompare list1 and list3: ");
            Console.WriteLine(list1 == list3);

            CollectionType<Book> list4 = new CollectionType<Book>();
            try
            {
                Book book1 = new Book("Harry Potter and the Prisoner of Azkaban", "United Kingdom", 1999, 464, "hard", 30);
                Book book2 = new Book("Harry Potter and the Half-Blood Prince", "United Kingdom", 2005, 607, "hard", 30);
                list4.Add(book1);
                list4.Add(book2);
                Console.WriteLine("\nList4: ");
                list4.ListOut();

                Console.WriteLine("\n\n\n~~~Writing to file~~~");
                StreamWriter file = new StreamWriter(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_8\Lab_8\lab_8(1)\FileForWriting.txt");
                file.WriteLine("List1:");
                list1.WriteInFile(file);
                file.WriteLine("\nList2:");
                list2.WriteInFile(file);
                file.WriteLine("\nList3:");
                list3.WriteInFile(file);
                file.WriteLine("\nList4:");
                list4.WriteInFile(file);
                file.Close();
                Console.WriteLine("Writing to file done)");

                Console.WriteLine("\n\n\n~~~Reading from file~~~");
                StreamReader read = new StreamReader(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_8\Lab_8\lab_8(1)\FileForWriting.txt");
                Console.WriteLine(read.ReadToEnd());
                read.Close();
                Console.WriteLine("Reading to file done)");
            }
            catch (EmptyTitle ex)
            {
                Console.WriteLine(ex.Message + "\nMethod with exception: " + ex.TargetSite + "\n" + ex.StackTrace + "\n\n");
            }
            catch (ImpossibleYear ex)
            {
                Console.WriteLine(ex.Message + "\nMethod with exception: " + ex.TargetSite + "\n" + ex.StackTrace + "\n\n");
            }
            finally
            {
                Console.WriteLine("The work is done!!!\nI hope so...\n\n");
            }
        }
    }
}

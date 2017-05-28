using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace SocketClient
{

    public class Book
    {
        public string Autor { get; set; }
        public string Title { get; set; }


    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SendMessageFromSocket(11000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }

        static void SendMessageFromSocket(int port)
        {
          
            // Буфер для входящих данных
            byte[] bytes = new byte[1024];
            List<Book> newBook = new List<Book>();

            // Соединяемся с удаленным устройством

            // Устанавливаем удаленную точку для сокета
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Соединяем сокет с удаленной точкой
            sender.Connect(ipEndPoint);

            menu();

            string a = Console.ReadLine();
            switch (a)
            {
                case "1":
                    {
                        Console.Clear();
                        //Отправка
                        byte[] msg = Encoding.UTF8.GetBytes(a);
                        int bytesSent = sender.Send(msg);

                        string data = null;
                        //Принять данные
                        int bytesRec = sender.Receive(bytes);

                        data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                        newBook = JsonConvert.DeserializeObject<List<Book>>(data);
                        //Вывод на консоль
                        foreach (Book kniga in newBook)
                        {
                            string dd = kniga.Autor + "-" + kniga.Title + Environment.NewLine;
                            Console.WriteLine(dd);
                        }
                        Console.ReadKey();
                        Console.Clear();
                        SendMessageFromSocket(port);
                        break;
                    }
                case "2":
                    {
                        Console.Clear();
                        newBook = new List<Book>();
                        string autor; //автор
                        string proza; //книга
                        string data2 = null;
                        byte[] bytes2 = new byte[1024];
                        //отправка выбора
                        byte[] msg2 = Encoding.UTF8.GetBytes(a);
                        int bytesSent2 = sender.Send(msg2);

                        //Принятие 
                        int bytesRec2 = sender.Receive(bytes2);

                        data2 += Encoding.UTF8.GetString(bytes2, 0, bytesRec2);
                        newBook = JsonConvert.DeserializeObject<List<Book>>(data2);

                        Console.WriteLine("Введите автора:");
                        autor = Console.ReadLine();
                        Console.WriteLine("Введите название произведения:");
                        proza = Console.ReadLine();

                        newBook.Add(new Book() { Autor = autor, Title = proza });
                        string serialized = JsonConvert.SerializeObject(newBook);
                        //Отправка
                        msg2 = Encoding.UTF8.GetBytes(serialized);
                        bytesSent2 = sender.Send(msg2);

                        Console.Clear();
                        SendMessageFromSocket(port);
                        break;
                    }
                case "4":
                    {
                        Console.Clear();
                        string data3 = null;
                        byte[] msg3 = Encoding.UTF8.GetBytes(a);
                        int bytesSent3 = sender.Send(msg3);

                        byte[] bytes3 = new byte[1024];
                        int bytesRec3 = sender.Receive(bytes3);

                        data3 += Encoding.UTF8.GetString(bytes3, 0, bytesRec3);
                        newBook = JsonConvert.DeserializeObject<List<Book>>(data3);

                        Console.WriteLine("Выберите какую книгу вы хотите удалить");
                        for (int i = 0; i < newBook.Count; i++)
                        {
                            Console.WriteLine(i + "   " + newBook[i].Autor + "-" + newBook[i].Title);

                        }
                        int ind = Int32.Parse(Console.ReadLine());

                        newBook.RemoveAt(ind);



                        foreach (Book kniga in newBook)
                        {
                            string dd = kniga.Autor + "-" + kniga.Title + Environment.NewLine;
                            Console.WriteLine(dd);
                        }
                        string serialized = JsonConvert.SerializeObject(newBook);
                        //Отправка
                        msg3 = Encoding.UTF8.GetBytes(serialized);
                        bytesSent3 = sender.Send(msg3);
                        Console.Clear();
                        Console.ReadKey();
                        SendMessageFromSocket(port);

                    }

                    break;
                case "5":
                    {
                        byte[] msg4 = Encoding.UTF8.GetBytes(a);

                        int bytesSent4 = sender.Send(msg4);
                        Environment.Exit(0);
                        break;
                    }
                default:
                    Console.WriteLine("Неверная команда");
                    SendMessageFromSocket(port);
                    break;
                case "3":
                    {
                        string autor;
                        string proza;
                        Console.Clear();
                        string data3 = null;
                        byte[] msg3 = Encoding.UTF8.GetBytes(a);
                        int bytesSent3 = sender.Send(msg3);

                        byte[] bytes3 = new byte[1024];
                        int bytesRec3 = sender.Receive(bytes3);

                        data3 += Encoding.UTF8.GetString(bytes3, 0, bytesRec3);
                        newBook = JsonConvert.DeserializeObject<List<Book>>(data3);

                        Console.WriteLine("Выберите произведение, параметры которого вы хотите редактировать");
                        for (int i = 0; i < newBook.Count; i++)
                        {
                            Console.WriteLine(i + "   " + newBook[i].Autor + "-" + newBook[i].Title);
                        }
                        int ind = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Введите автора:");
                        autor = Console.ReadLine();
                        Console.WriteLine("Введите название:");
                        proza = Console.ReadLine();
                        newBook[ind].Autor = autor;
                        newBook[ind].Title = proza;
                        foreach (Book kniga in newBook)
                        {
                            string dd = kniga.Autor + "-" + kniga.Title + Environment.NewLine;
                            Console.WriteLine(dd);
                        }
                        string serialized = JsonConvert.SerializeObject(newBook);
                        //Отправка
                        msg3 = Encoding.UTF8.GetBytes(serialized);
                        bytesSent3 = sender.Send(msg3);
                        Console.Clear();
                        Console.ReadKey();
                        SendMessageFromSocket(port);
                    }


                    break;
            }

        }



        public static void menu()
        {
            Console.WriteLine("Выберете действие:");
            Console.WriteLine("1) Посмотреть библиотеку");
            Console.WriteLine("2) Добавить книгу");
            Console.WriteLine("3) Изменить параметры произведения");
            Console.WriteLine("4) Удалить книгу");
            Console.WriteLine("5) Выйти");

        }


    }


}

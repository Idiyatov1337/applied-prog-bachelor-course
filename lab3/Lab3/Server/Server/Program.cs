using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;



namespace SocketServer
{


    public class Track
    {
        public string Artist { get; set; }
        public string Title { get; set; }


    }
    class Program
    {
        static void Main(string[] args)
        {

            // Устанавливаем для сокета локальную конечную точку
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);

            // Создаем сокет Tcp/Ip
            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);

                // Начинаем слушать соединения
                while (true)
                {
                    Console.WriteLine("Ожидаем соединение через порт {0}", ipEndPoint);

                    // Программа приостанавливается, ожидая входящее соединение
                    Socket handler = sListener.Accept();
                    string data = null;

                    // Мы дождались клиента, пытающегося с нами соединиться

                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);

                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    ReaderWriter f = new ReaderWriter();
                    switch (data)
                    {
                        case "1": //просмотр
                            using (FileStream fstream = File.OpenRead("date.json"))
                            {

                                string a = f.Reader(fstream);
                                byte[] msg = Encoding.UTF8.GetBytes(a);
                                handler.Send(msg);
                                break;

                            }
                        case "2": //добавить
                            {
                                using (FileStream fstream = File.OpenRead("date.json"))
                                {
                                    //Чтение
                                    string a = f.Reader(fstream);

                                    //Отправка
                                    byte[] msg2 = Encoding.UTF8.GetBytes(a);
                                    handler.Send(msg2);
                                }

                                //Получение

                                bytesRec = handler.Receive(bytes);
                                data = null;
                                data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                                using (FileStream fstream = new FileStream(@"date.json", FileMode.OpenOrCreate))
                                {

                                    f.Writer(fstream, data);
                                    Console.WriteLine("Текст записан в файл");
                                }
                                break;
                            }

                        case "3": //редактировать
                            {
                                using (FileStream fstream = File.OpenRead("date.json"))
                                {
                                    //Чтение
                                    string a = f.Reader(fstream);

                                    //Отправка
                                    byte[] msg2 = Encoding.UTF8.GetBytes(a);
                                    handler.Send(msg2);
                                }
                                //Получение

                                bytesRec = handler.Receive(bytes);
                                data = null;
                                data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                                using (FileStream fstream = new FileStream(@"date.json", FileMode.Create))
                                {

                                    f.Writer(fstream, data);
                                    Console.WriteLine("Текст записан в файл");
                                }

                                break;
                            }
                        
                        case "4"://удалить
                            {
                                using (FileStream fstream = File.OpenRead("date.json"))
                                {
                                    //Чтение
                                    string a = f.Reader(fstream);

                                    //Отправка
                                    byte[] msg2 = Encoding.UTF8.GetBytes(a);
                                    handler.Send(msg2);
                                }
                                //Получение

                                bytesRec = handler.Receive(bytes);
                                data = null;
                                data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                                using (FileStream fstream = new FileStream(@"date.json", FileMode.Create))
                                {

                                    f.Writer(fstream, data);
                                    Console.WriteLine("Текст записан в файл");
                                }


                                break;
                            }

                        case "5": //выход
                            Console.WriteLine("Сервер завершил соединение с клиентом.");

                            handler.Shutdown(SocketShutdown.Both);
                            handler.Close();
                            break;
                       
                    }

                }


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
    }
}

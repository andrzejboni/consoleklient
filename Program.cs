
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace czat
{
    class Klient
    {
        static void Main(string[] args)
        {
            var klient = new TcpClient();
            klient.Connect(IPAddress.Parse("127.0.0.1"), 2000);
            // "192.168.0.29"

            var watek = new System.Threading.Thread(wysylanie);
            watek.Start(klient);

            // DateTime dt = DateTime.Now;
            // DateTime bbb = DateTime.Now.TimeOfDay
            var dt = DateTime.Now.ToShortTimeString();

            while (true)
            {

                var dane = new byte[256];
                klient.GetStream().Read(dane, 0, dane.Length);
                Console.Write("["+dt+"]"+  "  ");  // Wyrzyguje date wysłania wiadomosci
                Console.WriteLine(Encoding.UTF8.GetString(dane));
            }
        }
        static void wysylanie(object klient)
        {
            var klientCzat = (TcpClient)klient;
            while (true)
            {
                var komunikat = Console.ReadLine();
                var komunikatBinarnie = Encoding.UTF8.GetBytes(komunikat);
                klientCzat.GetStream().Write(komunikatBinarnie, 0, komunikatBinarnie.Length);
            }
        }
    }
}

using System.Net.Sockets;
using System.Text;

namespace MessengerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userName = null;

            while(userName == null) 
            {
                Console.Write("Введите имя: ");
                userName = Console.ReadLine();
            }

            Client client = new Client(userName);
            client.Start();
        }
    }
}

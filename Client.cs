using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MessengerApp
{
    internal class Client
    {
        readonly string serverIP = "192.168.1.9";
        readonly int serverPort = 2897;
        readonly string userName = null;

        internal Client(string userName)
        {
            this.userName = userName;
        }
        internal void Start()
        {
            try
            {
                TcpClient client = new TcpClient(serverIP, serverPort);
                Console.WriteLine($"Подключение к серверу {serverIP}:{serverPort}");

                Thread serverThread = new Thread(HandleServer);
                serverThread.Start(client);

                Thread clientThread = new Thread(WriteMessage);
                clientThread.Start(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при работе клиента: {ex.Message}");
            }
        }

        //Слушаем сервер
        private void HandleServer(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream serverStream = tcpClient.GetStream();

            byte[] buffer = new byte[4096];
            int bytesRead;

            while (true)
            {
                //Чтение данных из потока
                bytesRead = serverStream.Read(buffer, 0, buffer.Length);

                Console.WriteLine(DecodeMessage(buffer, bytesRead));
            }
        }
        private void WriteMessage(object client)
        {
            TcpClient tcpClient = (TcpClient)client;

            NetworkStream stream = tcpClient.GetStream();

            string message;
            byte[] buffer = new byte[4096];

            while (true)
            {

                message = Console.ReadLine();

                buffer = EncodeMessage($"{userName}: " + message);
                stream.Write(buffer, 0, buffer.Length);
            }
        }
        private byte[] EncodeMessage(string message)
        {
            return _ = Encoding.UTF8.GetBytes(message);
        }
        private string DecodeMessage(byte[] message, int bytesRead)
        {
            byte[] actualMessage = new byte[bytesRead];
            Array.Copy(message, actualMessage, bytesRead);

            return _ = Encoding.UTF8.GetString(actualMessage);
        }
    }
}

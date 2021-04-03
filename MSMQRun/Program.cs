using System;
using System.Messaging;

namespace MSMQRun
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SendMessage();
        }

        private static void SendMessage()
        {
            using (MessageQueue simpleChatQueue = new MessageQueue()) 
            { 

            }
        }
    }
}

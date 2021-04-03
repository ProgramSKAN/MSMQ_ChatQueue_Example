using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace ChatQueue
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    class Program
    {
        public static readonly string _queuePath = ".\\private$\\chatqueue";
        static void Main(string[] args)
        {
            //for (int i = 0; i < 20000; i++)
            //{
            //    SendMessages(new User { Id = 1, Name = $"msg-{i}" });
            //}
            bool isContinue = false;
            while (!isContinue)
            {
                Console.Write("Enter Message: ");
                string input = Console.ReadLine();
                var msg = new User { Id = 1, Name = input };
                
                SendMessages(msg);

                //Console.WriteLine("wanna send another message? Y/N");
                //string val = Console.ReadLine();
                //if (val.ToLower() == "n") break;
            }
        }

        private static void SendMessages(User msg)
        {
            using(MessageQueue chatQueue=new MessageQueue(_queuePath))
            {
                if (!MessageQueue.Exists(chatQueue.Path))
                    MessageQueue.Create(chatQueue.Path,true);
                var message = new Message()
                {
                    Label = msg.GetMessageType(),
                    BodyStream = msg.ToJsonStream()
                };

                var tx = new MessageQueueTransaction();
                tx.Begin();
                chatQueue.Send(message,tx);
                tx.Commit();
            }
        }
        
    }
}

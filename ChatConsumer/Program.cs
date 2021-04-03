using ChatQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ChatConsumer
{
    class Program
    {
        public static readonly string _queuePath = ".\\private$\\chatqueue";
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            var t1=Task.Run(() => ReceieveMessagesAsync("T1"));
            var t2=Task.Run(() => ReceieveMessagesAsync("T2"));
            var t3=Task.Run(() => ReceieveMessagesAsync("T3"));
            var t4=Task.Run(() => ReceieveMessagesAsync("T4"));
            await Task.WhenAll(t1, t2,t3,t4);
        }

        private static Task ReceieveMessagesAsync(string task)
        {
            using (MessageQueue chatQueue = new MessageQueue(_queuePath))
            {
                Console.WriteLine("Listening chatqueue ..."+ task);
                while (true)
                {
                    using (var tx = new MessageQueueTransaction())
                    {
                        tx.Begin();
                        var message = chatQueue.Receive(tx);
                        var messageBody = (User)message.BodyStream.ReadFromJson(message.Label);
                        var messageType = messageBody.GetType();
                        if (messageType == typeof(User))
                        {
                            Console.WriteLine(task+"> " +messageBody.Name);
                        }
                        else
                        {
                            Console.WriteLine("command implementation not available");
                        }
                        tx.Commit();
                    }
                    //Message message = new Message();
                    //chatQueue.Receive(new TimeSpan(5));
                    //message.Formatter = new BinaryMessageFormatter(FormatterAssemblyStyle.Simple, FormatterTypeStyle.TypesAlways);
                    //string msg = message.Body.ToString();
                    //Console.WriteLine(message);
                }

            }
        }
    }
}

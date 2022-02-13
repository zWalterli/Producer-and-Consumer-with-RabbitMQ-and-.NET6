using Consumer.Service;

namespace Consumer
{
    public class Program
    {
        public static void Main()
        {
            QueueConsumer.Consume();
        }
    }
}
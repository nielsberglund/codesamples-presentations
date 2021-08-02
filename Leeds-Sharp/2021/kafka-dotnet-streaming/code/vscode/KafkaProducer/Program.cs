using System;
using Confluent.Kafka;
using System.Threading.Tasks;

namespace test
{
  class Program
  {
    static async Task Main(string[] args)
    {
      Console.WriteLine("Hello World!");
      var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
      using (var producer = new ProducerBuilder<string, string>(config).Build())
      {
        while (true)
        {
          Console.WriteLine("Press ENTER to exit, any other key to continue");
          var x = Console.ReadLine();
          if(x == "") {
            break;
          }
          try
          {
            var dr = await producer.ProduceAsync("testtopic", new Message<string, string> { Value = "hello" });
            Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
          }
          catch (ProduceException<Null, string> e)
          {
            Console.WriteLine($"Delivery failed: {e.Error.Reason}");
          }
        }
      }
    }
  }

  
}

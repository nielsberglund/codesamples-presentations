using System;
using Confluent.Kafka;
using System.Threading;

namespace KafkaConsumer
{
  class Program
  {
    static void Main(string[] args)
    {
      var conf = new ConsumerConfig
      {
        GroupId = "test-consumer-group",
        BootstrapServers = "localhost:9092",
        AutoOffsetReset = AutoOffsetReset.Earliest
      };

      var topic = "testtopic";
      var src = new CancellationTokenSource();
      var tkn = src.Token;

      using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
      {
        c.Subscribe(topic);
        try
        {
          while (true)
          {
            var cr = c.Consume(tkn);
            Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");
          }
        }
        catch (OperationCanceledException)
        {
          c.Close();
        }
      }
    }
  }
}

using System;
using Confluent.Kafka;
using System.Threading;

namespace CardConsumer
{
  class Program {
    static void Main(string[] args)
    {
      var conf = new ConsumerConfig
      {
        GroupId = "card-count-group",
        BootstrapServers = "localhost:9092",
        AutoOffsetReset = AutoOffsetReset.Earliest,

      };

      var topic = "TB_CARD_COUNT";
      var src = new CancellationTokenSource();
      var tkn = src.Token;

      using (var c = new ConsumerBuilder<string, string>(conf).Build())
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
using System;
using System.Threading;
using Confluent.Kafka;

namespace CardPublisher
{
  
  public class KafkaPublisher {
    static IProducer<string, string> producer = null;
    static ProducerConfig producerConfig = null;

    static bool isInitialised = false;

    internal static void InitialiseProducer()
    {
      producerConfig = new ProducerConfig
      {
        BootstrapServers = "localhost:9092"
      };

      producer = new ProducerBuilder<string, string>(producerConfig).Build();
      isInitialised = true;

    }
    internal static async void SendMessage(string topic, string partitionValue, string message)
    {
      if(!isInitialised) {
        InitialiseProducer();
      }
      var msg = new Message<string, string>
      {
        Key = partitionValue,
        Value = message

      };
      try {
        var delRep = await producer.ProduceAsync(topic, msg);
        var topicOffset = delRep.TopicPartitionOffset;

        Console.WriteLine($"Delivered '{delRep.Value}' to: {topicOffset}");
      }
      catch (ProduceException<Null, string> e)
      {
        Console.WriteLine($"Delivery failed: {e.Error.Reason}");
      }
    }

  }

}
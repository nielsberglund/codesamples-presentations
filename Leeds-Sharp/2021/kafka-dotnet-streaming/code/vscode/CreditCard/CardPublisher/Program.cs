using System;
using System.Text.Json;
namespace CardPublisher
{

  class Program
  {
    static void Main(string[] args)
    {
      var topic = "cardauth";
      while(true) {
        Console.WriteLine("Press ENTER to exit, any other key to continue");
        var input = Console.ReadLine();
        if(input == "") {
          break;
        }
        else {
          var c = CardAuthorization.GenerateCard();
          Console.WriteLine($"CardId: {c.CardId}, Date: {c.AuthDate}");
          var msg = JsonSerializer.Serialize<CardAuthorization>(c);

          KafkaPublisher.SendMessage(topic, c.CardId.ToString(), msg);
        }
      
    }


    }
  }

}
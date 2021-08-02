using System;

namespace CardPublisher
{
  public class CardAuthorization {
    
    public CardAuthorization(){}
    public CardAuthorization(long cardId) {
      CardId = cardId;
      AuthDate = DateTime.UtcNow;
    }
    public CardAuthorization(long cardId, DateTime dt) {
      CardId = cardId;
      AuthDate = dt;
    }
    public long CardId {get;set;}
    public DateTime AuthDate {get;set;}

    public static CardAuthorization GenerateCard(){
      Random rnd = new Random();
      int minCardId = 1001;
      int maxCardId = 1005;

      var cardId = (rnd.Next(minCardId, maxCardId));
      

      return new CardAuthorization(cardId, DateTime.UtcNow);
    }

  }

}
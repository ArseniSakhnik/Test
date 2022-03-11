using System.Collections.Generic;
using Test.Models;
using Test.Responses;
using Test.Responses.Raffle;

namespace Test.Interfaces
{
    public interface IPromoService
    {
        public int AddPromo(PromoActionRequest model);
        public IList<PromoDto> GetPromotions();
        public PromoFullInfoDto GetPromotionById(int promoId);
        public void ChangePromoById(int id, PromoActionRequest request);
        public void DeletePromoById(int promoId);
        public int AddParticipant(int promoId, AddParticipantRequest request);
        public void RemoveParticipantFromPromo(int promoId, int participantId);
        public int AddPrizeToPromo(int promoId, AddPrizeToPromoRequest request);
        public void DeletePrizeFromPromo(int promoId, int prizeId);
        public IList<RaffleDto> Raffle(int promoId);
        public bool RaffleValidate(int promoId);
    }
}
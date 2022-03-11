using System;
using System.Collections.Generic;
using System.Linq;
using Test.Entities;
using Test.Exceptions;
using Test.Interfaces;
using Test.Models;
using Test.Responses;
using Test.Responses.Raffle;

namespace Test.Services
{
    public class PromoService : IPromoService
    {
        private readonly IAppDbService _dbService;

        public PromoService(IAppDbService dbService)
        {
            _dbService = dbService;
        }

        public int AddPromo(PromoActionRequest model)
        {
            var promo = new Promotion
            {
                Id = _dbService.PromotionLastId,
                Name = model.Name,
                Description = model.Description
            };

            _dbService.Promotions.Add(promo);

            return promo.Id;
        }

        public IList<PromoDto> GetPromotions()
        {
            return _dbService.Promotions
                .Select(promotion => new PromoDto
                {
                    Id = promotion.Id,
                    Name = promotion.Name,
                    Description = promotion.Description
                })
                .ToList();
        }

        public PromoFullInfoDto GetPromotionById(int promoId)
        {
            var doesPromotionExist = _dbService.Promotions.Any(promotion => promotion.Id == promoId);

            if (!doesPromotionExist) throw new CustomException();

            return _dbService.Promotions
                .Select(promotion => new PromoFullInfoDto
                {
                    Id = promotion.Id,
                    Name = promotion.Name,
                    Description = promotion.Description,
                    Participants = promotion.Participants,
                    Prizes = promotion.Prizes
                })
                .SingleOrDefault(promotion => promotion.Id == promoId);
        }

        public void ChangePromoById(int id, PromoActionRequest request)
        {
            var promo = _dbService.Promotions
                .SingleOrDefault(promo => promo.Id == id);

            if (promo is null) throw new CustomException();

            promo.Name = request.Name;
            promo.Description = request.Description;
        }

        public void DeletePromoById(int promoId)
        {
            var promo = _dbService.Promotions
                .SingleOrDefault(promo => promo.Id == promoId);

            if (promo is null) throw new CustomException();

            _dbService.Promotions.Remove(promo);
        }

        public int AddParticipant(int promoId, AddParticipantRequest request)
        {
            var promo = _dbService.Promotions
                .SingleOrDefault(promo => promo.Id == promoId);
            
            if (promo is null) throw new CustomException();

            var participant = new Participant
            {
                Id = _dbService.ParticipantLastId,
                Name = request.Name
            };

            _dbService.Participants.Add(participant);
            promo.Participants.Add(participant);

            return participant.Id;
        }

        public void RemoveParticipantFromPromo(int promoId, int participantId)
        {
            var promo = _dbService.Promotions
                .SingleOrDefault(promo => promo.Id == promoId);
            
            
            if (promo is null) throw new CustomException();

            var participant = _dbService.Participants
                .SingleOrDefault(participant => participant.Id == participantId);

            promo.Participants.Remove(participant);
        }

        public int AddPrizeToPromo(int promoId, AddPrizeToPromoRequest request)
        {
            var promo = _dbService.Promotions
                .SingleOrDefault(promo => promo.Id == promoId);
            
            if (promo is null) throw new CustomException();

            var prize = new Prize
            {
                Id = _dbService.PrizeLastId,
                Description = request.Description
            };

            promo.Prizes.Add(prize);
            _dbService.Prizes.Add(prize);

            return prize.Id;
        }

        public void DeletePrizeFromPromo(int promoId, int prizeId)
        {
            var promo = _dbService.Promotions
                .SingleOrDefault(promo => promo.Id == promoId);
            
            if (promo is null) throw new CustomException();

            var prize = promo.Prizes
                .SingleOrDefault(prize => prize.Id == prizeId);

            promo.Prizes.Remove(prize);
            _dbService.Prizes.Remove(prize);
        }

        public IList<RaffleDto> Raffle(int promoId)
        {
            var promo = _dbService.Promotions
                .SingleOrDefault(promo => promo.Id == promoId);
            
            if (promo is null) throw new CustomException();

            var raffleResults = new List<RaffleDto>();

            var rand = new Random();

            foreach (var participant in promo.Participants)
            {
                var prizesForParticipant = promo.Prizes
                    .Where(prize => !raffleResults
                        .Select(result => result.Prize.Id).Contains(prize.Id))
                    .ToArray();

                var randomPrize = prizesForParticipant[rand.Next(prizesForParticipant.Length)];

                raffleResults.Add(new RaffleDto
                {
                    Prize = new PrizeDto
                    {
                        Id = randomPrize.Id,
                        Description = randomPrize.Description
                    },
                    Winner = new WinnerDto
                    {
                        Id = participant.Id,
                        Name = participant.Name
                    }
                });
            }

            return raffleResults;
        }

        public bool RaffleValidate(int promoId)
        {
            var promo = _dbService.Promotions
                .SingleOrDefault(promo => promo.Id == promoId);
            
            if (promo is null) throw new CustomException();

            return promo.Participants.Count == promo.Prizes.Count;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Test.Entities;
using Test.Interfaces;

namespace Test.Data
{
    public class AppDbService : IAppDbService
    {
        public List<DrawResult> DrawResults { get; set; } = new();
        public List<Participant> Participants { get; set; } = new();
        public List<Prize> Prizes { get; set; } = new();
        public List<Promotion> Promotions { get; set; } = new();

        public int PromotionLastId => !Promotions.Any()
            ? 1
            : Promotions.Max(promotion => promotion.Id) + 1;

        public int PrizeLastId => !Prizes.Any()
            ? 1
            : Prizes.Max(prize => prize.Id) + 1;

        public int ParticipantLastId => !Participants.Any()
            ? 1
            : Participants.Max(participant => participant.Id) + 1;
    }
}
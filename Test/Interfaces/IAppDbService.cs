using System.Collections.Generic;
using Test.Entities;

namespace Test.Interfaces
{
    public interface IAppDbService
    {
        public List<DrawResult> DrawResults { get; set; }
        public List<Participant> Participants { get; set; }
        public List<Prize> Prizes { get; set; }
        public List<Promotion> Promotions { get; set; }
        public int PromotionLastId { get; }
        public int PrizeLastId { get; }
        public int ParticipantLastId { get; }
    }
}
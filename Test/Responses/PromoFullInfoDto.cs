using System.Collections.Generic;
using Test.Entities;

namespace Test.Responses
{
    public class PromoFullInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Prize> Prizes { get; set; }
        public List<Participant> Participants { get; set; }
    }
}
using System.Collections.Generic;

namespace Test.Entities
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Prize> Prizes { get; set; } = new();
        public Participant Participant { get; set; } = new();
        public List<Participant> Participants { get; set; } = new();
    }
}
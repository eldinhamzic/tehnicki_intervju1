using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitedStateElections.Database
{
    public class ConstituencyOfCandudate
    {
        public int Id { get; set; }
        public int? ConstituencyId { get; set; }
        public int? CandidateId { get; set; }
        public int? NumberOfVotes { get; set; }
        public bool? OverrideFile { get; set; }

        public virtual Constituency Constituency { get; set; }
        public virtual Candidate Candidate { get; set; }

    }
}

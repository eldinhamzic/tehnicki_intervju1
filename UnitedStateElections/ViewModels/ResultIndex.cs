using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitedStateElections.ViewModels
{
    public class ResultIndex
    {
        internal object row;

        public List<Rows> Row { get; set; }

        public class Rows
        {
            public int ConstituencyId { get; set; }
            public string Constituency { get; set; }
            public int CandidateId { get; set; }
            public string Candidate { get; set; }
            public string NumberOfVotes { get; set; }
            public string Percentage { get; set; }

            public string OverrideFile { get; set; } 
        }
    }
}

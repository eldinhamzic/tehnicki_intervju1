
using System.Collections.Generic;


namespace UnitedStateElections.Database
{
    public class Candidate
    {
        public Candidate()
        {
            ConstituencyOfCandudates = new HashSet<ConstituencyOfCandudate>();
        }
        public int Id { get; set; }
        public string FullName { get; set; }
       
        public string CandidateCode { get; set; }

        public virtual ICollection<ConstituencyOfCandudate> ConstituencyOfCandudates{ get; set; }
        
    }
}

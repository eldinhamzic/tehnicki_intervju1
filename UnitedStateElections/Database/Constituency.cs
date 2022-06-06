using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitedStateElections.Database
{
    public class Constituency
    {
       public Constituency()
        {
            ConstituencyOfCandudates = new HashSet<ConstituencyOfCandudate>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ConstituencyOfCandudate> ConstituencyOfCandudates { get; }
     
        
    }
}

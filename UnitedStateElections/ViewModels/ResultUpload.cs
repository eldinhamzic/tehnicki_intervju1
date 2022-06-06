using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitedStateElections.ViewModels
{
    public class ResultUpload
    {
       
            
            public string Constituency { get; set; } = "";
            
            public string Candidate { get; set; } = "";
            
            public string NumberOfVotes { get; set; } = "";
            
            public string OverrideFile { get; set; } = "";
        
    }
}

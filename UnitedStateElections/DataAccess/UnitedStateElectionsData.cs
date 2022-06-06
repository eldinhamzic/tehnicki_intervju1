using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitedStateElections.Database;

namespace UnitedStateElections.DataAccess
{
    public partial class UnitedStateElectionsData
    {

        private void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            #region Adding Kandidate 
            modelBuilder.Entity<Candidate>().HasData(
                new Candidate()
                {
                    Id = 1,
                    FullName = "Donald Trump",
                    CandidateCode = "DT"
                },
                new Candidate()
                {
                    Id = 2,
                    FullName = "Joe Biden",
                    CandidateCode = "JB"
                   
                },
                new Candidate()
                {
                    Id = 3,
                    FullName = "Hillary Clinton",
                    CandidateCode = "HC"
                });
            #endregion


            #region Adding Constituency 
            modelBuilder.Entity<Constituency>().HasData(
                new Constituency()
                {
                    Id = 1,
                    Name = "Miami",
                },
                new Constituency()
                {
                    Id = 2,
                    Name = "NewYork",
                },
                new Constituency()
                {
                    Id = 3,
                    Name = "Chicago",
                });
            #endregion



            #region Adding Constituency 
            modelBuilder.Entity<ConstituencyOfCandudate>().HasData(
                new ConstituencyOfCandudate()
                {
                    Id = 1,
                    ConstituencyId = 1,
                    CandidateId = 1,
                    NumberOfVotes = 25000,
                    OverrideFile = false
                },
                new ConstituencyOfCandudate()
                {
                    Id = 2,
                    ConstituencyId = 1,
                    CandidateId = 2,
                    NumberOfVotes = 36521,
                    OverrideFile = false
                },
                new ConstituencyOfCandudate()
                {
                    Id = 3,
                    ConstituencyId = 1,
                    CandidateId = 3,
                    NumberOfVotes = 777,
                    OverrideFile = false
                },


                new ConstituencyOfCandudate()
                {
                    Id = 4,
                    ConstituencyId = 2,
                    CandidateId = 1,
                    NumberOfVotes = 7854,
                    OverrideFile = false
                },
                new ConstituencyOfCandudate()
                {
                    Id = 5,
                    ConstituencyId = 2,
                    CandidateId = 2,
                    NumberOfVotes = 9987,
                    OverrideFile = false
                },
                new ConstituencyOfCandudate()
                {
                    Id = 6,
                    ConstituencyId = 2,
                    CandidateId = 3,
                    NumberOfVotes = 15036,
                    OverrideFile = false
                },

                 new ConstituencyOfCandudate()
                 {
                     Id = 7,
                     ConstituencyId = 3,
                     CandidateId = 1,
                     NumberOfVotes = 53377,
                     OverrideFile = false
                 },
                new ConstituencyOfCandudate()
                {
                    Id = 8,
                    ConstituencyId = 3,
                    CandidateId = 2,
                    NumberOfVotes = 11117,
                    OverrideFile = false
                },
                new ConstituencyOfCandudate()
                {
                    Id = 9,
                    ConstituencyId = 3,
                    CandidateId = 3,
                    NumberOfVotes = 708,
                    OverrideFile = false
                });
            #endregion

        }
    }
}

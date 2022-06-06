using CsvHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnitedStateElections.DataAccess;
using UnitedStateElections.ViewModels;


namespace UnitedStateElections.Web.Controllers
{
    public class ResultsController : Controller
    {
        public UnitedStateElectionContext _context;
        public ResultsController(UnitedStateElectionContext context)
        {
            _context = context;
        }


        public IActionResult ShowResults()
        {
            var Res = _context.ConstituencyOfCandudates.Include(x => x.Candidate).Include(x => x.Constituency).FirstOrDefault();

            var model = new ResultIndex()
            {
                row = _context.ConstituencyOfCandudates
                .Select(x => new ResultIndex.Rows
                {
                    Constituency = x.Constituency.Name,

                    Candidate = x.Candidate.FullName,

                    NumberOfVotes = x.NumberOfVotes.ToString(),

                    OverrideFile = ((bool)x.OverrideFile).ToString(),

                    Percentage = Math.Round(((((float)x.NumberOfVotes) / 20201) * 100), 0) + "%".ToString()
                })
                  .ToList()
            };

            return View(model);
        }


        [HttpGet]
        public IActionResult Index(List<ViewModels.ResultUpload> test = null)
        {
            test = test == null ? new List<ViewModels.ResultUpload>() : test;
            List<ViewModels.ResultUpload> data = new List<ViewModels.ResultUpload>();

            return View(test);
        }


         [HttpPost]
        public IActionResult Index(IFormFile file, [FromServices] IHostingEnvironment hostingEnvironment)
        {
            try
            {
                #region Upload CSV
                string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
                using (FileStream fileStream = System.IO.File.Create(fileName))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }

                #endregion
                var test = this.GetTestList(file.FileName);
                return Index(test);
            }
            catch(System.Exception)
            {

                var Error = new Database.Exception();
                Error.Message = "(error code: Please insert a file! )";
                TempData["ErrorMessage"] = "Please insert a file!";
                _context.Exceptions.Add(Error);
                _context.SaveChanges();
            }
            
            return Index();
        }


        private List<ViewModels.ResultUpload> GetTestList(string fileName)
        {
            List<ViewModels.ResultUpload> test = new List<ViewModels.ResultUpload>();
            #region Read CSV
            string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fileName;
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var tests = csv.GetRecord<ResultUpload>();
                    test.Add(tests);
                }
            }
            #endregion

            #region Create CSV
            path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\FilesTo"}";
            using (var write = new StreamWriter(path + "\\NewFile.csv"))
            {
                using (var csv = new CsvWriter(write))
                {
                    csv.WriteRecords(test);
                }
            }
            #endregion

            var Data = new Database.ConstituencyOfCandudate();
            if (test.Count() > 0)
            {
                for (int i = 0; i < test.Count(); i++)
                {
                    var AConstituency = _context.Constituencies.Where(x => x.Name == test[i].Constituency).FirstOrDefault();
                    var ACandidates = _context.Candidates.Where(x => x.FullName == test[i].Candidate).FirstOrDefault();

                    if (test[i].OverrideFile.Contains("TRUE"))
                    {
                        Data = new Database.ConstituencyOfCandudate();
                        Data.ConstituencyId = AConstituency.Id;

                        Data.CandidateId = ACandidates.Id;

                        Data.NumberOfVotes = int.Parse(test[i].NumberOfVotes);

                        if (test[i].OverrideFile.Contains("TRUE"))
                            Data.OverrideFile = true;

                        if (test[i].OverrideFile.Contains("FALSE"))
                            Data.OverrideFile = false;

                        _context.ConstituencyOfCandudates.Update(Data);
                        _context.SaveChanges();
                    }
                    else
                    {
                        Data = new Database.ConstituencyOfCandudate();
                        Data.ConstituencyId = AConstituency.Id;

                        Data.CandidateId = ACandidates.Id;

                        Data.NumberOfVotes = int.Parse(test[i].NumberOfVotes);

                        if (test[i].OverrideFile.Contains("TRUE"))
                            Data.OverrideFile = true;

                        if (test[i].OverrideFile.Contains("FALSE"))
                            Data.OverrideFile = false;

                        _context.ConstituencyOfCandudates.Add(Data);
                        _context.SaveChanges();

                    }

                }
            }

            return test;
        }
    }
}

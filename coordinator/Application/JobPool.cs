using System;
using System.Collections.Generic;
using System.Linq;

namespace coordinator.Application
{
    public interface IJobPool
    {
        Job GetNextJob(int previousCorrectResponses);
    }

    public class JobPool : IJobPool
    {
        List<Job> jobs = new List<Job>
        {
            new Job { Difficulty = 0, Problem = "CALCULATE: 3 + 4", Solution = "7" },
            new Job { Difficulty = 0, Problem = "CALCULATE: 626 + 711", Solution = "1337" },
            new Job { Difficulty = 0, Problem = "CALCULATE: 626+711", Solution = "1337" },
            new Job { Difficulty = 0, Problem = "CALCULATE: 35 - 20", Solution = "15" },
            new Job { Difficulty = 0, Problem = "CALCULATE: 5 - 8", Solution = "-3" },
            new Job { Difficulty = 0, Problem = "CALCULATE: 9 / 3", Solution = "3" },
            new Job { Difficulty = 0, Problem = "CALCULATE: 3 / 4", Solution = ".75" },
            new Job { Difficulty = 0, Problem = "CALCULATE: 3/4", Solution = ".75" },
            new Job { Difficulty = 0, Problem = "CALCULATE: 12 * 6", Solution = "72" },
            new Job { Difficulty = 0, Problem = "CALCULATE: 11 * 11", Solution = "121" },
            new Job { Difficulty = 0, Problem = "CALCULATE: 11*11", Solution = "121" },
            new Job { Difficulty = 1, Problem = "CALCULATE: (3 + 4) * 5", Solution = "35" },
            new Job { Difficulty = 1, Problem = "CALCULATE: 5 * (3 + 4)", Solution = "35" },
            new Job { Difficulty = 2, Problem = "CALCULATE: 3^4", Solution = "81" },
            new Job { Difficulty = 2, Problem = "CALCULATE: 26%5", Solution = "1" },
            new Job { Difficulty = 2, Problem = "CALCULATE: 400^.5", Solution = "20" },
            new Job { Difficulty = 3, Problem = "CALCULATE: 26 modulo 5", Solution = "1" },
            new Job { Difficulty = 3, Problem = "CALCULATE: 5(6)", Solution = "30" },
            new Job { Difficulty = 3, Problem = "CALCULATE: 100 log 10", Solution = "2" },
            new Job { Difficulty = 3, Problem = "CALCULATE: sqrt 400", Solution = "20" },
            new Job { Difficulty = 4, Problem = "CALCULATE: IV plus III", Solution = "VII" },
            new Job { Difficulty = 4, Problem = "CALCULATE: XIV times IX", Solution = "CXXVI" },
            new Job { Difficulty = 5, Problem = "CALCULATE: sqrt -81", Solution = "9i" },
            new Job { Difficulty = 5, Problem = "CALCULATE: √(-81)", Solution = "9i" },
            new Job { Difficulty = 6, Problem = "CALCULATE: √(x^2 + 4x + 4)", Solution = "x + 2" },
    };

        public Job GetNextJob(int previousCorrectResponses)
        {
            return jobs.Where(x => x.Difficulty == previousCorrectResponses / 10).OrderBy(x => Guid.NewGuid().ToString()).FirstOrDefault() ?? new Job() { Difficulty = 10, Problem = "CALCULATE: The sum of all numbers, less than one million, which are palindromic in base 10 and base 2", Solution = "872187" };
        }
    }

    public class Job
    {
        public int Difficulty { get; set; }
        public string Problem { get; set; }
        public string Solution { get; set; }
    }
}
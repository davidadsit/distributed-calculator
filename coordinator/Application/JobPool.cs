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
        Random random = new Random();

        List<Job> jobs = new List<Job>
        {
            new Job { Difficulty = 2, Problem = "CALCULATE: (3 + 4) * 5", Solution = "35" },
            new Job { Difficulty = 2, Problem = "CALCULATE: 5 * (3 + 4)", Solution = "35" },
            new Job { Difficulty = 2, Problem = "CALCULATE: 3^4", Solution = "81" },
            new Job { Difficulty = 2, Problem = "CALCULATE: (3 * 4)^5", Solution = "248832" },
            new Job { Difficulty = 2, Problem = "CALCULATE: 400^.5", Solution = "20" },

            new Job { Difficulty = 3, Problem = "CALCULATE: 26 % 5", Solution = "1" },
            new Job { Difficulty = 3, Problem = "CALCULATE: 26 modulo 5", Solution = "1" },
            new Job { Difficulty = 3, Problem = "CALCULATE: 5(6)", Solution = "30" },
            new Job { Difficulty = 3, Problem = "CALCULATE: 100 log 10", Solution = "2" },
            new Job { Difficulty = 3, Problem = "CALCULATE: ln e", Solution = "1" },
            new Job { Difficulty = 3, Problem = "CALCULATE: ln e^3", Solution = "3" },
            new Job { Difficulty = 3, Problem = "CALCULATE: sqrt 400", Solution = "20" },

            new Job { Difficulty = 4, Problem = "CALCULATE: IV plus III", Solution = "VII" },
            new Job { Difficulty = 4, Problem = "CALCULATE: XIV times IX", Solution = "CXXVI" },

            new Job { Difficulty = 5, Problem = "CALCULATE: sqrt -81", Solution = "9i" },
            new Job { Difficulty = 5, Problem = "CALCULATE: √(-81)", Solution = "9i" },

            new Job { Difficulty = 6, Problem = "CALCULATE: √(x^2 + 4x + 4)", Solution = "x + 2" },
    };

        public Job GetNextJob(int previousCorrectResponses)
        {
            var difficulty = previousCorrectResponses / 10;
            switch (difficulty)
            {
                case 0:
                    return CreateSimpleJob(difficulty, 0, 1000);
                case 1:
                    return CreateSimpleJob(difficulty, -1000, 1000);
                default:
                    return jobs.Where(x => x.Difficulty == difficulty).OrderBy(x => Guid.NewGuid().ToString()).FirstOrDefault() ?? new Job() { Difficulty = 10, Problem = "CALCULATE: The sum of all numbers, less than one million, which are palindromic in base 10 and base 2", Solution = "872187" };
            }
        }

        private Job CreateSimpleJob(int level, int operandMin, int operandMax)
        {
            decimal operand1 = random.Next(operandMin, operandMax);
            decimal operand2 = random.Next(operandMin, operandMax);
            var operation = "";
            var solution = "";

            switch (random.Next(0, 4))
            {
                case 0:
                    operation = "+";
                    solution = $"{operand1 + operand2}";
                    break;
                case 1:
                    operation = "-";
                    solution = $"{operand1 - operand2}";
                    break;
                case 2:
                    operation = "*";
                    solution = $"{operand1 * operand2}";
                    break;
                case 3:
                    operation = "/";
                    solution = $"{operand1 / operand2:0.###}";
                    break;
            }
            return new Job
            {
                Difficulty = level,
                Problem = $"CALCULATE: {operand1} {operation} {operand2}",
                Solution = solution
            };
        }
    }

    public class Job
    {
        public int Difficulty { get; set; }
        public string Problem { get; set; }
        public string Solution { get; set; }
    }
}
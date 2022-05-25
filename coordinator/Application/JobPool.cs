namespace coordinator.Application;

public interface IJobPool
{
    Job GetNextJob(int previousCorrectResponses);
}

public class JobPool : IJobPool
{
    Random random = new Random();

    List<Job> jobs = new List<Job>
    {
        new Job { Difficulty = 3, Problem = "CALCULATE: (3 + 4) * 5", Solution = "35" },
        new Job { Difficulty = 3, Problem = "CALCULATE: 5 * (3 + 4)", Solution = "35" },
        new Job { Difficulty = 3, Problem = "CALCULATE: 3^4", Solution = "81" },
        new Job { Difficulty = 3, Problem = "CALCULATE: (3 * 4)^5", Solution = "248832" },
        new Job { Difficulty = 3, Problem = "CALCULATE: 400^.5", Solution = "20" },

        new Job { Difficulty = 4, Problem = "CALCULATE: 26 % 5", Solution = "1" },
        new Job { Difficulty = 4, Problem = "CALCULATE: 26 modulo 5", Solution = "1" },
        new Job { Difficulty = 4, Problem = "CALCULATE: 5(6)", Solution = "30" },
        new Job { Difficulty = 4, Problem = "CALCULATE: 100 log 10", Solution = "2" },
        new Job { Difficulty = 4, Problem = "CALCULATE: ln e", Solution = "1" },
        new Job { Difficulty = 4, Problem = "CALCULATE: ln e^3", Solution = "3" },
        new Job { Difficulty = 4, Problem = "CALCULATE: sqrt 400", Solution = "20" },

        new Job { Difficulty = 5, Problem = "CALCULATE: IV plus III", Solution = "VII" },
        new Job { Difficulty = 5, Problem = "CALCULATE: XIV times IX", Solution = "CXXVI" },

        new Job { Difficulty = 6, Problem = "CALCULATE: sqrt -81", Solution = "9i" },
        new Job { Difficulty = 6, Problem = "CALCULATE: √(-81)", Solution = "9i" },

        new Job { Difficulty = 7, Problem = "CALCULATE: √(x^2 + 4x + 4)", Solution = "x + 2" },
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
            case 2:
                return CreateThreeOperandJob();
            default:
                return jobs.Where(x => x.Difficulty == difficulty).OrderBy(x => Guid.NewGuid().ToString()).FirstOrDefault() ?? new Job() { Difficulty = 10, Problem = "CALCULATE: The sum of all numbers, less than one million, which are palindromic in base 10 and base 2", Solution = "872187" };
        }
    }

    private Job CreateSimpleJob(int level, int operandMin, int operandMax)
    {
        decimal operand1 = random.Next(operandMin, operandMax);
        decimal operand2 = random.Next(operandMin, operandMax);
        var operation = "";
        decimal solution = 0;

        switch (random.Next(0, 4))
        {
            case 0:
                operation = "+";
                solution = operand1 + operand2;
                break;
            case 1:
                operation = "-";
                solution = operand1 - operand2;
                break;
            case 2:
                operation = "*";
                solution = operand1 * operand2;
                break;
            case 3:
                if (operand2 == 0)
                {
                    operand2 = 1;
                }

                operation = "/";
                solution = operand1 / operand2;
                break;
        }
        return new Job
        {
            Difficulty = level,
            Problem = $"CALCULATE: {operand1} {operation} {operand2}",
            Solution = $"{solution:0.###}"
        };
    }
    private Job CreateThreeOperandJob()
    {
        decimal operand1 = random.Next(-1000, 1000);
        decimal operand2 = random.Next(-1000, 1000);
        decimal operand3 = random.Next(-1000, 1000);
        var operation1 = "";
        var operation2 = "";
        decimal solution = 0;

        switch (random.Next(0, 4))
        {
            case 0:
                operation1 = "+";
                operation2 = "+";
                solution = operand1 + operand2 + operand3;
                break;
            case 1:
                operation1 = "+";
                operation2 = "-";
                solution = operand1 + operand2 - operand3;
                break;
            case 2:
                operation1 = "+";
                operation2 = "*";
                solution = operand1 + operand2 * operand3;
                break;
            case 3:
                operation1 = "+";
                operation2 = "/";
                if (operand3 == 0)
                {
                    operand3 = 1;
                }

                solution = operand1 + operand2 / operand3;
                break;
            case 4:
                operation1 = "-";
                operation2 = "+";
                solution = operand1 - operand2 + operand3;
                break;
            case 5:
                operation1 = "-";
                operation2 = "-";
                solution = operand1 - operand2 - operand3;
                break;
            case 6:
                operation1 = "-";
                operation2 = "*";
                solution = operand1 - operand2 * operand3;
                break;
            case 7:
                operation1 = "-";
                operation2 = "/";
                if (operand3 == 0)
                {
                    operand3 = 1;
                }

                solution = operand1 - operand2 / operand3;
                break;
            case 8:
                operation1 = "*";
                operation2 = "+";
                solution = operand1 * operand2 + operand3;
                break;
            case 9:
                operation1 = "*";
                operation2 = "-";
                solution = operand1 * operand2 - operand3;
                break;
            case 10:
                operation1 = "*";
                operation2 = "*";
                solution = operand1 * operand2 * operand3;
                break;
            case 11:
                operation1 = "*";
                operation2 = "/";
                if (operand3 == 0)
                {
                    operand3 = 1;
                }

                solution = operand1 * operand2 / operand3;
                break;
            case 12:
                operation1 = "/";
                operation2 = "+";
                if (operand2 == 0)
                {
                    operand2 = 1;
                }
                solution = operand1 / operand2 + operand3;
                break;
            case 13:
                operation1 = "/";
                operation2 = "-";
                if (operand2 == 0)
                {
                    operand2 = 1;
                }
                solution = operand1 / operand2 - operand3;
                break;
            case 14:
                operation1 = "/";
                operation2 = "*";
                if (operand2 == 0)
                {
                    operand2 = 1;
                }
                solution = operand1 / operand2 * operand3;
                break;
            case 15:
                operation1 = "/";
                operation2 = "/";
                if (operand2 == 0)
                {
                    operand2 = 1;
                }
                if (operand3 == 0)
                {
                    operand3 = 1;
                }

                solution = operand1 / operand2 / operand3;
                break;
        }
        return new Job
        {
            Difficulty = 2,
            Problem = $"CALCULATE: {operand1} {operation1} {operand2} {operation2} {operand3}",
            Solution = $"{solution:0.###}"
        };
    }
}

public class Job
{
    public int Difficulty { get; set; }
    public string Problem { get; set; }
    public string Solution { get; set; }
}
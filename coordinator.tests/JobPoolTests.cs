using System;
using System.Text.RegularExpressions;
using coordinator.Application;
using NUnit.Framework;

namespace coordinator.tests
{
    public class JobPoolTests
    {
        [TestCase(0, 0)]
        [TestCase(1, 0)]
        [TestCase(2, 0)]
        [TestCase(3, 0)]
        [TestCase(4, 0)]
        [TestCase(5, 0)]
        [TestCase(6, 0)]
        [TestCase(7, 0)]
        [TestCase(8, 0)]
        [TestCase(9, 0)]
        [TestCase(10, 1)]
        [TestCase(19, 1)]
        [TestCase(20, 2)]
        [TestCase(21, 2)]
        [TestCase(29, 2)]
        [TestCase(30, 3)]
        [TestCase(31, 3)]
        [TestCase(39, 3)]
        [TestCase(40, 4)]
        [TestCase(41, 4)]
        [TestCase(49, 4)]
        [TestCase(50, 5)]
        [TestCase(51, 5)]
        [TestCase(59, 5)]
        [TestCase(60, 6)]
        [TestCase(61, 6)]
        [TestCase(69, 6)]
        public void GetNextJob_gets_a_job_of_the_appropriate_difficulty(int previousCorrectResponses, int expected)
        {
            var jobPool = new JobPool();
            Assert.That(jobPool.GetNextJob(previousCorrectResponses).Difficulty, Is.EqualTo(expected));
        }

        [Test]
        public void GetNextJob_gets_a_job_of_difficulty_10_for_high_level_calculators()
        {
            var jobPool = new JobPool();
            Assert.That(jobPool.GetNextJob(850).Difficulty, Is.EqualTo(10));
        }

        [Test]
        [Repeat(1000)]
        public void GetNextJob_gets_a_generated_simple_problem()
        {
            var simpleProblemPattern = @"^CALCULATE: \d{1,4} [-+/*] \d{1,4}$";
            var simpleSolutionPattern = @"^-?\d+(\.\d{1,3})?$";

            var jobPool = new JobPool();
            var job = jobPool.GetNextJob(0);
            Assert.That(job.Difficulty, Is.EqualTo(0));
            Assert.That(Regex.IsMatch(job.Problem, simpleProblemPattern));
            Assert.That(Regex.IsMatch(job.Solution, simpleSolutionPattern));
        }

        [Test]
        [Repeat(1000)]
        public void GetNextJob_gets_a_generated_simple_problem_with_negative_numbers()
        {
            var negativeNumbersPattern = @"^CALCULATE: -?\d{1,4} [-+/*] -?\d{1,4}$";
            var simpleSolutionPattern = @"^-?\d+(\.\d{1,3})?$";

            var jobPool = new JobPool();
            var job = jobPool.GetNextJob(15);
            Assert.That(job.Difficulty, Is.EqualTo(1));
            Assert.That(Regex.IsMatch(job.Problem, negativeNumbersPattern));
            Assert.That(Regex.IsMatch(job.Solution, simpleSolutionPattern));
        }

        [Test]
        [Repeat(10000)]
        public void GetNextJob_gets_a_generated_3_operand_problem()
        {
            var threeOperandPattern = @"^CALCULATE: -?\d{1,4} [-+/*] -?\d{1,4} [-+/*] -?\d{1,4}$";
            var simpleSolutionPattern = @"^-?\d+(\.\d{1,3})?$";

            var jobPool = new JobPool();
            var job = jobPool.GetNextJob(25);
            Assert.That(job.Difficulty, Is.EqualTo(2));
            Assert.That(Regex.IsMatch(job.Problem, threeOperandPattern));
            Assert.That(Regex.IsMatch(job.Solution, simpleSolutionPattern));
        }
    }
}
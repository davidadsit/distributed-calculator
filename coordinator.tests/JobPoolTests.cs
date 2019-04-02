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
            Assert.That(jobPool.GetNextJob(70).Difficulty, Is.EqualTo(10));
        }
    }
}
using ClearMeasure.Bootcamp.Core.Services.Impl;
using NUnit.Framework;

namespace ClearMeasure.Bootcamp.UnitTests.Core.Services
{
    [TestFixture]
    public class NumberGeneratorTester
    {
        [Test]
        public void ShouldBeFiveInLength()
        {
            var generator = new NumberGenerator();
            string number = generator.GenerateNumber();

            Assert.That(number.Length, Is.EqualTo(5));
        }
    }
}
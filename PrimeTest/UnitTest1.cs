using PrimeNumbers;

namespace PrimeTest
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(11)]
        [InlineData(17)]
        public void TestPrime(int prime)
        {
            Assert.True(new PrimeFilter().IsPrime(prime));
        }
    }
}
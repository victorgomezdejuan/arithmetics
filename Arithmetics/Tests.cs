namespace ArithmeticsTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void OnlyParenthesis()
        {
            CryptoTransaction transaction = new("()");

            Assert.That(transaction.Result, Is.EqualTo(0));
        }

        [Test]
        public void SimpleSum()
        {
            CryptoTransaction transaction = new("( 1 + 1 )");

            Assert.That(transaction.Result, Is.EqualTo(2));
        }

        [Test]
        public void AnotherSum()
        {
            CryptoTransaction transaction = new("( 2 + 3 )");

            Assert.That(transaction.Result, Is.EqualTo(5));
        }
    }
}
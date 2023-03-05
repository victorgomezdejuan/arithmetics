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
    }
}
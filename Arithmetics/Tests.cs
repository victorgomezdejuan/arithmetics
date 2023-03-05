namespace ArithmeticsTests
{
    public class Tests
    {
        [Test]
        public void AssertExpressions()
        {
            AssertExpression("()", 0);
            AssertExpression("( 1 + 1 )", 2);
            AssertExpression("( 2 + 3 )", 5);
            AssertExpression("( 2 * 3 )", 6);
        }

        private static void AssertExpression(string expr, int value)
        {
            CryptoTransaction transaction = new(expr);
            Assert.That(transaction.Result, Is.EqualTo(value));
        }
    }
}
using ArithmeticsTests;

namespace Arithmetics;

public class Tests
{
    [Test]
    public void AssertExpressions()
    {
        AssertExpression("()", 0);
        AssertExpression("( 2 + 3 )", 5);
        AssertExpression("( 2 * 3 )", 6);
        AssertExpression("( 2 - 3 )", -1);
        AssertExpression("( 6 / 3 )", 2);
        AssertExpression("(())", 0);
        AssertExpression("( 1 + ( 2 + 3 ))", 6);
        AssertExpression("((1 * 2) + ( 2 + 3 ))", 7);
    }

    private static void AssertExpression(string expression, int value)
        => Assert.That(CryptoTransaction.Process(expression), Is.EqualTo(value));
}
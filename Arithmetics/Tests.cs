using ArithmeticsTests;

namespace Arithmetics;

public class Tests
{
    [Test]
    public void AssertExpressions()
    {
        AssertExpression("()", 0);
        //AssertExpression("( 2 + 3 )", 5);
        //AssertExpression("( 2 * 3 )", 6);
        //AssertExpression("( 2 - 3 )", -1);
        //AssertExpression("( 6 / 3 )", 2);
        //AssertExpression("(())", 0);
        //AssertExpression("( 1 + ( 2 + 3 ))", 6);
        //AssertExpression("((1 * 2) + ( 2 + 3 ))", 7);
        //AssertExpression("( 1 + ( ( 2 + 3 ) * (4 * 5) ) )", 101);
        //AssertExpression("( 5 * ( 4 * ( 3 * ( 2 * ( 1 * 9 ) / 8 - 7 ) + 6 ) ) )", -165);
    }

    private static void AssertExpression(string expression, int value)
        => Assert.That(CryptoTransaction.Process(expression), Is.EqualTo(value));
}
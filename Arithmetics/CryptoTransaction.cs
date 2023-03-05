namespace ArithmeticsTests;

internal class CryptoTransaction
{
    private readonly string _expression;

    public int Result => ProcessExpression();

    public CryptoTransaction(string expression)
        => _expression = expression;

    private int ProcessExpression()
    {
        return 0;
    }
}
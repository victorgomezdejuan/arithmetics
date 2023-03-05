namespace ArithmeticsTests;

internal class CryptoTransaction
{
    private readonly string _expression;

    public int Result => ProcessExpression();

    public CryptoTransaction(string expression)
        => _expression = expression;

    private int ProcessExpression()
    {
        int? leftOperand = null;
        int? rightOperand = null;

        for (int i = 0; i < _expression.Length; i++) {
            if (char.IsDigit(_expression[i])) {
                if (leftOperand is null) {
                    leftOperand = int.Parse(_expression[i].ToString());
                }
                else {
                    rightOperand = int.Parse(_expression[i].ToString());
                }
            }
        }

        return leftOperand.HasValue ? leftOperand.Value + rightOperand.Value : 0;
    }
}
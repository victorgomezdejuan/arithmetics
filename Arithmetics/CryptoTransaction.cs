namespace ArithmeticsTests;

internal class CryptoTransaction
{
    private readonly char[] _operators = new char[] { '+', '-', '*' };
    private readonly string _expression;

    public int Result => ProcessExpression();

    public CryptoTransaction(string expression)
        => _expression = expression;

    private int ProcessExpression()
    {
        int leftOperand = 0;
        int rightOperand = 0;
        char @operator = ' ';

        for (int i = 0; i < _expression.Length; i++) {
            if (char.IsDigit(_expression[i])) {
                if (@operator == ' ')
                    leftOperand = int.Parse(_expression[i].ToString());
                else
                    rightOperand = int.Parse(_expression[i].ToString());
            }
            else if (_operators.Contains(_expression[i])) {
                @operator = _expression[i];
            }
        }

        if (@operator == '+')
            return leftOperand + rightOperand;
        else if (@operator == '-')
            return leftOperand - rightOperand;
        else if (@operator == '*')
            return leftOperand * rightOperand;
        else
            return 0;
    }
}
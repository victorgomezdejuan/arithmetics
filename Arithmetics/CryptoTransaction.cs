using System.Text.RegularExpressions;

namespace ArithmeticsTests;

internal static class CryptoTransaction
{
    public static int Process(string expression)
    {
        expression = expression.Trim();

        if (expression.Length.Equals(1) && char.IsDigit(expression[0]))
            return int.Parse(expression[..1]);

        if (Regex.IsMatch(expression, @"^\(\s*(\d|\(.+\))\s*(\+|\-|\*|\/)\s*(\d|\(.+\))\s*\)$")) {
            MatchCollection matchCollection = Regex.Matches(expression, @"^\(\s*(\d|\(.+\))\s*(\+|\-|\*|\/)\s*(\d|\(.+\))\s*\)$");
            int leftOperand = Process(matchCollection[0].Groups[1].Value);
            char @operator = matchCollection[0].Groups[2].Value[0];
            int rightOperand = Process(matchCollection[0].Groups[3].Value);

            return EvaluateOperation(leftOperand, rightOperand, @operator);
        }

        return 0;
    }

    private static int EvaluateOperation(int leftOperand, int rightOperand, char @operator)
    {
        if (@operator == '-')
            return leftOperand - rightOperand;
        else if (@operator == '*')
            return leftOperand * rightOperand;
        else if (@operator == '/')
            return leftOperand / rightOperand;
        else
            return leftOperand + rightOperand;
    }
}
using System.Text.RegularExpressions;

namespace ArithmeticsTests;

internal static class CryptoTransaction
{
    public static int Process(string expression)
    {
        if (Regex.IsMatch(expression, @"^\s*\d\s*$"))
            return int.Parse(expression);

        if (Regex.IsMatch(expression, @"^\s*\(\s*\d\s*[\+|\-|\*|\/]\s*\d\s*\)\s*$")) {
            MatchCollection nestedExpression = Regex.Matches(expression, @"^\s*\(\s*(\d)\s*([\+|\-|\*|\/])\s*(\d)\s*\)\s*$");
            int leftOperand = int.Parse(nestedExpression[0].Groups[1].Value);
            char @operator = nestedExpression[0].Groups[2].Value.Trim()[0];
            int rightOperand = int.Parse(nestedExpression[0].Groups[3].Value);

            return EvaluateOperation(leftOperand, rightOperand, @operator);
        }

        if (Regex.IsMatch(expression, @"^\s*\(.+?[\+|\-|\*|\/].+\)\s*$")) {
            MatchCollection nestedExpression = Regex.Matches(expression, @"^\s*\((.+?)([\+|\-|\*|\/])(.+)\)\s*$");
            int leftOperand = Process(nestedExpression[0].Groups[1].Value);
            char @operator = nestedExpression[0].Groups[2].Value.Trim()[0];
            int rightOperand = Process(nestedExpression[0].Groups[3].Value);

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
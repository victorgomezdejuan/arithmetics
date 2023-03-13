using Arithmetics;
using Arithmetics.Node;

namespace ArithmeticsTests;

internal static class CryptoTransaction
{
    public static double Process(string expression) => ProcessNode(expression).GetValue();

    private static INode ProcessNode(string expression, INode node = null)
    {
        string processedExpression = expression.Trim();

        if (processedExpression.StartsWith("("))
            return ProcessNode(expression[1..], new OperationNode());

        if (processedExpression.StartsWith(")"))
            return node;

        return null;
    }
}
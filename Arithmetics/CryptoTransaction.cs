using Arithmetics;
using Arithmetics.Node;
using System.Text.RegularExpressions;
using static Arithmetics.Node.OperationNode;

namespace ArithmeticsTests;

internal static class CryptoTransaction
{
    private const string NumberRegex = @"^\d+(.\d+)?";
    private record Result(INode ProcessedNode, string RemainingExpression);

    public static double Process(string expression)
    {
        if (!expression.StartsWith("(") || !expression.EndsWith(")"))
            throw new Exception("All of the operations should be wrapped in parentheses");

        return ProcessNextItem(expression).ProcessedNode.CalculateValue();
    }

    private static Result ProcessNextItem(string expression)
    {
        string currentExpr = expression.Trim();

        if (OnlyParenthesesExpression(currentExpr))
            return new Result(new SingleValueNode(0), string.Empty);

        if (currentExpr.StartsWith("("))
            return ProcessExpressionBetweenParentheses(currentExpr);

        if (NextItemIsANumber(currentExpr))
            return ProcessNumber(currentExpr);

        return ProcessOperationStartingWithNumber(ref currentExpr);
    }

    private static Result ProcessOperationStartingWithNumber(ref string currentExpr)
    {
        Match match = Regex.Match(currentExpr, @"^(\d+(.\d+)?)\s*(\+|\-|\*|\/)");
        SingleValueNode leftOperand = new(double.Parse(match.Groups[1].Value));
        char opChar = match.Groups[3].Value[0];
        Operation operation = ParseOperation(opChar);
        currentExpr = currentExpr[(currentExpr.IndexOf(opChar) + 1)..];
        Result processedNode = ProcessNextItem(currentExpr);
        INode rightOperand = processedNode.ProcessedNode;
        currentExpr = processedNode.RemainingExpression.Trim();
        OperationNode operationNode = new();
        operationNode.Operations.Add(operation);
        operationNode.Elements.Add(leftOperand);
        operationNode.Elements.Add(rightOperand);

        while (Regex.IsMatch(currentExpr, @"^(\+|\-|\*|\/)")) {
            Match nextMatch = Regex.Match(currentExpr, @"^(\+|\-|\*|\/)");
            char nextOpChar = nextMatch.Groups[1].Value[0];
            Operation nextOperation = ParseOperation(nextOpChar);
            currentExpr = currentExpr[(currentExpr.IndexOf(nextOpChar) + 1)..];
            Result nextProcessedNode = ProcessNextItem(currentExpr);
            currentExpr = nextProcessedNode.RemainingExpression.Trim();
            operationNode.Operations.Add(nextOperation);
            operationNode.Elements.Add(nextProcessedNode.ProcessedNode);
        }

        return new Result(operationNode, currentExpr);
    }

    private static bool OnlyParenthesesExpression(string currentExpr) => Regex.IsMatch(currentExpr, @"^\(+\s*\)+");

    private static Result ProcessExpressionBetweenParentheses(string currentExpr)
    {
        Result result = ProcessNextItem(currentExpr[1..]);
        INode leftOperand1 = result.ProcessedNode;
        currentExpr = result.RemainingExpression.Trim();
        Match match1 = Regex.Match(currentExpr, @"^(\+|\-|\*|\/)");
        char opChar1 = match1.Groups[1].Value[0];
        Operation operation1 = ParseOperation(opChar1);
        currentExpr = currentExpr[(currentExpr.IndexOf(opChar1) + 1)..];
        Result processedNode1 = ProcessNextItem(currentExpr);
        INode rightOperand1 = processedNode1.ProcessedNode;
        currentExpr = processedNode1.RemainingExpression.Trim();
        OperationNode operationNode1 = new();
        operationNode1.Operations.Add(operation1);
        operationNode1.Elements.Add(leftOperand1);
        operationNode1.Elements.Add(rightOperand1);

        while (Regex.IsMatch(currentExpr, @"^(\+|\-|\*|\/)")) {
            Match nextMatch1 = Regex.Match(currentExpr, @"^(\+|\-|\*|\/)");
            char nextOpChar1 = nextMatch1.Groups[1].Value[0];
            Operation nextOperation1 = ParseOperation(nextOpChar1);
            currentExpr = currentExpr[(currentExpr.IndexOf(nextOpChar1) + 1)..];
            Result nextProcessedNode1 = ProcessNextItem(currentExpr);
            currentExpr = nextProcessedNode1.RemainingExpression.Trim();
            operationNode1.Operations.Add(nextOperation1);
            operationNode1.Elements.Add(nextProcessedNode1.ProcessedNode);
        }

        return new Result(operationNode1, currentExpr[1..]);
    }

    private static bool NextItemIsANumber(string currentExpr) => Regex.IsMatch(currentExpr, NumberRegex);

    private static Result ProcessNumber(string currentExpr)
    {
        Match numberMatch = Regex.Match(currentExpr, @"^\d+(.\d+)?");
        return new Result(new SingleValueNode(double.Parse(numberMatch.Groups[0].Value)), currentExpr[numberMatch.Groups[0].Value.Length..]);
    }

    private static Operation ParseOperation(char op) => op switch {
        '+' => Operation.Addition,
        '-' => Operation.Subtraction,
        '*' => Operation.Multiplication,
        '/' => Operation.Division,
        _ => throw new NotSupportedException(),
    };
}
using Arithmetics;
using Arithmetics.Node;
using System.Text.RegularExpressions;
using static Arithmetics.Node.OperationNode;

namespace ArithmeticsTests;

internal static class CryptoTransaction
{
    private const string NumberRegex = @"^\d+(.\d+)?";
    private const string OperationRegex = @"^(\+|\-|\*|\/)";

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

        return ProcessOperationStartingWithNumber(currentExpr);
    }

    private static Result ProcessOperationStartingWithNumber(string expression)
    {
        string currentExpr = expression;
        Match match = Regex.Match(currentExpr, @"^(\d+(.\d+)?)\s*(\+|\-|\*|\/)");
        SingleValueNode leftOperand = new(double.Parse(match.Groups[1].Value));
        char opChar = match.Groups[3].Value[0];
        currentExpr = currentExpr[currentExpr.IndexOf(opChar)..];

        return ProcessSameLevelOperations(currentExpr, leftOperand);
    }

    private static Result ProcessSameLevelOperations(string expression, INode leftOperand)
    {
        string currentExpr = expression;
        char opChar = currentExpr[0];
        Operation operation = ParseOperation(opChar);
        OperationNode operationNode = new();
        operationNode.Elements.Add(leftOperand);
        currentExpr = currentExpr[1..];
        Result nextOperandProcessResult = ProcessNextItem(currentExpr);
        currentExpr = nextOperandProcessResult.RemainingExpression.Trim();
        operationNode.Operations.Add(operation);
        operationNode.Elements.Add(nextOperandProcessResult.ProcessedNode);

        while (AreThereOperationAtTheSameLevel(currentExpr)) {
            char nextOpChar = currentExpr[0];
            Operation nextOperation = ParseOperation(nextOpChar);
            currentExpr = currentExpr[1..];
            Result nextOperandProcessResult2 = ProcessNextItem(currentExpr);
            currentExpr = nextOperandProcessResult2.RemainingExpression.Trim();
            operationNode.Operations.Add(nextOperation);
            operationNode.Elements.Add(nextOperandProcessResult2.ProcessedNode);
        }

        return new Result(operationNode, currentExpr);
    }

    private static bool AreThereOperationAtTheSameLevel(string currentExpr) => Regex.IsMatch(currentExpr, OperationRegex);

    private static bool OnlyParenthesesExpression(string expression) => Regex.IsMatch(expression, @"^\(+\s*\)+");

    private static Result ProcessExpressionBetweenParentheses(string expression)
    {
        string currentExpr = expression;
        Result result = ProcessNextItem(currentExpr[1..]);
        INode leftOperand = result.ProcessedNode;
        currentExpr = result.RemainingExpression.Trim();
        Result finalResult = ProcessSameLevelOperations(currentExpr, leftOperand);

        return new Result(finalResult.ProcessedNode, finalResult.RemainingExpression[1..]);
    }

    private static bool NextItemIsANumber(string expression) => Regex.IsMatch(expression, NumberRegex);

    private static Result ProcessNumber(string expression)
    {
        Match numberMatch = Regex.Match(expression, @"^\d+(.\d+)?");
        return new Result(new SingleValueNode(double.Parse(numberMatch.Groups[0].Value)), expression[numberMatch.Groups[0].Value.Length..]);
    }

    private static Operation ParseOperation(char op) => op switch {
        '+' => Operation.Addition,
        '-' => Operation.Subtraction,
        '*' => Operation.Multiplication,
        '/' => Operation.Division,
        _ => throw new NotSupportedException(),
    };
}
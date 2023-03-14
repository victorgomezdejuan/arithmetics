using Arithmetics;
using Arithmetics.Node;
using System.Text.RegularExpressions;
using static Arithmetics.Node.OperationNode;

namespace ArithmeticsTests;

internal static class CryptoTransaction
{
    public static double Process(string expression) => ProcessNode(expression).Item1.GetValue();

    private static Tuple<INode, string> ProcessNode(string expression)
    {
        string currentExpr = expression.Trim();

        if (Regex.IsMatch(currentExpr, @"^\(\s*\)"))
            return new Tuple<INode, string>(new SingleValueNode(0), currentExpr[(currentExpr.IndexOf(")") + 1)..]);

        if (currentExpr.StartsWith("(")) {
            Tuple<INode, string> result = ProcessNode(currentExpr[1..]);

            if (Regex.IsMatch(result.Item2, @"^\)"))
                return new Tuple<INode, string>(result.Item1, result.Item2[1..]);
            else {
                currentExpr = result.Item2.Trim();
                Match match1 = Regex.Match(currentExpr, @"^(\+|\-|\*|\/)");
                INode leftOperand1 = result.Item1;
                char opChar1 = match1.Groups[1].Value[0];
                Operation operation1 = ParseOperation(opChar1);
                currentExpr = currentExpr[(currentExpr.IndexOf(opChar1) + 1)..];
                Tuple<INode, string> processedNode1 = ProcessNode(currentExpr);
                INode rightOperand1 = processedNode1.Item1;
                currentExpr = processedNode1.Item2.Trim();
                OperationNode operationNode1 = new();
                operationNode1.Operations.Add(operation1);
                operationNode1.Elements.Add(leftOperand1);
                operationNode1.Elements.Add(rightOperand1);

                while (Regex.IsMatch(currentExpr, @"^(\+|\-|\*|\/)")) {
                    Match nextMatch1 = Regex.Match(currentExpr, @"^(\+|\-|\*|\/)");
                    char nextOpChar1 = nextMatch1.Groups[1].Value[0];
                    Operation nextOperation1 = ParseOperation(nextOpChar1);
                    currentExpr = currentExpr[(currentExpr.IndexOf(nextOpChar1) + 1)..];
                    Tuple<INode, string> nextProcessedNode1 = ProcessNode(currentExpr);
                    currentExpr = nextProcessedNode1.Item2.Trim();
                    operationNode1.Operations.Add(nextOperation1);
                    operationNode1.Elements.Add(nextProcessedNode1.Item1);
                }

                return new Tuple<INode, string>(operationNode1, currentExpr);
            }
        }

        if (Regex.IsMatch(currentExpr, @"^(\d+(.\d+)?)\s*\)")) {
            Match numberMatch = Regex.Match(currentExpr, @"^\d+(.\d+)?");
            return new Tuple<INode, string>(new SingleValueNode(double.Parse(numberMatch.Groups[0].Value)), currentExpr[currentExpr.IndexOf(")")..]);
        }

        Match match = Regex.Match(currentExpr, @"^(\d+(.\d+)?)\s*(\+|\-|\*|\/)");
        SingleValueNode leftOperand = new(double.Parse(match.Groups[1].Value));
        char opChar = match.Groups[3].Value[0];
        Operation operation = ParseOperation(opChar);
        currentExpr = currentExpr[(currentExpr.IndexOf(opChar) + 1)..];
        Tuple<INode, string> processedNode = ProcessNode(currentExpr);
        INode rightOperand = processedNode.Item1;
        currentExpr = processedNode.Item2.Trim();
        OperationNode operationNode = new();
        operationNode.Operations.Add(operation);
        operationNode.Elements.Add(leftOperand);
        operationNode.Elements.Add(rightOperand);

        while (Regex.IsMatch(currentExpr, @"^(\+|\-|\*|\/)")) {
            Match nextMatch = Regex.Match(currentExpr, @"^(\+|\-|\*|\/)");
            char nextOpChar = nextMatch.Groups[1].Value[0];
            Operation nextOperation = ParseOperation(nextOpChar);
            currentExpr = currentExpr[(currentExpr.IndexOf(nextOpChar) + 1)..];
            Tuple<INode, string> nextProcessedNode = ProcessNode(currentExpr);
            currentExpr = nextProcessedNode.Item2.Trim();
            operationNode.Operations.Add(nextOperation);
            operationNode.Elements.Add(nextProcessedNode.Item1);
        }

        return new Tuple<INode, string>(operationNode, currentExpr);
    }

    private static Operation ParseOperation(char op) => op switch {
        '+' => Operation.Addition,
        '-' => Operation.Subtraction,
        '*' => Operation.Multiplication,
        '/' => Operation.Division,
        _ => throw new NotSupportedException(),
    };
}
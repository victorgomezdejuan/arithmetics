namespace Arithmetics.Node;

internal class OperationNode : INode
{
    internal enum Operation
    {
        Multiplication,
        Division,
        Addition,
        Subtraction
    }

    private static readonly Dictionary<Operation, int> Preferences = new() {
        [Operation.Multiplication] = 1,
        [Operation.Division] = 1,
        [Operation.Addition] = 0,
        [Operation.Subtraction] = 0,
    };

    public OperationNode()
    {
        Elements = new();
        Operations = new();
    }

    public List<INode> Elements { get; }

    public List<Operation> Operations { get; }

    public double GetValue()
    {
        int nextOperationIndex = 0;

        while (Operations.Count > 0) {
            for (int i = 1; i < Operations.Count; i++) {
                if (Preferences[Operations[i]] > Preferences[Operations[nextOperationIndex]])
                    nextOperationIndex = i;
            }
            double leftOperand = Elements[nextOperationIndex * 2].GetValue();
            double rightOperand = Elements[nextOperationIndex * 2 + 1].GetValue();
            Operation operation = Operations[nextOperationIndex];
            double result = ExecuteOperation(leftOperand, rightOperand, operation);
            Operations.RemoveAt(nextOperationIndex);
            Elements.RemoveAt(nextOperationIndex * 2);
            Elements.RemoveAt(nextOperationIndex * 2 + 1);
            Elements.Insert(nextOperationIndex * 2, new SingleValueNode(result));
        }

        return Elements.Count > 0 ? Elements[0].GetValue() : 0;
    }

    private static double ExecuteOperation(double leftOperand, double rightOperand, Operation operation)
    {
        return operation switch {
            Operation.Multiplication => leftOperand * rightOperand,
            Operation.Division => leftOperand / rightOperand,
            Operation.Addition => leftOperand + rightOperand,
            Operation.Subtraction => leftOperand - rightOperand,
            _ => throw new NotSupportedException(),
        };
    }
}
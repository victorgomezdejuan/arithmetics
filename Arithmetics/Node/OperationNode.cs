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

    public double CalculateValue()
    {
        while (Operations.Count > 0) {
            int nextOperationIndex = NextOperationIndexByPriority();

            double leftOperand = Elements[nextOperationIndex].CalculateValue();
            double rightOperand = Elements[nextOperationIndex + 1].CalculateValue();
            Operation operation = Operations[nextOperationIndex];

            double result = ExecuteOperation(leftOperand, rightOperand, operation);

            Operations.RemoveAt(nextOperationIndex);
            Elements.RemoveRange(nextOperationIndex, 2);
            Elements.Insert(nextOperationIndex, new SingleValueNode(result));
        }

        return Elements[0].CalculateValue();
    }

    private int NextOperationIndexByPriority()
    {
        int nextOperationIndex = 0;
        for (int i = 1; i < Operations.Count; i++) {
            if (Preferences[Operations[i]] > Preferences[Operations[nextOperationIndex]])
                nextOperationIndex = i;
        }

        return nextOperationIndex;
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
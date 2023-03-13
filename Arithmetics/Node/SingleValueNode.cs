namespace Arithmetics;

internal class SingleValueNode : INode
{
    public SingleValueNode(double value) => Value = value;

    public double Value { get; }

    public double GetValue() => Value;
}

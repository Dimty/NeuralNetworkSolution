namespace Neuron;

public class Neuron
{
    private double _output;
    private double _input;
    public double Input { get=>_input; set=>_input = value; }
    public double Output
    {
        get => Sigmoid(_input);
        set => _output = value;
    }

    private NeuronType _type;

    public Neuron(int type)
    {
        if (type is -1 or 0 or 1) this._type = (NeuronType) type;
        else throw new ArgumentException($"Argument: ({nameof(type)} = {type}) is not equal to any valid value");
    }

    private static double Sigmoid(double x)
    {
        var result = 1 / (x + Math.Exp(-x));
        return result;
    }

    private enum NeuronType
    {
        Input=-1,
        Hidden,
        Output
    }
}
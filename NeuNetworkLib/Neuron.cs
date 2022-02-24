namespace Neuron;

public class Neuron
{
    private double _derivative;
    private double _input;
    private double _output;
    private double _sigmoid;
    

    public double Derivative { get; set; }
    public double Input { get=>_input; set=>_input = value; }

    public double Output
    {
        get { return _output; }
        set
        {
            if (_type == NeuronType.Input) _output = value;
            else
            {
                _output = Sigmoid(value);
                _derivative = DerSig(_output);
            }
        }

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
    private static double DerSig(double x)
    {
        var result = x * (1.0d - x);
        return result;
    }

    private enum NeuronType
    {
        Input=-1,
        Hidden,
        Output
    }
}
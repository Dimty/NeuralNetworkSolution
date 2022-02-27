namespace Neuron;
public class Neuron
{
    private double _derivative;
    private double _output;
    private double _delta;
    private double[] _inputWeight;
    

    public double Derivative { get; set; }
    public double Delta
    {
        get => _delta;
        set
        {
            if (_type != NeuronType.Input) _delta = value;
        }
    }
    public double Input { get; set; } = 0;

    public double Output
    {
        get => _output;
        set
        {
            if (_type == NeuronType.Input) _output = value;
            else
            {
                _output = Sigmoid(value);
                Derivative = DerSig(_output);
            }
        }
    }

    private NeuronType _type;

    public Neuron(int type, int size)
    {
        if (type is -1 or 0 or 1) this._type = (NeuronType) type;
        else throw new ArgumentException($"Argument: ({nameof(type)} = {type}) is not equal to any valid value");
        if(type is not -1) FillArrayWeight(size);
    }

    private void FillArrayWeight(int size)
    {
        _inputWeight = new double[size];
        for (int i = 0; i < size; i++)
        {
            _inputWeight[i] = new Random().NextDouble() - 0.5d;
            //_inputWeight[i] = 0.5;
        }
    }

    public double this[int index]
    {
        get => _inputWeight[index];
        set => _inputWeight[index] = value;
    }
    private static double Sigmoid(double x)
    {
        var result = 1 / (1 + Math.Exp(-x));
        return result;
    }
    private static double DerSig(double x)
    {
        var result = x * (1.0d - x);
        return result;
    }

    public override string ToString()
    {
        string res = $"Delta={_delta} | Der={_derivative} | Output={_output}";
        return res;


        foreach (var item in _inputWeight)
        {
            res += item.ToString() + '\n';
        }

        return res;
    }

    private enum NeuronType
    {
        Input=-1,
        Hidden,
        Output
    }
}
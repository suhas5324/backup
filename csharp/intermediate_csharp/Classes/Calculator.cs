
public class Calculator : ICalculator
{
    
    private double _result;

    public int Add(int a, int b)
    {
        _result = a + b;
        return a + b;
    }

    public int Add(int a, int b, int c)
    {
        _result = a + b + c;
        return a + b + c;
    }

    public double Add(double a, double b)
    {
        _result = a + b;
        return a + b;
    }

    public virtual double GetResult()
    {
        return _result;
    }
    public void SetResult(double result)
    {
        _result = result;
    }
}

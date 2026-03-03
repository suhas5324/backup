public class Calculator : ICalculator
{
    protected static double result;
    public int Add(int a, int b)
    {
        result = a + b;
        return (int)result;
    }
    public int Add(int a, int b, int c)
    {
        
        result = a + b + c;
        return (int)result;
    }
    public double Add(double a, double b)
    {
        result = a + b;
        return result;
    }
    public virtual double GetResult()
    {
        return result;
    }
    protected void SetResult(double value)
    {
        result = value;
    }
}

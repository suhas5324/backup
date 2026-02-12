public class AdvancedCalculator : Calculator, IAdvancedCalculator
{
    public double Power(int baseValue, int exponent)
    {
        double total = Math.Pow(baseValue, exponent);
        SetResult(total);
        return total;
    }

    public override double GetResult()
    {
        return base.GetResult() * 1_000_000;
    }
}

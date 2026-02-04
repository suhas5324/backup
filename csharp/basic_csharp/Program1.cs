class Program1
{
   public static void CalculateSumUntilExit()
    {
        int sum=0;
        while(true)
        {
            Console.WriteLine("Enter a number to add to the sum or 'ok' to exit:");
            string? input=Console.ReadLine();
            if(input?.ToLower()=="ok")
            {
                break;
            }
            else
            {
                try
                {
                    int number=Convert.ToInt32(input);
                    sum+=number;
                }
                catch(FormatException)
                {
                    Console.WriteLine("Please enter a valid integer or 'ok' to exit.");
                }
            }
        }
        Console.WriteLine("The sum is: " + sum);
    }
}
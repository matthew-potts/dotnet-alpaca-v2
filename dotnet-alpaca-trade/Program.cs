using System;

namespace dotnet_alpaca_trade
{
    public class Progam
    {
        public static void Main()
        {
            var alpacaAccount = new AlpacaAccount();

            Console.WriteLine($"{alpacaAccount.AlpacaAccountConfiguration}");

            Console.ReadLine();
        }
    }

}
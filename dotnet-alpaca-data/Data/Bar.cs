using System;
using Alpaca.Markets;

namespace dotnet_alpaca_data
{
	public class Bar : IBar
	{
		
        public string Symbol => Symbol; // these are 'expression-bodied members', not lambda. 

        public DateTime TimeUtc => TimeUtc;

        public string Date => TimeUtc.ToShortDateString();

        public decimal Open => Open;

        public decimal High => High;

        public decimal Low => Low;

        public decimal Close => Close;

        public decimal Volume => Volume;

        public decimal Vwap => Vwap;

        public ulong TradeCount => TradeCount;
    }
}


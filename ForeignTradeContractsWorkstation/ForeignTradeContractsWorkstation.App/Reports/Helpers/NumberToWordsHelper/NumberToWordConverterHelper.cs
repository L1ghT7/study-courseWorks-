using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ForeignTradeContractsWorkstation.App.Reports.Helpers.NumberToWordsHelper.View_Models;
using TsSoft.Orthography.Numbers;

namespace ForeignTradeContractsWorkstation.App.Reports.Helpers
{
    public static class NumberToWordConverterHelper
    {
        public static CurrencyViewModel ConvertCurrencyToWords(decimal number)
        {
            var converter = NumbersToWordsConverterFactory.CreateRussianConverter();
            var inWords = converter.ConvertCurrency(number);
            var viewModel = DeriveToViewModel(inWords);
            return viewModel;

        }

        private static CurrencyViewModel DeriveToViewModel(string converted)
        {
            var index = converted.IndexOf("руб");
            var number = converted.Remove(index);
            var coins = Regex.Match(converted, @"\d+").Value;

            return new CurrencyViewModel() {Number = number, Coins = coins};
        }

    }
}

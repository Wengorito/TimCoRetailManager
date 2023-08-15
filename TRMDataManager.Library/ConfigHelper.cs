using System.Configuration;

namespace TRMDataManager.Library
{
    public static class ConfigHelper
    {
        public static decimal GetTaxRate()
        {
            string taxRate = ConfigurationManager.AppSettings["taxRate"];

            bool isValidTaxRate = decimal.TryParse(taxRate, out decimal output);

            if (!isValidTaxRate)
            {
                throw new ConfigurationErrorsException("The tax rate is not set up properly");
            }

            return output;
        }
    }
}

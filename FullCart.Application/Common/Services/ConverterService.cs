using FullCart.Application.Common.Interfaces;

namespace FullCart.Application.Common.Services
{
    public class ConverterService : IConverterService
    {
        public DateTime? GetXLDateTime(object value)
        {
            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                return (DateTime)value;

            return null;
        }

        public decimal? GetDecimal(string value)
        {
            decimal vOut;
            if (decimal.TryParse(value, out vOut))
                return vOut;

            return null;
        }

        public double? GetDouble(string value)
        {
            double vOut;
            if (double.TryParse(value, out vOut))
                return vOut;

            return null;
        }

        public int? GetInt(string value)
        {
            int vOut;
            if (int.TryParse(value, out vOut))
                return vOut;

            return null;
        }

        public long? GetLong(string value)
        {
            long vOut;
            if (long.TryParse(value, out vOut))
                return vOut;

            return null;
        }
    }
}

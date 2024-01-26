namespace FullCart.Application.Common.Interfaces
{
    public interface IConverterService
    {
        int? GetInt(string value);
        long? GetLong(string value);
        decimal? GetDecimal(string value);
        double? GetDouble(string value);
        DateTime? GetXLDateTime(object value);
    }
}

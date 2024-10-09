using System.Globalization;

namespace EFCoreQueryCachingDemo.Extensions;

public static class DateTimeExtensions
{
  public static string ToSqlDate(this DateTime self)
  {
    return self.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
  }

  public static string ToSqlDateShort(this DateTime self)
  {
    return self.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
  }

  public static string ToSqlDateTime(this DateTime self)
  {
    return self.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
  }
}
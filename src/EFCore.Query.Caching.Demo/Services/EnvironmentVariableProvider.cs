namespace EFCoreQueryCachingDemo.Services;

	public static class EnvironmentVariableProvider
	{
		public static T GetSetting<T>(string name, object defValue) 
		{
			var value = Environment.GetEnvironmentVariable(name);
			if (string.IsNullOrEmpty(value))
			{
				return (T)Convert.ChangeType(defValue, typeof(T));
			}
			return (T)Convert.ChangeType(value, typeof(T));
		}
	}

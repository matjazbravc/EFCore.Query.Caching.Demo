namespace EFCoreQueryCachingDemo.Services.Configuration
{
	public class EasyCachingConfig
	{
		/// <summary>
		/// Get or sets whether null values should be cached, default is false.
		/// </summary>
		public bool CacheNulls { get; set; } = false;
		
		/// <summary>Gets or sets a value indicating whether enable logging.</summary>
		/// <value><c>true</c> if enable logging; otherwise, <c>false</c>.</value>
		public bool EnableLogging { get; set; } = false;

		/// <summary>
		/// Gets or sets the expiration timeout in seconds, default is 300000 (5 mins)
		/// </summary>
		/// <value>The sleep ms.</value>
		public int ExpirationTimeoutMs { get; set; } = 300000;

		/// <summary>
		/// Gets or sets the lock ms.
		/// mutex key's alive time(ms), default is 5000
		/// </summary>
		/// <value>The lock ms.</value>
		public int LockMs { get; set; } = 5000;

		/// <summary>Gets or sets the max random second.</summary>
		/// <remarks>
		/// If this value greater then zero, the seted cache items' expiration will add a random second
		/// This is mainly for preventing Cache Crash
		/// </remarks>
		/// <value>The max random second.</value>
		public int MaxRdSecond { get; set; } = 120;

		/// <summary>
		/// Gets or sets the sleep ms.
		/// when mutex key alive, it will sleep some time, default is 300
		/// </summary>
		/// <value>The sleep ms.</value>
		public int SleepMs { get; set; } = 300;

		/// <summary>
		/// Gets or sets a value indicating whether this allow admin.
		/// </summary>
		/// <value><c>true</c> if allow admin; otherwise, <c>false</c>.</value>
		public bool DbConfig_AllowAdmin { get; set; }

		/// <summary>Gets or sets the timeout for any connect operations.</summary>
		/// <value>The connection timeout.</value>
		public int DbConfig_ConnectionTimeout { get; set; } = 5000;

		/// <summary>
		/// Endpoint to be used to connect to the Redis server.
		/// </summary>
		public string DbConfig_Endpoint { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to use SSL encryption.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is SSL; otherwise, <c>false</c>.
		/// </value>
		public bool DbConfig_IsSsl { get; set; } = false;

		/// <summary>
		/// Gets or sets the password to be used to connect to the Redis server.
		/// </summary>
		/// <value>The password.</value>
		public string DbConfig_Password { get; set; }

		/// <summary>
		/// Redis server port number
		/// </summary>
		public int DbConfig_Port { get; set; }

		/// <summary>
		/// Gets or sets the SSL Host.
		/// If set, it will enforce this particular host on the server's certificate.
		/// </summary>
		/// <value>The SSL host.</value>
		public string DbConfig_SslHost { get; set; } = string.Empty;
	}
}

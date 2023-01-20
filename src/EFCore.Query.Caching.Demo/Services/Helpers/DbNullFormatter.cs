using MessagePack;
using MessagePack.Formatters;

namespace EFCoreQueryCachingDemo.Services.Helpers
{
	public class DbNullFormatter : IMessagePackFormatter<DBNull>
	{
		public static DbNullFormatter Instance = new();

		private DbNullFormatter()
		{
		}

		public void Serialize(ref MessagePackWriter writer, DBNull value, MessagePackSerializerOptions options)
		{
			// always serialize as nil (if present, it's never null)
			writer.WriteNil();
		}

		public DBNull Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
		{
			return DBNull.Value;
		}
	}
}

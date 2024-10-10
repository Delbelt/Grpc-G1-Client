using Google.Protobuf.WellKnownTypes;

namespace clientGrpc.Handlers
{
    public class TimestampGrpcConverter
    {
        public static DateTime ConvertToDateTime(Timestamp timestamp)
        {
            return timestamp.ToDateTime();
        }
        public static DateTime ToDateTime(dateProto.Date date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }

        public static string ToFormattedString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }
    }
}

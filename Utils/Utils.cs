namespace Utils
{
    public class Utils
    {
        public static bool IsValidUUId(Guid id)
        {
            return id != Guid.Empty;
        }
    }
}
namespace SalesManagement.MvcApplication
{
    public class PreprocessorHelper
    {
        public static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}
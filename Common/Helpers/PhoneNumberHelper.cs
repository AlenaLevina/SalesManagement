namespace Common.Helpers
{
    public class PhoneNumberHelper
    {
        public static bool IsPhoneNumber(string number)
        {
            number=number.Replace("-", "");
            ulong parseResult;
            return ulong.TryParse(number, out parseResult);
        }
    }
}

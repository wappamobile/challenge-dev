using System.Linq;

namespace WappaMobile.ChallengeDev
{
    public static class Assertion
    {
        public static bool IsNumeric(object value)
        {
            return value.ToString().All(x => "0123456789".Contains(x));
        }

        public static bool IsAllNumbers(object values)
        {
            foreach(var i in values.ToString().ToCharArray())
            {
                if(!IsNumeric(i))
                    return false;
            }

            return true;
        }

        public static bool IsLetter(object value)
        {
            return value.ToString().ToLower().All(x => "abcdefghijklmnopqrstuvwxyz ".Contains(x));
        }

        public static bool IsAllLetters(object values)
        {
            foreach(var i in values.ToString().ToCharArray())
            {
                if(!IsLetter(i))
                    return false;
            }

            return true;
        }
    }
}

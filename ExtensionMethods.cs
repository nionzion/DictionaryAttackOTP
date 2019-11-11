using System;
namespace dictionaryAttackOTP
{
    public static class ExtensionMethods
    {
        
        public static string ToText(this int[] bits)
        {
            var str = "";
            foreach (var bit in bits)
                str += bit.ToString();

            return str;
        }
    }
}

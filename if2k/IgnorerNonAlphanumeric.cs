using System;

namespace InternetFilter.If2k.Kernel

{
    public class IgnorerNonAlphanumeric : Ignorer

    {

        public bool isIgnored(char c)

        {
            return !Char.IsLetterOrDigit(c);
        }
    }
}

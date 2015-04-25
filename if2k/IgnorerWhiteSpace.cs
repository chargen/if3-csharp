using System;

namespace InternetFilter.If2k.Kernel

{

    public class IgnorerWhiteSpace : Ignorer

    {

        public bool isIgnored(char c)

        {
            return Char.IsWhiteSpace(c);
        }
    }
}

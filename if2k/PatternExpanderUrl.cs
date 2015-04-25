using System;
using System.IO;
using System.Diagnostics;

using InternetFilter.Log;

namespace InternetFilter.If2k.Kernel

{
    public class PatternExpanderUrl : PatternExpanderPhrase
    {

        Ignorer ignorer;
        String common_prefix;

        public PatternExpanderUrl(PatternExpanderTarget target) : base(target)
        {
            ignorer = new IgnorerWhiteSpace();
            //common_prefix = "[,www.]";
            common_prefix = "";
        }

        public PatternExpanderUrl() : base(null)
        {
        }

        public override void add(String prefix, String pattern, TreeFlag flags)
        {
            base.add(common_prefix + fixUrlString(prefix), fixUrlString(pattern), flags);
        }

        public override bool remove(String prefix, String pattern )
        {
            return base.remove(common_prefix + fixUrlString(prefix), fixUrlString(pattern) );
        }

        private String fixUrlString(String s)
        {
            String result = "";
            for (int i = 0; i < s.Length; ++i)
            {
                char c = s[i];
                if (!ignorer.isIgnored(s[i]))
                {
                    result += c;
                }
            }
            return result;
        }
    }
}

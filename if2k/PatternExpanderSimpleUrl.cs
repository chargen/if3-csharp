using System;
using System.IO;
using System.Diagnostics;

using InternetFilter.Log;

namespace InternetFilter.If2k.Kernel

{
    public class PatternExpanderSimpleUrl : PatternExpander
    {

        public PatternExpanderSimpleUrl(PatternExpanderTarget target) : base(target)
        {
        }

        public PatternExpanderSimpleUrl()
        {
        }

        public override void add(String prefix, String pattern, TreeFlag flags)
        {
            String real_pattern = prefix + pattern;
            if (real_pattern.StartsWith("http://www."))
            {
                real_pattern = real_pattern.Substring(11);
            } else if (real_pattern.StartsWith("http://"))
              {
                  real_pattern = real_pattern.Substring(7);
              }
            // Note: We leave all other prefixes as is. We want to know about other
            // subdomains besides www and we want to
            // explicitly allows specialization of searches with https:// prefix.

            getTarget().add(real_pattern, flags);
        }

        public override bool remove(String prefix, String pattern)
        {
            String real_pattern = prefix + pattern;
            if (real_pattern.StartsWith("http://www."))
            {
                real_pattern = real_pattern.Substring(11);
            } else if (real_pattern.StartsWith("http://"))
              {
                  real_pattern = real_pattern.Substring(7);
              }
            // Note: We leave all other prefixes as is. We want to know about other
            // subdomains besides www and we want to
            // explicitly allows specialization of searches with https:// prefix.

            return getTarget().remove(real_pattern);
        }

    }
}

using System;
using System.IO;
using System.Diagnostics;

using InternetFilter.Log;

namespace InternetFilter.If2k.Kernel

{
    public class PatternExpanderSimple : PatternExpander
    {

        public PatternExpanderSimple(PatternExpanderTarget target) : base(target)
        {
        }

        public PatternExpanderSimple()
        {
        }

        public override void add(String prefix, String pattern, TreeFlag flags)
        {
            getTarget().add(prefix + pattern, flags);
        }

        public override bool remove(String prefix, String pattern)
        {
            return getTarget().remove(prefix + pattern);
        }

    }
}

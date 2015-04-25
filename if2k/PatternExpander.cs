using System;
using System.IO;
using System.Diagnostics;

using InternetFilter.Log;

namespace InternetFilter.If2k.Kernel

{
    public abstract class PatternExpander
    {

        public PatternExpander(PatternExpanderTarget target)
        {
            this.target = target;
        }

        public PatternExpander()
        {
            this.target = null;
        }

        public void add(String pattern, TreeFlag flags)
        {
            add("", pattern, flags);
        }

        public bool remove(String pattern)
        {
            return remove("",pattern);
        }

        public abstract void add(String prefix, String pattern, TreeFlag flags);

        public abstract bool remove(String prefix, String pattern );

        public PatternExpanderTarget getTarget()
        {
            return this.target;
        }

        public void setTarget(PatternExpanderTarget target)
        {
            this.target = target;
        }
        private PatternExpanderTarget target;
    }

}

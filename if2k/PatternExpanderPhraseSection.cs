using System;
using System.IO;
using System.Diagnostics;

using System.Collections.Generic;
using InternetFilter.Log;

namespace InternetFilter.If2k.Kernel

{
    public class PatternExpanderPhraseSection
    {

        LinkedList<String> strings;
        PatternExpanderPhraseSection next;

        public PatternExpanderPhraseSection()
        {
            strings = new LinkedList<String>();
            next = null;
        }

        public void clear()
        {
            strings.Clear();
            next = null;
        }

        public PatternExpanderPhraseSection getNext()
        {
            return next;
        }

        public LinkedList<String> getStrings()
        {
            return strings;
        }

        public void addSection(PatternExpanderPhraseSection section)
        {
            if (next == null)
            {
                next = section;
            } else
              {
                  next.addSection(section);
              }
        }

        public void addString(String s)
        {
            strings.AddLast(s);
        }

        public void expandAllToTarget(String prefix, PatternExpanderTarget target,
                                      TreeFlag flag)
        {
            // do we have any strings to contribute to the phrase?
            if (strings.Count > 0)
            {
                // yes, iterate through them
                LinkedList<String>.Enumerator s_it = strings.GetEnumerator();
                while (s_it.MoveNext() )
                {
                    // get the string but prefix it with prefix
                    String v = prefix + s_it.Current;
                    // add the resultant string to the target
                    addToTarget(v, target, flag);
                }
            } else
              {
                  addToTarget(prefix, target, flag);
              }
        }

        private void addToTarget(String s, PatternExpanderTarget target, TreeFlag flag)
        {
            // no, do we have a section after us?
            if (next != null)
            {
                // yes, give the next section this request
                next.expandAllToTarget(s, target, flag);
            } else
              {
                  // no, tell the target to add this string.
                  target.add(s, flag);
              }

        }

        public bool expandAllToTargetRemove(String prefix, PatternExpanderTarget target)
        {
            bool r=false;
            // do we have any strings to contribute to the phrase?
            if (strings.Count > 0)
            {
                // yes, iterate through them
                LinkedList<String>.Enumerator s_it = strings.GetEnumerator();
                while (s_it.MoveNext())
                {
                    // get the string but prefix it with prefix
                    String v = prefix + s_it.Current;
                    // add the resultant string to the target
                    r |= removeFromTarget(v, target);
                }
            } else
              {
                  r|=removeFromTarget(prefix, target);
              }
            return r;
        }

        private bool removeFromTarget(String s, PatternExpanderTarget target)
        {
            bool r = false;
            // no, do we have a section after us?
            if (next != null)
            {
                // yes, give the next section this request
                r|=next.expandAllToTargetRemove(s, target);
            } else
              {
                  // no, tell the target to add this string.
                  r|=target.remove(s);
              }
            return r;
        }

    }
}

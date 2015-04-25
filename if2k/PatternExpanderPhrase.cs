using System;
using System.IO;
using System.Diagnostics;

using InternetFilter.Log;

namespace InternetFilter.If2k.Kernel

{
    public class PatternExpanderPhrase : PatternExpander
    {

        PatternExpanderPhraseSection sections;

        public PatternExpanderPhrase(PatternExpanderTarget target) : base(target)
        {
            sections = new PatternExpanderPhraseSection();
        }

        public PatternExpanderPhrase() : base(null)
        {
        }

        public override void add(String prefix, String pattern, TreeFlag flags)
        {

            sections.clear();

            String s = prefix + pattern;
            if (s.Length > 0)
            {
                if (s[0] == '[')
                {

                    addSections(s);

                    // now iterate through all combinations of the section list
                    // and call target().add() with them.

                    iterateAllCombinations(flags);

                } else
                  {
                      // no optional sections here, just add the string
                      getTarget().add(s, flags);
                  }
            }
        }

        public override bool remove(String prefix, String pattern )
        {

            sections.clear();

            String s = prefix + pattern;
            if (s.Length > 0)
            {
                if (s[0] == '[')
                {

                    addSections(s);

                    // now iterate through all combinations of the section list
                    // and call target().add() with them.

                    return iterateAllCombinationsRemove();
                } else
                  {
                      // no optional sections here, just remove the string
                      return getTarget().remove(s);
                  }
            }
            return true;
        }

        private void iterateAllCombinations(TreeFlag flags)
        {
            sections.expandAllToTarget("", getTarget(), flags);
        }

        private bool iterateAllCombinationsRemove()
        {
            return sections.expandAllToTargetRemove("", getTarget() );
        }

        private void addSections(String s)
        {
            // if the phrase starts with an open bracket, then there are
            // optional sections separated by commas. Get the whole bracketed text and
            // pass it to addSection and repeat.
            String cur = "";
            bool in_section = false;
            for (int i = 0; i < s.Length; ++i)
            {
                if (s[i] == '[')
                {
                    // '[' found, this is the beginning of a section
                    in_section = true;
                } else if (in_section == true && s[i] == ']')
                  {
                      // ']' found while in a section, so submit sections to addSection()
                      addSectionEntries(cur);
                      // clear cur string
                      cur = "";
                      // no longer in brackets
                      in_section = false;
                  } else
                    {
                        cur += s[i];
                    }
            }
            addSectionEntries(cur);
        }

        private void addSectionEntries(String s)
        {

            // create a new section
            PatternExpanderPhraseSection section = new PatternExpanderPhraseSection();

            // create a new string to put in the section
            String cur = "";

            // go through the text, grabbing characters and adding strings to
            // the section when ',' is found

            for (int i = 0; i < s.Length; ++i)
            {
                if (s[i] == ',')
                {
                    // found a ',' so submit the built string to the section.
                    section.addString(cur);
                    cur = "";
                } else
                  {
                      // no ',' yet, so we are building a string
                      cur += s[i];
                  }
            }
            // cur contains the last string built without ending with ',' so
            // we need to add it here

            section.addString(cur);

            // append this entire section to the end of our section list
            sections.addSection(section);
        }
    }
}

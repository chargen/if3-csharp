using System;
using System.IO;
using System.Diagnostics;

namespace InternetFilter.If2k.Kernel

{
    public class Scanner

    {

        /**
         *
         * Construct a Scanner object, attached to a Tree and a PrefixTester object
         *
         * @param t
         *            Tree to use
         * @param prefix_tester
         *            PrefixTester to use
         */
        public Scanner(Tree t, PrefixTester prefix_tester,
                       PatternExpander pattern_expander, TreeLoader loader)

        {
            this.tree = t;
            this.prefix_tester = prefix_tester;
            this.pattern_expander = pattern_expander;
            this.loader = loader;
        }

        public void Load(TextReader reader, String prefix, TreeFlag flag)

        {
            if (reader != null)

            {
                loader.Load(reader, pattern_expander, prefix, flag );
            }
        }

        /**
         *
         * Call scanBuffer() to see if a buffer contains any potential matches. Any
         * matches are given to the ScannerTarget specified. The count of matches is
         * returned.
         *
         * @param buf
         *            Buffer to scan
         * @param target
         *            ScannerTarget to notify when any matches are found
         * @return int count of matches
         */
        public int ScanBuffer(String buf, ScannerTarget target)

        {
            int match_count = 0;
            for (int i = 0; i < buf.Length; ++i)

            {
                // do a test on first character or on any other character where the
                // previous
                // character is matched by the prefix tester object
                bool needs_testing = (i == 0);
                if (i != 0)

                {
                    needs_testing = prefix_tester.isPrefix(buf[i - 1]);
                }

                // TODO: prefix_tester's result is currently ignored

                if (true)

                {
                    // find the longest matching word in a tree
                    TreeNode n = tree.findLongest(buf, i);
                    if (n != null)

                    {
                        target.matchFound(this, n);
                        match_count++;
                    }
                }
            }
            return match_count;
        }

        /**
         *
         * Get the tree that is used for Scanning.
         *
         * @return Tree
         */
        public Tree GetTree()

        {
            return tree;
        }

        /**
         *
         * Set a new tree to use for scanning
         *
         * @param t
         *            Tree
         */
        public void SetTree(Tree t)

        {
            tree = t;
        }

        public PatternExpander GetPatternExpander()

        {
            return pattern_expander;
        }
        private Tree tree;
        private PrefixTester prefix_tester;
        private PatternExpander pattern_expander;
        private TreeLoader loader;
    }
}

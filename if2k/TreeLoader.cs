using System;
using System.IO;
using System.Diagnostics;

namespace InternetFilter.If2k.Kernel

{
    public class TreeLoader

    {
        public void Load(System.IO.TextReader reader, PatternExpander pattern_expander, String prefix, TreeFlag flag)

        {
            String line;

            while ((line = reader.ReadLine()) != null)

            {
                if (line.Length > 4)

                {
                    pattern_expander.add(prefix, line, flag);
                }
            }
        }
    }
}

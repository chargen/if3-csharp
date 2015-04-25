using System;
using System.IO;
using System.Diagnostics;

using InternetFilter.Log;

namespace InternetFilter.If2k.Kernel

{
    public interface TreeAction
    {
        void matchFound(Context c, Tree t, TreeNode n);
    }
}

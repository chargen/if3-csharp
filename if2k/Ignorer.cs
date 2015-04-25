using System;
namespace InternetFilter.If2k.Kernel

{
    public interface Ignorer

    {
        bool isIgnored(char c);
    }
}

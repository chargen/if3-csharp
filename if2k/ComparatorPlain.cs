using System;
namespace InternetFilter.If2k.Kernel

{
    public class ComparatorPlain : Comparator

    {
        public bool isEqual(char a, char b)

        {
            return a == b;
        }
    }
}

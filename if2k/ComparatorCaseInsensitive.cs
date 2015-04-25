using System;
namespace InternetFilter.If2k.Kernel

{
    public class ComparatorCaseInsensitive : Comparator

    {
        public bool isEqual(char a, char b)

        {
            return Char.ToUpper(a) == Char.ToUpper(b);
        }
    }
}

using System;
namespace InternetFilter.If2k.Kernel

{
    public class ComparatorWildcard : Comparator

    {
        private Comparator chained;

        public ComparatorWildcard(Comparator chained)

        {
            this.chained = chained;
        }

        public bool isEqual(char a, char b)

        {
            bool r = false;
            if (a == '?' || chained.isEqual(a, b))

            {
                r = true;
            }
            return r;
        }
    }
}


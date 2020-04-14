using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelFor2020
{
    class ParallelSummator
    {
        private int N = 1677721600;
        private class Sum
        {
            public long value;
        }
        private Sum res;
        public long Result
        {
            get { return res.value; }
            private set { res.value = value; } 
        }

        private int parts = 4;

        public ParallelSummator()
        {
            res = new Sum();
            Result = 0;
        }

        private void _Summ(int part)
        {
            // 0 1 2 3 4 5 6 | 7 8  9 10 11 12
            // 1 2 3 4 5 6 7 | 8 9 10 11 12 13
            int partsSize = (int) N / parts; //3
            int ost = N - partsSize * parts; //2
            int st = part * partsSize + ((part<ost)?part:ost);
            int fn = (part+1)*partsSize + ((part+1<ost)?part:(ost-1));
            long s = 0;
            for (int i = st; i <= fn; i++)
            {
                s += 1;
            }
            Monitor.Enter(res);
            try
            {
                Result += s;
            }
            finally
            {
                Monitor.Exit(res);
            }
        }

        public void Summ()
        {
            Parallel.For(
                0, 
                parts, 
                new Action<int>(_Summ)
            );
        }
    }
}

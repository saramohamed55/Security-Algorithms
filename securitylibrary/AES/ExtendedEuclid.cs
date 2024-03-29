﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{
    public class ExtendedEuclid 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="baseN"></param>
        /// <returns>Mul inverse, -1 if no inv</returns>
        public int GetMultiplicativeInverse(int number, int baseN)
        {
            List<int> q = new List<int>();
            List<int> A1 = new List<int>();
            List<int> A2 = new List<int>();
            List<int> A3 = new List<int>();
            List<int> B1 = new List<int>();
            List<int> B2 = new List<int>();
            List<int> B3 = new List<int>();
            q.Add(55555);
            A1.Add(1);
            A2.Add(0);
            A3.Add(baseN);
            B1.Add(0);
            B2.Add(1);
            B3.Add(number);
            int the_inverse = 0;
            int x = A1[0];
            int y = x;
            while (true)
            {
                int qi = A3[A3.Count() - 1] / B3[B3.Count() - 1];
                q.Add(qi);
                A1.Add(B1[B1.Count() - 1]);
                A2.Add(B2[B2.Count() - 1]);
                A3.Add(B3[B3.Count() - 1]);
                B1.Add(A1[A1.Count() - 2] - q[q.Count() - 1] * B1[B1.Count() - 1]);
                B2.Add(A2[A2.Count() - 2] - q[q.Count() - 1] * B2[B2.Count() - 1]);
                B3.Add(A3[A3.Count() - 2] - q[q.Count() - 1] * B3[B3.Count() - 1]);



                if (B3[B3.Count() - 1] == 1)
                {
                    the_inverse = B2[B2.Count() - 1];
                    break;
                }
                else if (B3[B3.Count() - 1] == 0)
                {
                    return -1;
                }


            }
            if (the_inverse % baseN < 0)
            {
                return (the_inverse % baseN) + baseN;
            }
            else
            {
                return (the_inverse % baseN);
            }


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.ElGamal
{
    public class ElGamal
    {
        /// <summary>
        /// Encryption
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="q"></param>
        /// <param name="y"></param>
        /// <param name="k"></param>
        /// <returns>list[0] = C1, List[1] = C2</returns>
        public List<long> Encrypt(int q, int alpha, int y, int k, int m)
        {
            
            long c1 =(long) Getres(alpha, k, q);
            int K = Getres(y, k, q);
            long c2 = (long)Getres(K * m, 1, q);
            List<long> ans=new List<long>();
            ans.Add(c1);
            ans.Add(c2);
            return ans;
        }
        public int Decrypt(int c1, int c2, int x, int q)
        { 
            int K = Getres(c1, x, q);
            //to get inverse K
           
            int invK = GetMultiplicativeInverse(K, q);
            int ans = (int)((long)c2 * invK % q);
            return ans;
        }
       
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
        public int Getres(int a, int b, int c)
        {
            int res = 1;
            for (int i = 0; i < b; i++)
            {
                res *= a;
                res %= c;
            }
            return res;
        }


    }
}

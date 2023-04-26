using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DiffieHellman
{
    public class DiffieHellman 
    {
        public List<int> GetKeys(int q, int alpha, int xa, int xb)
        {//every public power private mod q
         //public key= generator power private key mod q
            if (q == 1) return null;
              int p1 = Getres(alpha, xa, q);
              int p2 = Getres(alpha, xb, q);
              List<int> ans = new List<int>();
              int K1 = Getres(p1, xb, q);
              ans.Add(K1);
              int K2 = Getres(p2, xa, q);
              ans.Add(K2);
              return ans;

        }

      //  where a is num , b is power and c is modules
        public int Getres(int a, int b, int c)
        {   if (c == 1) return 0;
            if (b < 0)
            {
                b = 1 / b;
                b = -b;
            }
            int res = 1;
            a %= c;
            int count = 0;
           
            while (count < b)
            {
                res = res * a;
                res %= c;
                count++;
            }


            return res % c;
        }
    }
}

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
              int p1 = Getres(alpha, xa, q);
              int p2 = Getres(alpha, xb, q);
              List<int> ans = new List<int>();
              int K1 = Getres(p1, xb, q);
              ans.Add(K1);
              int K2 = Getres(p2, xa, q);
              ans.Add(K2);
              return ans;

        }
        public int Getres(int a, int b, int c)
        {
            int res = 1;
            for (int i = 0; i < b; i++)
            {
                res = res * a;
                res %= c;
            }
            return res;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        public string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }

        public struct Matrics
        {
            public Dictionary<char, Tuple<int, int>> litters;
            public List<List<char>> row;

        }
        public string Encrypt(string plainText, string key)
        {
            //check conditions of plain Text(duplicated,length)
            for(int i = 0; i < plainText.Length - 1; i += 2)
            {
                if (plainText[i] == plainText[i + 1])
                {
                    plainText = plainText.Substring(0, i + 1) + 'x' + plainText.Substring(i + 1);

                }
            }
            if (plainText.Length % 2 != 0) plainText += 'x';
            ///////////initialize key
            HashSet<char> Table = new HashSet<char>();
            string alpha= "abcdefghjklmnopqrstuvwxyz";//put j instead of i
            for(int i = 0; i < key.Length; i++)
            {
                if (key[i] == 'i')
                {
                    Table.Add('j');
                }
                else
                {
                    Table.Add(key[i]);
                }

            }
            //continue the table

            for(int i = 0; i < 25; i++)
            {
                Table.Add(alpha[i]);
            }
            Matrics m = new Matrics();
            //matrics 5x5 with alphabet
            m = MakeMatrics(Table);
            string ans = "";
            for(int i = 0; i < plainText.Length; i += 2)
            {//same row
                if (m.litters[plainText[i]].Item1 == m.litters[plainText[i + 1]].Item1)
                {
                    ans += m.row[m.litters[plainText[i]].Item1][(m.litters[plainText[i]].Item2 + 1) % 5];
                    ans += m.row[m.litters[plainText[i + 1]].Item1 + 1][(m.litters[plainText[i + 1]].Item2 + 1) % 5];

                }
                //same colm 
                else if (m.litters[plainText[i]].Item2==m.litters[plainText[i+1]].Item2)
                {
                    ans += m.row[(m.litters[plainText[i]].Item1 + 1) % 5][m.litters[plainText[i]].Item2];
                    ans += m.row[(m.litters[plainText[i+1]].Item1 + 1) % 5][m.litters[plainText[i+1]].Item2];

                }

                //else
                else
                {
                    ans += m.row[m.litters[plainText[i]].Item1][m.litters[plainText[i]].Item2 ];
                    ans += m.row[m.litters[plainText[i + 1]].Item1 ][m.litters[plainText[i + 1]].Item2 ];


                }
            }
            return ans.ToUpper();
           

        }
        public Matrics MakeMatrics(HashSet<char> set)
        {
            Dictionary<char, Tuple<int, int>> mat = new Dictionary<char, Tuple<int, int>>();
            List<List<char>> row = new List<List<char>>();
            int counter = 0;
            for (int i = 0; i < 5; i++)
            {
                List<char> tmp = new List<char>();
                for (int j = 0; j < 5; j++)
                {
                    if (counter < 25)
                    {
                        mat.Add(set.ElementAt(counter), new Tuple<int, int>(i, j));
                        tmp.Add(set.ElementAt(counter));
                        counter++;
                    }
                }

                row.Add(tmp);
            }

            Matrics m = new Matrics();
            m.litters = mat;
            m.row = row;

            return m;
        }
    }
}

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
            cipherText = cipherText.ToLower();
            //start hash the key
            HashSet<char> Table = new HashSet<char>();
            string alpha = "abcdefghiklmnopqrstuvwxyz";//put i instead of j
            for (int i = 0; i < key.Length; i++)
            {
                if (key[i] == 'j')
                {
                    Table.Add('i');
                }
                else
                {
                    Table.Add(key[i]);
                }

            }
            //continue the table

            for (int i = 0; i < 25; i++)
            {
                Table.Add(alpha[i]);
            }
            Matrics m = new Matrics();
            //matrics 5x5 with alphabet
            m = MakeMatrics(Table);
            string Plaintxt = "";
            for (int i = 0; i < cipherText.Length - 1; i += 2)
            { //same clm
                if (m.letters[cipherText[i]].Item2==m.letters[cipherText[i+1]].Item2)
                {
                    Plaintxt += m.row[(m.letters[cipherText[i]].Item1+4)%5][m.letters[cipherText[i]].Item2];
                    Plaintxt += m.row[(m.letters[cipherText[i+1]].Item1 + 4) % 5][m.letters[cipherText[i + 1]].Item2];

                }
                //same row
                else if (m.letters[cipherText[i]].Item1 == m.letters[cipherText[i + 1]].Item1)
                {
                    //i need index before the char (-1 from the index)
                    Plaintxt += m.row[m.letters[cipherText[i]].Item1][(m.letters[cipherText[i]].Item2+4)%5];
                    Plaintxt += m.row[m.letters[cipherText[i + 1]].Item1][(m.letters[cipherText[i + 1]].Item2 +4) % 5];

                }
                //else
                else
                {
                    Plaintxt += m.row[m.letters[cipherText[i]].Item1][m.letters[cipherText[i + 1]].Item2];
                    Plaintxt += m.row[m.letters[cipherText[i + 1]].Item1][m.letters[cipherText[i]].Item2];
                }

            }

            //remove X

            string ans = Plaintxt;
            if (Plaintxt[Plaintxt.Length - 1] == 'x')
            {
                ans = ans.Remove(Plaintxt.Length - 1);
            }
            //while removing x ->lenght is changing so we had to add (update term) to keep length
            int Update = 0;
            for (int i = 0; i < ans.Length; i++)
            {
                if (Plaintxt[i] == 'x')
                {
                    //(i-1)%2==0 to check that there is two char after and before him
                    
                    if ((Plaintxt[i - 1] == Plaintxt[i + 1])&& (i - 1) % 2 == 0)
                    {
                            ans = ans.Remove(i+Update, 1);
                        
                            Update--;
                      
                    }
                }
            }

            return ans;

        }

        public struct Matrics
        {
            public Dictionary<char, Tuple<int, int>> letters;
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
            string alpha= "abcdefghiklmnopqrstuvwxyz";//put i instead of j
            for(int i = 0; i < key.Length; i++)
            {
                if (key[i] == 'j')
                {
                    Table.Add('i');
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
            for(int i = 0; i < plainText.Length-1; i += 2)
            {
                
                //same colm 
                 if (m.letters[plainText[i]].Item2==m.letters[plainText[i+1]].Item2)
                {
                    ans += m.row[(m.letters[plainText[i]].Item1 + 1) % 5][m.letters[plainText[i]].Item2];
                    ans += m.row[(m.letters[plainText[i+1]].Item1 + 1) % 5][m.letters[plainText[i+1]].Item2];

                }
                //same row
                else if (m.letters[plainText[i]].Item1 == m.letters[plainText[i + 1]].Item1)
                {
                    ans += m.row[m.letters[plainText[i]].Item1][(m.letters[plainText[i]].Item2 + 1) % 5];
                    ans += m.row[m.letters[plainText[i + 1]].Item1][(m.letters[plainText[i + 1]].Item2 + 1) % 5];

                }

                //else
                else
                {
                    //same row of first char and same colm to the second char 
                   
                    ans += m.row[m.letters[plainText[i]].Item1][m.letters[plainText[i+1]].Item2 ];
                    ans += m.row[m.letters[plainText[i + 1]].Item1 ][m.letters[plainText[i]].Item2 ];


                }
            }
            return ans.ToUpper();
           

        }
        public Matrics MakeMatrics(HashSet<char> set)
        {
            Dictionary<char, Tuple<int, int>> mat = new Dictionary<char, Tuple<int, int>>();
            List<List<char>> rows = new List<List<char>>();
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

                rows.Add(tmp);
            }

            Matrics m = new Matrics();
            m.letters = mat;
            m.row = rows;

            return m;
        }
    }
}

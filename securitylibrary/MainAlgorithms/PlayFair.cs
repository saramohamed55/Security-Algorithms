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



        }
    }
}

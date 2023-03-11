using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        
        public string Analyse(string plainText, string cipherText)
        {
            Console.WriteLine("The analysis class : ");
            cipherText = cipherText.ToLower();
            Dictionary<Char, Char> dict = new Dictionary<char, char>();
            char sValue = 'a';
            char cValue = 'A';
            for (int i = 0; i < 26; i++)
            {

                dict[sValue] = cValue;
                cValue++;
                sValue++;
            }
            for (int i = 0; i < plainText.Length; i++)
            {
                dict[plainText[i]] = cipherText[i];
            }
            string key = "";

            var keysList = dict.Keys.ToList();
            keysList.Sort();

            foreach (var keys in keysList)
            {
                key += dict[keys];
            }
            Console.Write("The key is : "); Console.Write(key);
            return key;
            //throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, string key)
        {
            string[] encr = new string[26];
            Console.WriteLine("The decryption class : ");
            string decryptedWord = "";
            char character = 'a';
            cipherText = cipherText.ToLower();
            Dictionary<Char, Char> dict = new Dictionary<char, char>();
            for (int i = 0; i < key.Length; i++)
            {
                dict.Add(key[i], character);
                //Console.Write(x);
                character++;
            }
            for (int i = 0; i < cipherText.Length; i++)
            {
                char val = cipherText[i];
                decryptedWord += dict[val];
            }
            Console.Write("The encrypted word : "); Console.Write(cipherText);
            Console.Write("\nThe real word : "); Console.Write(decryptedWord);
            
            return decryptedWord;
            //throw new NotImplementedException();
        }

        public string Encrypt(string plainText, string key)
        {
            string[] decr = new string[26];
            Console.WriteLine("The encryption class : ");
            string encryptedWord = "";
            char character = 'a';
            Dictionary<Char, Char> mp = new Dictionary<char, char>();
            for (int i = 0; i < key.Length; i++)
            {
                mp.Add(character, key[i]);
                character++;
            }
            for (int i = 0; i < plainText.Length; i++)
            {
                char value = plainText[i];
                encryptedWord += mp[value];
            }
            Console.Write("The real word : "); Console.Write(plainText);
            Console.Write("\nThe encrypted word : "); Console.Write(encryptedWord);
            return encryptedWord;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            string[] characters = new string[26];
            string word = "";
            Dictionary<int, char> dict = new Dictionary<int, char>();
            dict.Add(1, 'E');
            dict.Add(2, 'T');
            dict.Add(3, 'A');
            dict.Add(4, 'O');
            dict.Add(5, 'I');
            dict.Add(6, 'N');
            dict.Add(7, 'S');
            dict.Add(8, 'R');
            dict.Add(9, 'H');
            dict.Add(10, 'L');
            dict.Add(11, 'D');
            dict.Add(12, 'C');
            dict.Add(13, 'U');
            dict.Add(14, 'M');
            dict.Add(15, 'F');
            dict.Add(16, 'P');
            dict.Add(17, 'G');
            dict.Add(18, 'W');
            dict.Add(19, 'Y');
            dict.Add(20, 'B');
            dict.Add(21, 'V');
            dict.Add(22, 'K');
            dict.Add(23, 'X');
            dict.Add(24, 'J');
            dict.Add(25, 'Q');
            dict.Add(26, 'Z');
            Dictionary<Char, int> count_dict = new Dictionary<char, int>();
            for (char a = 'A'; a <= 'Z'; a++)
            {
                count_dict.Add(a, 0);
            }
            for (int i = 0; i < cipher.Length; i++)
            {
                count_dict[cipher[i]]++;
            }

            var items = from pair in count_dict
                        orderby pair.Value descending
                        select pair;
            int n = 1;
            Dictionary<char, char> keyMap = new Dictionary<char, char>();
            // keymap key=> chiper, value=> main cahr 
            foreach (KeyValuePair<char, int> pair in items)
            {
                keyMap.Add(pair.Key, dict[n]);
                n++;
            }
            for (int i = 0; i < cipher.Length; i++)
            {
                word += keyMap[cipher[i]];
            }

            Console.Write("The word : "); Console.Write(word);
            return word;
            //throw new NotImplementedException();
        }
    }
}

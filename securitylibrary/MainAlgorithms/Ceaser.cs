using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        public static char cipherCharacter(char ch, int key)
        {
            char offset = char.IsUpper(ch) ? 'A' : 'a';
            char newCh = (char)((((ch + key) - offset) % 26) + offset);
            return newCh;
        }

        char[] characters = new char[25];
        string[] words = new string[25];

        public string Encrypt(string plainText, int key)
        {
            Console.WriteLine("The encryption class : ");
            string output = "";
            foreach (char ch in plainText)
            {                
                output += cipherCharacter(ch,key);
            }

            

            Console.WriteLine("The text is : " + output);
            return output;
            //throw new NotImplementedException();
        }

        string[] character = new string[26];

        public string Decrypt(string cipherText, int key)
        {
            Console.WriteLine("The decryption class : ");
            return Encrypt(cipherText, 26 - key);
            //throw new NotImplementedException();
        }

        public int Analyse(string plainText, string cipherText)
        {
            Console.WriteLine("The analysis class : ");
            int key = 0;
            string resultPlain = Encrypt(plainText, key);

            Console.WriteLine("Real inputs : ");
            Console.WriteLine(cipherText);
            Console.WriteLine("My inputs");
            Console.WriteLine(resultPlain);
            Console.WriteLine("----------------------");
            Console.WriteLine("The loop is running");
            Console.WriteLine("----------------------");


            while (resultPlain != cipherText.ToLower() && key < 26)
            {
                key++;
                resultPlain = Encrypt(plainText, key);
                Console.WriteLine(resultPlain);
                Console.WriteLine("---------------");
            }
            Console.WriteLine(key);
            return key;
            

            //throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class AES : CryptographicTechnique
    {
          int Nb = 4; 
          int Nk;
          int Nr; 
         

        private readonly byte[] Sbox = new byte[256] {
    //0     1    2      3     4    5     6     7      8    9     A      B    C     D     E     F
    0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76, //0
    0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0, //1
    0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15, //2
    0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75, //3
    0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84, //4
    0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf, //5
    0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8, //6
    0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2, //7
    0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73, //8
    0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb, //9
    0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79, //A
    0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08, //B
    0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a, //C
    0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e, //D
    0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf, //E
    0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16 }; //F

   private readonly byte[] InvSbox = new byte[256]{
             0x52, 0x09, 0x6a, 0xd5, 0x30, 0x36, 0xa5, 0x38, 0xbf, 0x40, 0xa3, 0x9e, 0x81, 0xf3, 0xd7, 0xfb,
    /*1*/  0x7c, 0xe3, 0x39, 0x82, 0x9b, 0x2f, 0xff, 0x87, 0x34, 0x8e, 0x43, 0x44, 0xc4, 0xde, 0xe9, 0xcb,
    /*2*/  0x54, 0x7b, 0x94, 0x32, 0xa6, 0xc2, 0x23, 0x3d, 0xee, 0x4c, 0x95, 0x0b, 0x42, 0xfa, 0xc3, 0x4e,
    /*3*/  0x08, 0x2e, 0xa1, 0x66, 0x28, 0xd9, 0x24, 0xb2, 0x76, 0x5b, 0xa2, 0x49, 0x6d, 0x8b, 0xd1, 0x25,
    /*4*/  0x72, 0xf8, 0xf6, 0x64, 0x86, 0x68, 0x98, 0x16, 0xd4, 0xa4, 0x5c, 0xcc, 0x5d, 0x65, 0xb6, 0x92,
    /*5*/  0x6c, 0x70, 0x48, 0x50, 0xfd, 0xed, 0xb9, 0xda, 0x5e, 0x15, 0x46, 0x57, 0xa7, 0x8d, 0x9d, 0x84,
    /*6*/  0x90, 0xd8, 0xab, 0x00, 0x8c, 0xbc, 0xd3, 0x0a, 0xf7, 0xe4, 0x58, 0x05, 0xb8, 0xb3, 0x45, 0x06,
    /*7*/  0xd0, 0x2c, 0x1e, 0x8f, 0xca, 0x3f, 0x0f, 0x02, 0xc1, 0xaf, 0xbd, 0x03, 0x01, 0x13, 0x8a, 0x6b,
    /*8*/  0x3a, 0x91, 0x11, 0x41, 0x4f, 0x67, 0xdc, 0xea, 0x97, 0xf2, 0xcf, 0xce, 0xf0, 0xb4, 0xe6, 0x73,
    /*9*/  0x96, 0xac, 0x74, 0x22, 0xe7, 0xad, 0x35, 0x85, 0xe2, 0xf9, 0x37, 0xe8, 0x1c, 0x75, 0xdf, 0x6e,
    /*a*/  0x47, 0xf1, 0x1a, 0x71, 0x1d, 0x29, 0xc5, 0x89, 0x6f, 0xb7, 0x62, 0x0e, 0xaa, 0x18, 0xbe, 0x1b,
    /*b*/  0xfc, 0x56, 0x3e, 0x4b, 0xc6, 0xd2, 0x79, 0x20, 0x9a, 0xdb, 0xc0, 0xfe, 0x78, 0xcd, 0x5a, 0xf4,
    /*c*/  0x1f, 0xdd, 0xa8, 0x33, 0x88, 0x07, 0xc7, 0x31, 0xb1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xec, 0x5f,
    /*d*/  0x60, 0x51, 0x7f, 0xa9, 0x19, 0xb5, 0x4a, 0x0d, 0x2d, 0xe5, 0x7a, 0x9f, 0x93, 0xc9, 0x9c, 0xef,
    /*e*/  0xa0, 0xe0, 0x3b, 0x4d, 0xae, 0x2a, 0xf5, 0xb0, 0xc8, 0xeb, 0xbb, 0x3c, 0x83, 0x53, 0x99, 0x61,
    /*f*/  0x17, 0x2b, 0x04, 0x7e, 0xba, 0x77, 0xd6, 0x26, 0xe1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0c, 0x7d };

      
       public override string Decrypt(string cipherText, string key)
        {
            byte[] cipherBytes = null;

            if (cipherText.StartsWith("0x"))
            {
                cipherBytes = HexStringToByteArray(cipherText.Substring(2));
            }
            else
            {
                cipherBytes = Encoding.UTF8.GetBytes(cipherText);
            }

            byte[] expandedKey = null;

            if (key.StartsWith("0x"))
            {
                expandedKey = ExpandKey(HexStringToByteArray(key.Substring(2)));
            }
            else
            {
                expandedKey = ExpandKey(Encoding.UTF8.GetBytes(key));
            }

            byte[,] state = new byte[4, Nb];

            for (int i = 0; i < 4 * Nb; i++)
            {
                state[i % 4, i / 4] = cipherBytes[i];
            }

            // AddRoundKey - initial round
            state = AddRoundKey(state, GetRoundKey(expandedKey, Nr));

            for (int round = Nr - 1; round >= 1; round--)
            {
                // InvShiftRows - undo the ShiftRows step from encryption
                InvShiftRows(state);

               
                InvSubBytes(state);

                // AddRoundKey
                state = AddRoundKey(state, GetRoundKey(expandedKey, round ));

                // InvMixColumns - undo the MixColumns step from encryption
                
                  state = InvMixColumns(state);
                
            }
            // InvShiftRows - undo the ShiftRows step from encryption
            InvShiftRows(state);

            // InvSubBytes - undo the SubBytes step from encryption
            InvSubBytes(state);

            // AddRoundKey - final round
            state = AddRoundKey(state, GetRoundKey(expandedKey, 0));


            byte[] output = new byte[4 * Nb];
            for (int i = 0; i < 4 * Nb; i++)
            {
                output[i] = state[i % 4, i / 4];
            }

            return "0x" + BitConverter.ToString(output).Replace("-", "");

        }
       


        public override string Encrypt(string plainText, string key)
        {
            
            byte[] input = null;
            byte[] expandedKey = null;

            // Convert input and key to byte arrays
            if (plainText.StartsWith("0x"))
            {
                input = HexStringToByteArray(plainText.Substring(2));
            }
            else
            {
                input = Encoding.UTF8.GetBytes(plainText);
            }

            if (key.StartsWith("0x"))
            {
                expandedKey = ExpandKey(HexStringToByteArray(key.Substring(2)));
            }
            else
            {
                expandedKey = ExpandKey(Encoding.UTF8.GetBytes(key));
            }
            byte[,] state = new byte[4, Nb];
           
          
            for (int i = 0; i < 4 * Nb; i++)
            {
                state[i % 4, i / 4] = input[i];
            }

            // Add round key
            state = AddRoundKey(state, GetRoundKey(expandedKey, 0));

            // Perform Nr rounds of the AES algorithm
            for (int round = 1; round <= Nr; round++)
            {
                 SubBytes(state,Sbox);
                 ShiftRows(state);
                if (round < Nr)
                {
                     MixColumns(state);
                }
                state = AddRoundKey(state, GetRoundKey(expandedKey, round));
            }

            // Convert output to string
            byte[] output = new byte[4 * Nb];
            for (int i = 0; i < 4 * Nb; i++)
            {
                output[i] = state[i % 4, i / 4];
            }

            return "0x" + BitConverter.ToString(output).Replace("-", "");
        

    }
        private byte[] ExpandKey(byte[] key)
        {
            Nk = key.Length / 4;
            Nr = Math.Max(Nb, Nk) + 6;
            int expandedKeyLength = 4 * Nb * (Nr + 1);
            byte[] expandedKey = new byte[expandedKeyLength];
            byte[] temp = new byte[4]; 
            int i = 0;

            while (i < Nk)
            {
                Array.Copy(key, 4 * i, expandedKey, 4 * i, 4);
                i++;
            }

            i = Nk;

            while (i < Nb * (Nr + 1))
            {
                for (int j = 0; j < 4; j++)
                {
                    temp[j] = expandedKey[(i - 1) * 4 + j];
                }

                if (i % Nk == 0)
                {
                    temp = SubWord(RotWord(temp));
                    temp[0] ^= Rcon(i / Nk);
                }
                else if (Nk > 6 && i % Nk == 4)
                {
                    temp = SubWord(temp);
                }

                for (int j = 0; j < 4; j++)
                {
                    expandedKey[i * 4 + j] = (byte)(expandedKey[(i - Nk) * 4 + j] ^ temp[j]);
                }

                i++;
            }

            return expandedKey;
        }
        private byte[] SubWord(byte[] word)
        {
            for (int i = 0; i < 4; i++)
            {
                word[i] = Sbox[word[i]];
            }
            return word;
        }

        private byte[] RotWord(byte[] word)
        {
            byte temp = word[0];
            word[0] = word[1];
            word[1] = word[2];
            word[2] = word[3];
            word[3] = temp;
            return word;
        }
        private void InvSubBytes(byte[,] state)
        {
            int numRows = state.GetLength(0);
            int numCols = state.GetLength(1);

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    state[row, col] = InvSbox[state[row, col]];
                }
            }
        }

        // Shifts the rows of the state array to the right
        private static void InvShiftRows(byte[,] state)
        {
            int numRows = state.GetLength(0);
            int numCols = state.GetLength(1);

            for (int row = 1; row < numRows; row++)
            {
                for (int i = 0; i < row; i++)
                {
                    byte temp = state[row, numCols - 1];
                    for (int col = numCols - 1; col > 0; col--)
                    {
                        state[row, col] = state[row, col - 1];
                    }
                    state[row, 0] = temp;
                }
            }
        }

        private byte[,] InvMixColumns(byte[,] state)
        {

            int numRows = state.GetLength(0);
            int numCols = state.GetLength(1); byte[,] result = new byte[numRows, numCols];

            for (int col = 0; col < numCols; col++)
            {

                result[0, col] = (byte)(Multiply(state[0, col], 0x0E) ^ Multiply(state[1, col], 0x0B) ^ Multiply(state[2, col], 0x0D) ^ Multiply(state[3, col], 0x09));

                result[1, col] = (byte)(Multiply(state[0, col], 0x09) ^ Multiply(state[1, col], 0x0E) ^ Multiply(state[2, col], 0x0B) ^ Multiply(state[3, col], 0x0D));

                result[2, col] = (byte)(Multiply(state[0, col], 0x0D) ^ Multiply(state[1, col], 0x09) ^ Multiply(state[2, col], 0x0E) ^ Multiply(state[3, col], 0x0B));


                result[3, col] = (byte)(Multiply(state[0, col], 0x0B) ^ Multiply(state[1, col], 0x0D) ^ Multiply(state[2, col], 0x09) ^ Multiply(state[3, col], 0x0E));
            }

            return result;
        }


        private byte[] GetRoundKey(byte[] expandedKey, int round)
        {
            
            int startIndex = round * 4 * Nb;
            byte[] roundKey = new byte[4 * Nb];

            for (int i = 0; i < 4 * Nb; i++)
            {
                roundKey[i] = expandedKey[startIndex + i];
            }

            return roundKey;

        }
      
        private byte[,] AddRoundKey(byte[,] state, byte[] roundKey)
        {
            int roundKeyLength = roundKey.Length;
            int rowIndex = 0;
            int colIndex = 0;

            for (int i = 0; i < roundKeyLength; i++)
            {
                state[rowIndex, colIndex] ^= roundKey[i];
                rowIndex++;
                if (rowIndex == 4)
                {
                    rowIndex = 0;
                    colIndex++;
                }
            }

            return state;
        }
     
        public void SubBytes(byte[,] state, byte[] Sbox)
        {
            int rows = state.GetLength(0);
            int cols = state.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    state[i, j] = Sbox[state[i, j]];
                }
            }
        }
        public static byte[] HexStringToByteArray(string hex)
        {
            if (hex.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                hex = hex.Substring(2);
            }


            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }
        public void ShiftRows(byte[,] state)
        {
            int rows = state.GetLength(0);
            int cols = state.GetLength(1);

            for (int i = 1; i < rows; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    byte temp = state[i, 0];
                    for (int k = 1; k < cols; k++)
                    {
                        state[i, k - 1] = state[i, k];
                    }
                    state[i, cols - 1] = temp;
                }
            }
        }

        public void MixColumns(byte[,] state)
        {
            int rows = state.GetLength(0);
            int cols = state.GetLength(1);

            for (int i = 0; i < cols; i++)
            {
                byte[] col = new byte[rows];
                for (int j = 0; j < rows; j++)
                {
                    col[j] = state[j, i];
                }

                state[0, i] = (byte)(Multiply(col[0], 0x02) ^ Multiply(col[1], 0x03) ^ col[2] ^ col[3]);
                state[1, i] = (byte)(col[0] ^ Multiply(col[1], 0x02) ^ Multiply(col[2], 0x03) ^ col[3]);
                state[2, i] = (byte)(col[0] ^ col[1] ^ Multiply(col[2], 0x02) ^ Multiply(col[3], 0x03));
                state[3, i] = (byte)(Multiply(col[0], 0x03) ^ col[1] ^ col[2] ^ Multiply(col[3], 0x02));
            }
        }

       

       
        private  byte Rcon(int index)
        {
            byte result = 1;

            for (int i = 1; i < index; i++)
            {
                result = Multiply(result, 2);
            }

            return result;
        }

          private byte Multiply(byte a, byte b)
          {
              byte result = 0;
              byte high_bit_mask = 0x80;
              byte modulo_mask = 0x1B;

              for (int i = 0; i < 8; i++)
              {
                  if ((b & 1) == 1)
                  {
                      result ^= a;
                  }

                  bool high_bit_set = (a & high_bit_mask) == high_bit_mask;
                  a <<= 1;

                  if (high_bit_set)
                  {
                      a ^= modulo_mask;
                  }

                  b >>= 1;
              }

              return result;
          }

    }
}

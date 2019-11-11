using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using dictionaryAttackOTP;

namespace dictionaryAttackOTP
{
    public class BitArray
    {
        public static int[] ToBits(int decimalnumber, int numberofbits)
        {
            int[] bitarray = new int[numberofbits];
            int k = numberofbits - 1;
            char[] bd = Convert.ToString(decimalnumber, 2).ToCharArray();

            for (int i = bd.Length - 1; i >= 0; --i, --k)
            {
                if (bd[i] == '1')
                    bitarray[k] = 1;
                else
                    bitarray[k] = 0;
            }

            while (k >= 0)
            {
                bitarray[k] = 0;
                --k;
            }

            return bitarray;
        }

        public static int ToDecimal(int[] bitsarray)
        {
            string stringvalue = "";
            for (int i = 0; i < bitsarray.Length; i++)
            {
                stringvalue += bitsarray[i].ToString();
            }
            int DecimalValue = Convert.ToInt32(stringvalue, 2);

            return DecimalValue;
        }
    }

    class MainClass
    {
        private static int[] XOROperation(int[] array1, int[] array2, int[] array3, int SizeOfTheArray)
        {
            for (int i = 0; i < SizeOfTheArray; i++)
            {
                array3[i] = array1[i] ^ array2[i];
            }
            return array3;
        }

        static string ToBinaryString(Encoding encoding, string text)
        {
            return string.Join("", encoding.GetBytes(text).Select(n => Convert.ToString(n, 2).PadLeft(8, '0')));
        }

        public static void Main(string[] args)
        {
            var content = "";
            using (StreamReader sr = new StreamReader(@"/Users/prettyfl4cko/Desktop/4letterwords.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    content += line;
                }
            }

            var words = content.Replace(" ", "");

            var stringArr = new List<string>();
            for (var i = 0; i < words.Length; i += 4)
            {
                stringArr.Add(words.Substring(i, 4));
            }


            if (stringArr.Any(w => w.Length != 4))
            {
                Console.WriteLine("error");
            }

            var formattedWords = new List<string>();
            foreach (var word in stringArr)
            {
                formattedWords.Add(word.ToUpper());
            }


            var cypherBits = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 };

            var bitrepresentations = new List<int[]>();

            foreach (var word in formattedWords)
            {
                var stn = ToBinaryString(Encoding.ASCII, word);
                var intBase = new int[32];
                for (var i = 0; i < 32; i++)
                {
                    intBase[i] = int.Parse(stn[i].ToString());
                }
                bitrepresentations.Add(intBase);
            }

            var j = 0;
            var t = 0;

            foreach (var presentation in bitrepresentations)
            {
                t++;
                Console.WriteLine("t: " + t);
                foreach (var ct2 in bitrepresentations)
                {
                    j++;
                    if(j % 10 == 0)
                    {
                        Console.WriteLine(j);
                    }
                    var xored = XOROperation(presentation, ct2, new int[32], 32);

                    if (xored.ToText() == cypherBits.ToText())
                    {
                        Console.WriteLine("pres: " + presentation.ToText());
                        Console.WriteLine("ct2: " + ct2.ToText());
                    }
                }


            }
            Console.ReadLine();
        }
    }
}
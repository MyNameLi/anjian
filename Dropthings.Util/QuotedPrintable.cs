using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dropthings.Util
{
    public class QuotedPrintable
    {
        public static string Encode(string input, Encoding encoding)
        {
            const int MAXLINELENGTH = 76;
            int currentLineLength = 0;
            byte[] bytes = encoding.GetBytes(input.ToCharArray());
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 10 || bytes[i] == 13)
                {
                    if (bytes[i] == 13 && GetNextByte(i, bytes, 1) == 10)
                    {
                        CheckLineLength(MAXLINELENGTH, ref currentLineLength, 0, result);
                        result.Append(" ");
                        currentLineLength = 0;
                        i++;
                        continue;
                    }

                    if (bytes[i] == 10)
                    {
                        CheckLineLength(MAXLINELENGTH, ref currentLineLength, 0, result);
                        result.Append(" ");
                        currentLineLength = 0;
                    }

                    if (bytes[i] == 13)
                    {
                        CheckLineLength(MAXLINELENGTH, ref currentLineLength, 3, result);
                        result.Append("=" + ConvertToHex(bytes[i]));
                    }
                }
                else
                {
                    if ((bytes[i] >= 33 && bytes[i] <= 60) || (bytes[i] >= 62 && bytes[i] <= 126))
                    {
                        CheckLineLength(MAXLINELENGTH, ref currentLineLength, 1, result);
                        result.Append(System.Convert.ToChar(bytes[i]));
                    }
                    else
                    {
                        if (bytes[i] == 9 || bytes[i] == 32)
                        {
                            CheckLineLength(MAXLINELENGTH, ref currentLineLength, 0, result);
                            result.Append(System.Convert.ToChar(bytes[i]));
                            currentLineLength++;
                        }
                        else
                        {
                            CheckLineLength(MAXLINELENGTH, ref currentLineLength, 3, result);
                            result.Append("=" + ConvertToHex(bytes[i]));
                        }
                    }
                }
            }

            return result.ToString();
        }

        private static void CheckLineLength(int maxLineLength, ref int currentLineLength, int newStringLength, StringBuilder sb)
        {
            if (currentLineLength + 1 == maxLineLength || currentLineLength + newStringLength + 1 >= maxLineLength)
            {
                sb.Append("= ");
                currentLineLength = 0 + newStringLength;
            }
            else
            {
                currentLineLength += newStringLength;
            }
        }

        private static int GetNextByte(int index, byte[] bytes, int shiftValue)
        {
            int newIndex = index + shiftValue;

            if (newIndex < 0 || newIndex > bytes.Length - 1 || bytes.Length == 0)
                return -1;
            else
                return bytes[newIndex];
        }

        private static string ConvertToHex(byte number)
        {
            string result = System.Convert.ToString(number, 16).ToUpper();
            return (result.Length == 2) ? result : "0" + result;
        }
    }
}

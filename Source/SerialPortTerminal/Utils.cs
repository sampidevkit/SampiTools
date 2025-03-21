using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Utils
    {
        public bool IsBin(string s)
        {
            char[] arr = s.ToCharArray();

            foreach (char c in arr)
            {
                if ((c != '0') && (c != '1'))
                    return false;
            }

            return true;
        }

        public bool IsDec(string s)
        {
            char[] arr = s.ToCharArray();

            foreach (char c in arr)
            {
                if ((c > '9') || (c < '0'))
                    return false;
            }

            return true;
        }

        public bool IsHex(string s)
        {
            char[] arr = s.ToCharArray();

            foreach (char c in arr)
            {
                if ((c > '9') || (c < '0'))
                {
                    if ((c < 'A') || (c > 'F'))
                    {
                        if ((c < 'a') || (c > 'f'))
                            return false;
                    }
                }
            }

            return true;
        }

        public void ReplaceCharArray(ref char[] ChrArr, char oldChar, char newChar)
        {
            int i, j;
            int len = ChrArr.Length;

            for (i = 0; i < len; i++)
            {
                if (ChrArr[i] == oldChar)
                {
                    if (newChar != (char)0x00)
                        ChrArr[i] = newChar;
                    else
                    {
                        for (j = i; j < (len - 1); j++)
                            ChrArr[j] = ChrArr[j + 1];

                        ChrArr[j] = (char)0x00;
                        len--;
                    }
                }
            }

            char[] newChrArr = new char[len];

            for (i = 0; i < len; i++)
                newChrArr[i] = ChrArr[i];

            ChrArr = null;
            ChrArr = newChrArr;
        }

        public void RemoveStr(ref string Str, char oldChar, char newChar)
        {
            char[] Arr = Str.ToCharArray();

            ReplaceCharArray(ref Arr, oldChar, newChar);
            Str = new string(Arr);
        }

        public bool Is_ASCII_Printable_Character(byte b, ref long Line)
        {
            if ((b >= 0x20) && (b <= 0x7F))
                return true;
            else if (b == '\r')
                return true;
            else if (b == '\n')
            {
                Line++;
                return true;
            }
            else if (b == 0x09)// tab value
                return true;
            else
            {
                Line = 0;
                return false;
            }
        }

        public int Char2Integer(char[] pChr, int offset, int len)
        {
            int i;
            int value = 0;

            for (i = 0; i < len; i++)
            {
                value *= 10;

                if ((pChr[offset + i] >= '0') && (pChr[offset + i] <= '9'))
                    value += (int)(pChr[offset + i] - '0');
                else
                    return (-1);
            }

            return value;
        }
    }
}

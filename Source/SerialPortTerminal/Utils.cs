using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public bool Is_InvisibleCharacter(char c)
        {
            if((c < 0x20) || (c > 0x7F))
                return true;

            return false;
        }

        public string ShowInvisiableCharacters(string s)
        {
            StringBuilder sb = new StringBuilder(s.Length);

            foreach (char c in s)
            {
                if (Is_InvisibleCharacter(c))
                {
                    sb.Append('<');
                    sb.AppendFormat("{0:X2}", (int)c);
                    sb.Append('>');
                }
                else
                    sb.Append(c);
            }

            return sb.ToString();
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

        public bool BinParse(string s, ref UInt64 val)
        {
            RemoveStr(ref s, ' ', (char)0x00);
            RemoveStr(ref s, '.', (char)0x00);

            if (!IsBin(s))
            {
                MessageBox.Show("Error", "Incorrect binary format");
                return false;
            }

            if (s.Length > 64)
            {
                MessageBox.Show("Error", "Maximum length of binary number is 64");
                return false;
            }

            char[] arr = s.ToCharArray();
            val = 0;

            foreach (char c in arr)
            {
                val <<= 1;

                if (c == '1')
                    val |= 1;
            }

            return true;
        }

        public bool DecParse(string s, ref UInt64 val)
        {
            RemoveStr(ref s, ' ', (char)0x00);
            RemoveStr(ref s, '.', (char)0x00);

            if (!IsDec(s))
            {
                MessageBox.Show("Error", "Incorrect decimal format");
                return false;
            }

            if (s.Length > 20)
            {
                MessageBox.Show("Error", "Maximum length of decimal number is 20");
                return false;
            }

            try
            {
                val = UInt64.Parse(s);
            }
            catch
            {

            }

            return true;
        }

        public bool HexParse(string s, ref UInt64 val)
        {
            RemoveStr(ref s, ' ', (char)0x00);
            RemoveStr(ref s, '.', (char)0x00);

            if (!IsHex(s))
            {
                MessageBox.Show("Error", "Incorrect hexadecimal format");
                return false;
            }

            if (s.Length > 16)
            {
                MessageBox.Show("Error", "Maximum length of hexadecimal number is 16");
                return false;
            }

            char[] arr = s.ToCharArray();
            val = 0;

            foreach (char c in arr)
            {
                val <<= 4;

                if (c <= '9')
                    val |= ((UInt32)(c - '0'));
                else if (c >= 'A')
                    val |= ((UInt32)(c - 'A') + 10);
                else
                    val |= ((UInt32)(c - 'a') + 10);
            }

            return true;
        }

        public string DisplayBin(UInt64 val)
        {
            string s = null;
            int i, j, k;
            //DebugLog("\nval=" + val.ToString(), Color.Red);

            for (i = 0, j = -1; i < 64; i++)
            {
                if ((val & 0x8000000000000000) > 0)
                {
                    s += "1";

                    if (j == (-1))
                    {
                        j = 64 - i;

                        if ((j % 8) > 0)
                            j = ((j / 8) + 1) * 8;

                        //DebugLog("\nJ=" + j.ToString(), Color.Red);
                    }
                }
                else
                    s += "0";

                val <<= 1;
            }

            if (j == (-1))
            {
                s = "0";
                return s;
            }

            char[] sArr = s.ToCharArray();

            if (j > 8)
                k = (j - 8) / 8;
            else
                k = 0;

            char[] arr = new char[i + k];

            for (i = 0, k = 0; i < j; i++, k++)
            {
                if ((i > 0) && ((i % 8) == 0))
                    arr[k++] = '.';

                arr[k] = sArr[64 - j + i];

            }

            s = new string(arr);

            return s;
        }

        public string DisplayDec(UInt64 val)
        {
            string s = null;
            int i, j, k;

            for (i = 0, j = -1; i < 64; i++)
            {
                if ((val & 0x8000000000000000) > 0)
                {
                    s += "1";

                    if (j == (-1))
                    {
                        j = 64 - i;

                        if ((j % 8) > 0)
                            j = ((j / 8) + 1) * 8;

                        //DebugLog("\nJ=" + j.ToString(), Color.Red);
                    }
                }
                else
                    s += "0";

                val <<= 1;
            }

            if (j == (-1))
            {
                s = "0";
                return s;
            }

            char[] sArr = s.ToCharArray();

            if (j > 8)
                k = (j - 8) / 8;
            else
                k = 0;

            char[] arr = new char[i + k];

            for (i = 0, k = 0; i < j; i++, k++)
            {
                if ((i > 0) && ((i % 8) == 0))
                    arr[k++] = '.';

                arr[k] = sArr[64 - j + i];

            }

            s = new string(arr);

            return s;
        }

        public string DisplayHex(UInt64 val)
        {
            string s = null;
            int i, j, k;
            //DebugLog("\nval=" + val.ToString(), Color.Red);

            for (i = 0, j = -1; i < 64; i++)
            {
                if ((val & 0x8000000000000000) > 0)
                {
                    s += "1";

                    if (j == (-1))
                    {
                        j = 64 - i;

                        if ((j % 8) > 0)
                            j = ((j / 8) + 1) * 8;

                        //DebugLog("\nJ=" + j.ToString(), Color.Red);
                    }
                }
                else
                    s += "0";

                val <<= 1;
            }

            if (j == (-1))
            {
                s = "0";
                return s;
            }

            char[] sArr = s.ToCharArray();

            if (j > 8)
                k = (j - 8) / 8;
            else
                k = 0;

            char[] arr = new char[i + k];

            for (i = 0, k = 0; i < j; i++, k++)
            {
                if ((i > 0) && ((i % 8) == 0))
                    arr[k++] = '.';

                arr[k] = sArr[64 - j + i];

            }

            s = new string(arr);

            return s;
        }

    }
}

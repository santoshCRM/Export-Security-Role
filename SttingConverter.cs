using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsCrmTools.Soaplogger
{
    public static class SttingConverter
    {
        public static string ConvertString(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
            {
                return string.Empty;
            }
            string str = string.Empty;
            string[] separator = new string[] { "\r\n" };
            string[] strArray = Input.Replace("\"", "'").Split(separator, StringSplitOptions.None);
            for (int i = 0; i < (strArray.Length - 1); i++)
            {
                if (strArray[i].Length <= 0)
                {
                    continue;
                }
                string text = strArray[i];
                if (text[0] == '-')
                {
                    text = text.ReplaceFirst("-", "");
                }
                else
                {
                    for (int j = 1; j < text.Length; j++)
                    {
                        if (text[j - 1] != ' ')
                        {
                            break;
                        }
                        if (text[j] == '-')
                        {
                            text = text.ReplaceFirst("-", "");
                            break;
                        }
                    }
                }
                string[] textArray2 = new string[] { str, "\"", text, "\"+", Environment.NewLine };
                str = string.Concat(textArray2);
            }
            if ((strArray.Length != 0) && (strArray[strArray.Length - 1].Length > 0))
            {
                str = str + "\"" + strArray[strArray.Length - 1] + "\";";
            }
            return str;
        }
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int index = text.IndexOf(search);
            if (index < 0)
            {
                return text;
            }
            return (text.Substring(0, index) + replace + text.Substring(index + search.Length));
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace App.Common.Static
{
    public static class SlugGeneretor
    {
        public static string GenerateSlug(this string phrase)
        {
            if (!phrase.IsNormalized(NormalizationForm.FormKD))
            {
                phrase = phrase.Normalize(NormalizationForm.FormKD);
            }
            string str = phrase.ToLower();//phrase.RemoveAccent().ToLower();
            // invalid chars           
            //str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", "-").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public static string RemoveAccent(this string txt)
        {
            //byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            byte[] bytes = System.Text.Encoding.GetEncoding("utf-16 ").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}

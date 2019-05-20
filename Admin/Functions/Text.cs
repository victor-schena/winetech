using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Admin.Functions
{
  public class Text
  {
    public static string RemoveDiacritics(string s)
    {
      if (string.IsNullOrWhiteSpace(s))
        return s;

      s = s.Normalize(NormalizationForm.FormD);
      var chars = s.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
      return new string(chars).Normalize(NormalizationForm.FormC);
    }

    public static string SetFriendlyName(string s)
    {
      var str = RemoveDiacritics(s).ToLower(); // remove acentuação
      var str2 = Regex.Replace(str, @"\s+", "-"); // substitui espaços em branco por um traço
      var str3 = Regex.Replace(str2, @"[^a-zA-Z0-9_.-]+", "", RegexOptions.Compiled); // remove caracteres especiais, exceto underlines, pontos ou traços
      var str4 = Regex.Replace(str3, @"[\s._]+", "-"); // substitui espaços em branco, pontos e underlines por um traço
      var str5 = Regex.Replace(str4, @"[-]+", "-"); // substitui dois ou mais traços por apenas um traço
      var last = str5[str5.Length - 1];
      if (last.Equals('-')) // se o último caractere for um traço, remove
        return str5.Remove(str5.Length - 1, 1);
      else
        return str5;
    }
  }
}
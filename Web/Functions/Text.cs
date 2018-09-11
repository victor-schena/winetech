using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Web.Functions
{
  public class Text
  {
    public static string RemoveHtmlTags(string s)
    {
      string withoutHtml = Regex.Replace(s, @"<[^>]+>|&nbsp;", "").Trim();
      return Regex.Replace(withoutHtml, @"\s{2,}", " ");
    }

    public static string RemoveDiacritics(string s)
    {
      if (string.IsNullOrWhiteSpace(s))
        return s;

      s = s.Normalize(NormalizationForm.FormD);
      var chars = s.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
      return new string(chars).Normalize(NormalizationForm.FormC);
    }
  }
}
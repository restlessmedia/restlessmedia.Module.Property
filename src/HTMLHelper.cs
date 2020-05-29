using System.Text.RegularExpressions;

namespace restlessmedia.Module.Property
{
  internal static class HTMLHelper
  {
    /// <summary>
    /// Converts an html to text
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
    public static string ToText(string html)
    {
      if (string.IsNullOrWhiteSpace(html))
      {
        return html;
      }

      const string _matchTagPattern = @"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[\^'"">\s]+))?)+\s*|\s*)/?>";
      return Regex.Replace(html, _matchTagPattern, string.Empty).Trim();
    }
  }
}
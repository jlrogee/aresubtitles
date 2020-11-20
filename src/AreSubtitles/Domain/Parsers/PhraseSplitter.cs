using System;
using System.Text.RegularExpressions;

namespace Domain.Parsers
{
    public class PhraseSplitter : IPhraseSplitter
    {
        public string[] Split(string text)
        {
            var cleanStr = Regex.Replace(text, "[^A-Za-z0-9 -']", "")
                
                // TODO Remove tags
                
                .Replace("!", ""); // ?

            return cleanStr.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }
    }

    public interface IPhraseSplitter
    {
        string[] Split(string text);
    }
}
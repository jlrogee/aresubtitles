using System;
using System.IO;
using System.Threading.Tasks;
using Debug.Commands;

namespace Debug
{
    public class LoadFilesImdbDebugCommand : DebugCommand
    {
        public override string Name => "parse";
        public override async Task Execute()
        {
            var titlePath = @"C:\Users\AyratS\Desktop\title.basics.tsv\data.tsv";
            var titleTxt =  await ParseImdbFile(titlePath);  
            //  7481751 -> tt9916880	tvEpisode	Horrid Henry Knows It All	Horrid Henry Knows It All	0	2014	\N	10	Animation,Comedy,Family
            
            var namePath = @"C:\Users\AyratS\Desktop\name.basics.tsv\data.tsv";
            var nameTxt =  await ParseImdbFile(namePath);
            //  10616756 -> nm9993719	Andre Hill	\N	\N		\N
            
            Console.WriteLine("parse");
        }

        private static async Task<ParseFileStatus> ParseImdbFile(string path)
        {
            var lastRow = string.Empty;
            await using var fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            await using var bs = new BufferedStream(fs);
            using var sr = new StreamReader(bs);
            string line;
            var row = 0;
            while ((line = await sr.ReadLineAsync()) != null)
            {
                row++;
                if (!string.IsNullOrEmpty(line))
                    lastRow = line;
            }

            return new ParseFileStatus(lastRow, row);
        }
    }

    internal record ParseFileStatus
    {
        public string Txt { get; set; }
        public int RowsCount { get; set; }
        public ParseFileStatus(string txt, int rowsCount) 
            => (Txt, RowsCount) = (txt, rowsCount);
    }
}
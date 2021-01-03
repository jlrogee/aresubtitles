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
            var path = @"C:\Users\AyratS\Desktop\title.basics.tsv\data.tsv";
            var line = "";

            await using (var fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))  
            {
                await using (var bs = new BufferedStream(fs))  
                {  
                    using (var sr = new StreamReader(bs))  
                    {  
                        while ((line = await sr.ReadLineAsync()) != null)  
                        {  
  
                        }  
                    }  
                }  
            }  
            
            Console.WriteLine("parse");
        }
    }
}
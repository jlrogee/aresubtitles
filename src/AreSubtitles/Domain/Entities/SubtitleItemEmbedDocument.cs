namespace Domain.Entities
{
    public class SubtitleItemEmbedDocument
    {
        public int Num { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public string Text { get; set; }
        public string[] Words { get; set; }
    }
}
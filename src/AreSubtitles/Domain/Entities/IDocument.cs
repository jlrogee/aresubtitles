namespace Domain.Entities
{
    public interface IDocument : IDocumentWithKey<string>
    {
    }
    
    public interface IDocumentWithKey<TKey>
    {
        TKey Id { get; set; }
    }
}
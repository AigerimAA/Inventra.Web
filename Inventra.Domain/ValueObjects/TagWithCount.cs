namespace Inventra.Domain.ValueObjects
{
    public class TagWithCount
    {
        public string Name { get;} 
        public int Count { get;}

        public TagWithCount(string name, int count)
        {
            Name = name;
            Count = count;
        }
    }
}

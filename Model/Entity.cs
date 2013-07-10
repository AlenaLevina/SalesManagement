namespace Model
{
    public class Entity { }

    public class Entity<TKey> : Entity
    {
        public TKey Id { get; set; }
    }
}
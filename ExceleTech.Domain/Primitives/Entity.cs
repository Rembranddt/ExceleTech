namespace ExceleTech.Domain.Primitives
{
    public abstract class Entity<T>
    {  
        public T Id { get; protected set; }
    }


}

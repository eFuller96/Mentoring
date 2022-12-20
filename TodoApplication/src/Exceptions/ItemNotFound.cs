namespace ToDoApplication.Exceptions
{
    public class ItemNotFound : Exception
    {
        public ItemNotFound() { }
        public ItemNotFound(string message) : base(message) { }
        public ItemNotFound(string message, Exception inner) : base(message, inner) { }
    }
}

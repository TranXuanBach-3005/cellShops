namespace cellShopSolution.Utilities.Exceptions
{
    public class cellException : Exception
    {
        public cellException()
        {
        }
        public cellException(string message) : base(message) 
        {
        }
        public cellException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

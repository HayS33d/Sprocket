namespace Sprocket
{
    public class SprocSharpException : Exception
    {
        public Exception? OriginalException { get; }
        public SprocSharpException()
        {
        }

        public SprocSharpException(string message, Exception e) : base(message)
        {
            OriginalException = e;
        }
    }
}
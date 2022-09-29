namespace Sprocket
{
    public class NullDataException : Exception
    {
        public NullDataException() : base("The command completed successfully but Not data is available")
        {
        }
    }
}
namespace Sprocket
{
    public interface ISqlResponse
    {
        DetailedResponseMessage<T> As<T>();

        bool IsSuccess { get; }
        Exception? Exception { get; }
    }
}
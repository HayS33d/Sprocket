namespace Sprocket
{
    public interface ISqlRequest : ISqlResponse
    {
        ISqlRequest ResponseIsXml(bool isXml = true);
        ISqlRequest AddParameter(string name, object value);
        ISqlRequest SetTimeout(int seconds);

    }
}
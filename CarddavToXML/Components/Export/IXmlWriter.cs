namespace PhonebookConverter.Components.Export
{
    public interface IXmlWriter 
    {
        void YealinkLocal(string filepath, string dataType);
        void YealinkRemote(string filepath, string dataType);
        void FanvilRemoteAndLocal(string filepath, string dataType);
    }
}

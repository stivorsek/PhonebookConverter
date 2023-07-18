namespace PhonebookConverter.Components.Export
{
    public interface IXmlWriter 
    {
        void YealinkLocal(string filepath);
        void YealinkRemote(string filepath);
        void FanvilRemoteAndLocal(string filepath);
    }
}

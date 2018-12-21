namespace SocomapLib
{
    /// <summary>  
    ///  This interface defines the methods the processing backend offers.
    /// </summary>  
    public interface IInboxFunctionsSampleApi
    {
        /// <exception cref="System.ArgumentException">Thrown when the inbox already exists.</exception>
        void CreateInbox(Inbox inbox);

        /// <returns>The inbox object for this party name, if such a party exists. Null otherwise.</exception>
        Inbox GetInbox(string partyName);
    }
}

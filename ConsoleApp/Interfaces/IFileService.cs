namespace ConsoleApp.Interfaces
{
    public interface IFileService
    {
        string GetContentFromFile();
        List<IContact> LoadContactsFromFile();
        bool SaveContentToFile(string content);
    }
}
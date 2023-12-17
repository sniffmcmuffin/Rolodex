namespace ConsoleApp.Interfaces
{
    public interface IFileService
    {
        string GetContentFromFile();
        /// <summary>
        /// Loads content from file.
        /// </summary>
        /// <returns>Loads file content into a specified list.</returns>
        List<IContact> LoadContactsFromFile();
        
        /// <summary>
        /// Saves content to file.
        /// </summary>
        /// <param name="content">Content is string and int.</param>
        /// <returns>Returns true if saved else false if failed.</returns>
        bool SaveContentToFile(string content);
    }
}
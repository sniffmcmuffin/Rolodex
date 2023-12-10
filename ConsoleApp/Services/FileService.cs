using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ConsoleApp.Services;

public class FileService : IFileService
{
    private readonly string _filePath;

    public FileService(string filePath)
    {
        _filePath = filePath;
    }

    public string GetContentFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                using (var sr = new StreamReader(_filePath))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public bool SaveContentToFile(string content)
    {
        try
        {
            using (var sw = new StreamWriter(_filePath))
            {
                sw.WriteLine(content);
            }

            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public List<IContact> LoadContactsFromFile()
    {
        List<IContact> contactList = new List<IContact>();

        try
        {
            string content = GetContentFromFile();

            if (!string.IsNullOrEmpty(content))
            {
                // Deserialize the JSON content into a list of Contacts
                List<Contact> deserializedList = JsonConvert.DeserializeObject<List<Contact>>(content);

                // Convert the list of Contacts to a list of IContact
                contactList = deserializedList.Cast<IContact>().ToList();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return contactList;
    }
}

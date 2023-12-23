using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Models;
using System.Diagnostics;

namespace Shared.Services;

public class FileService : IFileService
{
    private readonly string _filePath;

    public FileService(string filePath)
    {
        _filePath = filePath;
    }

    public string ReadContentFromFile()
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
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
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
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }

    public List<IContact> LoadContactsFromFile()
    {
        List<IContact> contactList = new List<IContact>();

        try
        {
            string content = ReadContentFromFile();

            if (!string.IsNullOrEmpty(content))
            {
                List<Contact> deserializedList = JsonConvert.DeserializeObject<List<Contact>>(content) ?? new List<Contact>();
               
                contactList = deserializedList.Cast<IContact>().ToList();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return contactList;
    }



    public string GetContentFromFile()
    {
        throw new NotImplementedException();
    }
}

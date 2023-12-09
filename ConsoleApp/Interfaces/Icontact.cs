namespace ConsoleApp.Interfaces;


public interface IContact
{
    int Id { get; set; }
    string firstName { get; set; }
    string lastName { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; } // String instead of int to allow - in number for easiser viewing.
}

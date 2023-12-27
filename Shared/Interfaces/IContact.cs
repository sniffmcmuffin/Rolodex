namespace Shared.Interfaces
{
    public interface IContact
    {
        string city { get; set; }
        string email { get; set; }
        string firstName { get; set; }
        int Id { get; set; }
        string lastName { get; set; }
        string phoneNumber { get; set; }
        string street { get; set; }
        string zipCode { get; set; } // Cant be int in case someone might have a space between digits.
        string CompanyName { get; set; }
        string ContactPerson { get; set; }
    }
}
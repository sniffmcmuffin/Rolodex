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
        int zipCode { get; set; }
        string CompanyName { get; set; }
        string ContactPerson { get; set; }
    }
}
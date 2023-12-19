namespace SharedProj.Interfaces
{
    public interface IContact
    {
        string city { get; set; }
        string email { get; set; }
        string firstName { get; set; }
        Guid Id { get; set; }
        string lastName { get; set; }
        string phoneNumber { get; set; }
        string street { get; set; }
        int zipCode { get; set; }
    }
}
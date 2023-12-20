using Shared.Interfaces;

namespace Shared.Models;

public class Contact : IContact
{
    public Guid Id { get; set; } = Guid.NewGuid(); // Hmm it worked here, but not in console app. 
    public string firstName { get; set; } = null!;
    public string lastName { get; set; } = null!;
    public string street { get; set; } = null!;
    public int zipCode { get; set; } = 0!;
    public string city { get; set; } = null!;
    public string email { get; set; } = null!;
    public string phoneNumber { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string ContactPerson { get; set; } = null!;

}

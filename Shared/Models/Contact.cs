using Shared.Interfaces;

namespace Shared.Models;

public class Contact : IContact
{
    public int Id { get; set; } = 0!; // Hmm it worked here, but not in console app. 
    public string firstName { get; set; } = null!;
    public string lastName { get; set; } = null!;
    public string street { get; set; } = null!;
    public string zipCode { get; set; } = null!;
    public string city { get; set; } = null!;
    public string email { get; set; } = null!;
    public string phoneNumber { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string ContactPerson { get; set; } = null!;

    public override string ToString()
    {
        return $"First Name: {firstName}, Last Name: {lastName}, Email: {email}, Phone Number: {phoneNumber}";
    }
}

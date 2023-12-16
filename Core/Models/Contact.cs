using Core.Interfaces;

namespace Core.Models;

internal class Contact : IContact
{
    public int Id { get; set; }
    public string firstName { get; set; } = null!;
    public string lastName { get; set; } = null!;
    public string email { get; set; } = null!;
    public string phoneNumber { get; set; } = null!;
    public override string ToString()
    {
        return $"First Name: {firstName}, Last Name: {lastName}, Email: {email}, Phone Number: {phoneNumber}";
    }
}

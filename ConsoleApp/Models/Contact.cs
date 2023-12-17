using ConsoleApp.Interfaces;

namespace ConsoleApp.Models;


public class Contact : IContact
{
    public int Id { get; set; }
   
    // This broke things. Will work it in later when I have time. Reverting to using Id in Interface for now.
    //public Guid id { get; set; } = Guid.NewGuid(); // Not in Interface, but here according to Hans. 
    
    public string firstName { get; set; } = null!;
    public string lastName { get; set; } = null!;
    public string email { get; set; } = null!;
    public string phoneNumber { get; set; } = null!;
    public override string ToString()
    {
        return $"First Name: {firstName}, Last Name: {lastName}, Email: {email}, Phone Number: {phoneNumber}";
    }
}


using ConsoleApp.Interfaces;

namespace ConsoleApp.Models;


public class Contact : IContact
{
    public int Id { get; set; }
    public string firstName { get; set; } = null!;
    public string lastName { get; set; } = null!;
}

namespace ConsoleApp.Interfaces;

/// <summary>
/// Interface for the contact model.
/// </summary>
public interface IContact
{
    /// <summary>
    /// Gets or sets a unique identity number of a contact.
    /// </summary>
    int Id { get; set; }

    /// <summary>
    /// Gets or sets the first name of a contact.
    /// </summary>
    string firstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of a contact.
    /// </summary>
    string lastName { get; set; }

    /// <summary>
    /// Gets or sets the email of a contact.
    /// </summary>
    public string email { get; set; }

    /// <summary>
    /// Gets or sets the phonenumber of a contact.
    /// </summary>
    public string phoneNumber { get; set; } // String instead of int to allow - in number for easiser viewing.
}

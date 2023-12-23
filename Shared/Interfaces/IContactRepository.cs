using Shared.Models;

namespace Shared.Interfaces;

/// <summary>
/// Interface for contact repository, providing CRUD operations (Create, Read, Update, Delete).

/// </summary>
public interface IContactRepository
{
    /// <summary>
    /// Adds new contact to the repository.
    /// </summary>
    /// <param name="contact">Contact to be added.</param>
    /// <returns>An IServiceResult indicating the outcome of the operation.</returns>
    IServiceResult AddContact(IContact contact);

    /// <summary>
    /// Gets all contacts from the repository.
    /// </summary>
    /// <returns>An IServiceResult with the list of contacts, else error message if failed.</returns>
    IServiceResult GetAllContacts();

    /// <summary>
    /// Gets a contact from repository based on provided predicate.
    /// </summary>
    /// <param name="predicate">The predicate used to filter contacts.</param>
    /// <returns>An IServiceResult containing the matching contact or error message if failed.</returns>
    IServiceResult GetContactByEmail(Func<Contact, bool> predicate);

    /// <summary>
    /// Deletes a contacts from repository based on provided predicate.
    /// </summary>
    /// <param name="predicate">The predicate used to identify contact for deletion.</param>
    /// <returns>An IServiceResult indicating the outcome of the operation.</returns>
    IServiceResult DeleteContact(Func<IContact, bool> predicate);
}

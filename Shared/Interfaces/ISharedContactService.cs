using Shared.Models;

namespace Shared.Interfaces
{
    /// <summary>
    /// Interface for a shared contact service, defining operations on contact entities.
    /// </summary>
    public interface ISharedContactService
    {
        /// <summary>
        /// Adds a new contact to the contact service.
        /// </summary>
        /// <param name="contact">The contact to be added.</param>
        /// <returns>An IServiceResult indicating the result of the operation.</returns>
        IServiceResult AddContact(IContact contact);

        /// <summary>
        /// Deletes contacts based on the specified predicate.
        /// </summary>
        /// <param name="predicate">A predicate for selecting contacts to be deleted.</param>
        /// <returns>An IServiceResult indicating the result of the operation.</returns>
        IServiceResult DeleteContact(Func<IContact, bool> predicate);
        
        /// <summary>
        /// Retrieves all contacts.
        /// </summary>
        /// <returns>An IServiceResult containing a collection of contacts.</returns>
        IServiceResult GetAllContacts();
               
        /// <summary>
        /// Retrieves contacts based on the specified predicate.
        /// </summary>
        /// <param name="predicate">A predicate for selecting contacts to be retrieved.</param>
        /// <returns>An IServiceResult containing a collection of contacts.</returns>
        IServiceResult GetContactFromList(Func<Contact, bool> predicate);
    }
}
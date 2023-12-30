namespace KonsolApp.Interfaces
{
    /// <summary>
    /// Handles menu in the console application.
    /// </summary>
    public interface IMenuService
    {
        /// <summary>
        /// Delete a contact
        /// </summary>
        void DeleteContact();

        /// <summary>
        /// Lists a contact based on email.
        /// </summary>
        /// <param name="email"></param>
        void ListContactByEmail(string email);

        /// <summary>
        /// Show a contact.
        /// </summary>
        void ShowContactByEmail();

        /// <summary>
        /// Displays the main menu.
        /// </summary>
        void ShowMenu();
    }
}
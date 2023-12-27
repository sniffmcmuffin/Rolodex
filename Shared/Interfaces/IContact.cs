﻿namespace Shared.Interfaces
{
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
        /// </summary
        string street { get; set; }
        string zipCode { get; set; } // Cant be int in case someone might have a space between digits.
        string city { get; set; }

        public string email { get; set; }

        /// <summary>
        /// Gets or sets the phonenumber of a contact.
        /// </summary>
        public string phoneNumber { get; set; } // String instead of int to allow - in number for easiser viewing.
        string CompanyName { get; set; }
        string ContactPerson { get; set; }

    }
}
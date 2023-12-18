using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Mvvm.Interfaces;

namespace WpfApp.Mvvm.Models
{
    internal class Contact : IContact
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Hmm it worked here, but not in console app. 
        public string firstName { get; set; } = null!;
        public string lastName { get; set; } = null!;
        public string email { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
    }
}

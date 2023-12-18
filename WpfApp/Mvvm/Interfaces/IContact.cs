namespace WpfApp.Mvvm.Interfaces
{
    internal interface IContact
    {
        string email { get; set; }
        string firstName { get; set; }
        Guid Id { get; set; }
        string lastName { get; set; }
        string phoneNumber { get; set; }
    }
}
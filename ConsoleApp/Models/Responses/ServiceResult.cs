// using ConsoleApp.Enums;
using Shared.Enums;

using ConsoleApp.Interfaces;

namespace ConsoleApp.Models.Responses;

public class ServiceResult : IServiceResult
{
    public ServiceStatus Status { get; set; }
    public object Result { get; set; } = null!;
}

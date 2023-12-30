using Shared.Enums;

namespace Shared.Interfaces;

/// <summary>
/// Gets or sets result type like success, failed, already exists.
/// </summary>
public interface IServiceResult
{
    object Result { get; set; }

    ServiceStatus Status { get; set; }
}
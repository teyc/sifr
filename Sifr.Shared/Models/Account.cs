using System;

namespace Sifr.Shared.Models
{
    public record Account(Guid Id, string Code, string Name, string Type, Guid? ParentId);
}

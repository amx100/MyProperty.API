using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MyProperty.API.Core.Domain.Entities.JointTable;

public class AccountIdentityUserRole : IdentityUserRole<string>
{
    public virtual Account User { get; set; }
    public virtual AccountRole Role { get; set; }

    public override string ToString() => Role.Name;
}

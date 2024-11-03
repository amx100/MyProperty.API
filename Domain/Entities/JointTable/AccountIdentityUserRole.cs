using Microsoft.AspNetCore.Identity;
using MyProperty.API.Core.Domain.Entities;
using static MyProperty.API.Core.Domain.Entities.Account;

namespace MyProperty.API.Core.Domain.Entities.JointTable;

public class AccountIdentityUserRole : IdentityUserRole<string>
{
    public virtual Account User { get; set; }
    public virtual AccountRole Role { get; set; }

    public override string ToString() => Role.Name;
}

using System;
using System.Security.Policy;

namespace GymMe;

public class User
{
    public int ID { get; set; }
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public Role Role { get; set; } = Role.Member;
}
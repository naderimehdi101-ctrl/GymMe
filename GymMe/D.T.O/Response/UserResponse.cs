
using GymMe;

namespace GymMe.Response.DTO;

public class UserResponse
{
    public int ID { get; set; }
    public string Email { get; set; } = "";
    public Role Role { get; set; }
}
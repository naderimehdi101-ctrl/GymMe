
namespace GymMe.Response.DTO;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public int ID { get; set; }
    public string Email { get; set; } = string.Empty;
    public Role Role { get; set; }
}
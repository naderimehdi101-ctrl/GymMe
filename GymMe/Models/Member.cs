
using System.ComponentModel.DataAnnotations;

namespace GymMe.Model;

public class Member
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }

    public int GymPlanId { get; set; } 

    public GymPlan GymPlan { get; set; } = null!;
    
    [ConcurrencyCheck]
    public Guid Version { get; set; } = Guid.NewGuid();
}

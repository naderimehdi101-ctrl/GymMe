
using System.ComponentModel.DataAnnotations;

namespace GymMe.Model;

public class GymPlan
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }
    
    [ConcurrencyCheck]
    public Guid Version { get; set; } = Guid.NewGuid();
}
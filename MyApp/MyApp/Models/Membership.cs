public class Membership
{
    public int Id { get; set; }

    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;

    public int GymPlanId { get; set; }
    public GymPlan GymPlan { get; set; } = null!;

    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
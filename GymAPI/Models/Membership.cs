public class Membership
{
    public int ID { get; set; }

    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;

    public int GymPlanId { get; set; }
    public GymPlan GymPlan { get; set; } = null!;

    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public int DurationMonths { get; set; }
    public bool IsActive => EndDate >= DateOnly.FromDateTime(DateTime.UtcNow);
}
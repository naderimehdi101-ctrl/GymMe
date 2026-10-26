public class Member
{
    public int ID { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }

    public int GymPlanId { get; set; } 

    public GymPlan? GymPlan { get; set; }
}


public class CreateMembershipDto
{
    public int MemberID { get; set; }
	public int GymPlanID { get; set; }
	
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
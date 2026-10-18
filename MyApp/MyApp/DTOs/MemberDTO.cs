
public class CreateMemberDTO
{
    public string? Name { get; set; }
	
    public int Age { get; set; }

    public int GymPlanId { get; set; }
}

public class MemberIDDTO
{
    public int ID { get; set; }

    public int NextID { get; set; }
}
using System;

namespace MyApp;

public class MembershipService
{
   private readonly AppDbContext _db;

    public MembershipService(AppDbContext db)
    {
        _db = db;
    }
	
	

}
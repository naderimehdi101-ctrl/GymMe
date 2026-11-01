using System;

namespace GymMe;

public static class PaginationHelper
{
    public static int ValidatePageAndPagesize(int page, int pageSize)
    {
        if (page < 1 || pageSize < 1)
            throw new ArgumentException("Page & Pagesize must both be atleast 1!");
        if (pageSize > 20)
            pageSize = 20;
            
        return pageSize;    
    }
}
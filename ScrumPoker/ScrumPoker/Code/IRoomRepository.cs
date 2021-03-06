﻿using ScrumPoker.Models;

namespace ScrumPoker.Code
{
    public interface IRoomRepository : ICrud<Room,string>
    {
        Room ReadByAdminId(string adminId);
    }
}
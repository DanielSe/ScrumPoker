using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScrumPoker.Models;

namespace ScrumPoker
{
    public interface IRoomRepository : ICrud<Room,string>
    {
    }
}
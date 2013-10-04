using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using ScrumPoker.Models;

namespace ScrumPoker
{
    public class ScrumPokerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IIdGenerator<string>>().To<IdGenerator>();
            Bind<IRoomRepository>().To<Rooms>().InSingletonScope();
        }
    }
}
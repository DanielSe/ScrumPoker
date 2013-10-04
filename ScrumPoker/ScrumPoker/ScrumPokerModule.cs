using System;
using Ninject.Modules;
using ScrumPoker.Code;
using ScrumPoker.Models;

namespace ScrumPoker
{
    public class ScrumPokerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<Random>().ToSelf().InSingletonScope();
            Bind<IIdGenerator<string>>().To<IdGenerator>()
                .WithConstructorArgument("length", 6);
            Bind<IRoomRepository>().To<RoomRepository>().InSingletonScope();
            Bind<IIssueRepository>().To<IssueRepository>().InSingletonScope();
        }
    }
}
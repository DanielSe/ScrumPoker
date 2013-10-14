using System;
using Ninject.Modules;
using ScrumPoker.Code;
using ScrumPoker.Models;
using ScrumPoker.Hubs;
using ScrumPoker.ApiControllers;

namespace ScrumPoker
{
    public class ScrumPokerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<Random>().ToSelf().InSingletonScope();

            Bind<IIdGenerator<string>>().To<IdGenerator>()
                .WithConstructorArgument("length", 6);

            Bind<ScrumPokerContext>().ToSelf().InSingletonScope();

            Bind<IRoomRepository>().To<RoomRepository>().InSingletonScope();
            Bind<IIssueRepository>().To<IssueRepository>().InSingletonScope();
            Bind<IParticipantRepository>().To<ParticipantRepository>().InSingletonScope();

            // HUB Bindings
            Bind<RoomHub>().ToSelf();

            // WebAPI Bindings
            Bind<ParticipantController>().ToSelf();
        }
    }
}
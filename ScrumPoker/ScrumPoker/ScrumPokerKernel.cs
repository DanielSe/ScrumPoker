using Ninject;

namespace ScrumPoker
{
    public static class ScrumPokerKernel
    {
        static ScrumPokerKernel()
        {
            Instance = new StandardKernel(new ScrumPokerModule());
        }

        public static IKernel Instance { get; private set; }
    }
}
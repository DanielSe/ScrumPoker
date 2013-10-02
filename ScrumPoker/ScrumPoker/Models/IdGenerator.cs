using System;
using System.Linq;

namespace ScrumPoker.Models
{
    public class IdGenerator : IIdGenerator<string>
    {
        private const string Source = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const int Length = 6;

        private static readonly Random Rnd = new Random();

        public string CreateId()
        {
            var chars = Enumerable.Range(1, Length).Select(x => Source[Rnd.Next(Source.Length)]).ToArray();
            return new string(chars);
        }
    }
}
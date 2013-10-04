using System;
using System.Linq;

namespace ScrumPoker.Models
{
    public class IdGenerator : IIdGenerator<string>
    {
        private const string Source = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly int _length = 6;
        private readonly Random _rnd;

        public IdGenerator(int length, Random rnd)
        {
            _length = length;
            _rnd = rnd;
        }

        public string CreateId()
        {
            var chars = Enumerable.Range(1, _length).Select(x => Source[_rnd.Next(Source.Length)]).ToArray();
            return new string(chars);
        }
    }
}
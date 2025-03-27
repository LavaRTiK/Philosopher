using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philosopher
{
    public class GlobalStatePhilosophe
    {
        private readonly ConcurrentBag<Philosoph> philosophs = new ConcurrentBag<Philosoph>();
        private void AddPhilosoph(Philosoph philosoph)
        {
            philosophs.Add(philosoph);
        }
    }
}

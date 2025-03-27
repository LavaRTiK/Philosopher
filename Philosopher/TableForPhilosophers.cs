using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philosopher
{
    public class TableForPhilosophers
    {
        private Logger _logger;
        private List<bool> _blocks = new List<bool>() {false,false,false,false,false };
        private BlockingCollection<string> _philosoph; 
        private object _lock = new object();
        public TableForPhilosophers(Logger logger)
        {
            _philosoph = new BlockingCollection<string>();
            this._logger = logger;
        }
        public void RegisterGust(Philosoph philosoph)
        {
            if (_philosoph.TryAdd(philosoph.Name))
            {
                lock (_lock)
                {
                    int index;
                    index = _philosoph.ToList().IndexOf($"{philosoph.Name}");
                    _logger.LogMessage("Info", $"Философ за столом {index}");
                }
            }
        }
        public void RequstToEat(Philosoph philosoph)
        {
            lock(_lock)
            {
                int index = _philosoph.ToList().IndexOf($"{philosoph.Name}");
                //if philosophe hands left
                if (_blocks[index] == false && index + 1 == 6 ? _blocks[0] == false : _blocks[index + 1])
                {

                }
            }
        }
    }
}

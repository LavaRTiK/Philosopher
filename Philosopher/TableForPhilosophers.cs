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
        private List<Philosoph> _guest;
        private List<bool> forks = new List<bool> { false,false,false,false,false };
        object _lock = new object();
        Semaphore _semaphoreListGuest;
        Semaphore _toEat;
        Semaphore _toDisposableForks;

        public TableForPhilosophers(Logger logger)
        {
            _semaphoreListGuest = new Semaphore(1, 1);
            _guest = new List<Philosoph>();
            _toEat = new Semaphore(1, 1);
            _toDisposableForks = new Semaphore(1, 1);
            this._logger = logger;
        }
        public void RegisetGuest(Philosoph philosoph)
        {
            _semaphoreListGuest.WaitOne();
            _guest.Add(philosoph);
            Console.WriteLine($"{philosoph.Name} селл за стол ");
            _logger.LogMessage("Info", $"{philosoph.Name} сів за стіл");
            _semaphoreListGuest.Release();
        }
        public bool RequstToEat(Philosoph philosoph)
        {
            lock (_lock)
            {
                if (_guest.Count < 5)
                {
                    return false;
                }
            }
            _logger.LogMessage("Info", $"{philosoph.Name} Чекає свої черги");
            _toEat.WaitOne();
            try
            {
                int index = _guest.IndexOf(philosoph);
                if (forks[index] == false && (index + 1 == _guest.Count ? forks[0] == false : forks[index + 1] == false))
                {
                    forks[index] = true;
                    if (index + 1 == _guest.Count)
                    {
                        forks[0] = true;
                    }
                    else
                    {
                        forks[index + 1] = true;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                string text = "";
                foreach (var fork in forks)
                {
                    text += $" {fork.ToString()}";
                }
                _logger.LogMessage("Info", text);
                _toEat.Release();
            }
        }
        //DisposableVedelki
        public void DisposableForks(Philosoph philosoph)
        {
            _toEat.WaitOne();
            try
            {
                int index = _guest.IndexOf(philosoph);
                if (forks[index] == true && (index + 1 == _guest.Count ? forks[0] == true : forks[index + 1] == true))
                {
                    forks[index] = false;
                    if (index + 1 == _guest.Count)
                    {
                        forks[0] = false;
                    }
                    else
                    {
                        forks[index + 1] = false;
                    }
                    _logger.LogMessage("Info", $"{philosoph.Name} Віддав виделки");
                }
            }
            finally
            {
                _toEat.Release();
            }
        }
    }
}

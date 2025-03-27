using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philosopher
{
    public class Philosoph
    {
        public string Name { get; set; }
        public TableForPhilosophers table;
        private int _hungry;
        private bool _cancelled;
        public Logger _logger;
        private PhilosopherState _state;
        public Philosoph(string Name,Logger logger,TableForPhilosophers tableForPhilosophers) {
            this.table = tableForPhilosophers;
            this.Name = Name;
            _logger = logger;
            _hungry = 0;
            _state = PhilosopherState.Thinks;
        }
        public int GetHungry()
        {
            return _hungry;
        }
        public void AppendHungry()
        {
            _hungry = _hungry + 1;
        }
        public void ResetHungry()
        {
            _hungry = 0;
        }
        public PhilosopherState GetState()
        {
            return _state;
        }
        public void SetState(PhilosopherState state)
        {
            _state = state;
        }
        public void PhilosopherLive()
        {
            var thread = new Thread(PhilosopherLogic);
            thread.Start();
        }
        public void PhilsopherDead()
        {
            _cancelled = true;
        }
        private void PhilosopherLogic()
        {
            table.RegisterGust(this);
            while (!_cancelled)
            {
                ResetHungry();
                Thinks();

            };
        }
        private void Thinks()
        {
            _logger.LogMessage("Info",$"{Name} Думає");
            SetState(PhilosopherState.Thinks);
            Thread.Sleep(1500);
        }
    }
}

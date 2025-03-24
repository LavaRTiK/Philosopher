using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philosopher
{
    public class Philosopher
    {
        public string Name { get; set; }
        private int _hungry;
        private PhilosopherState _state;
        public Philosopher(string Name) {
            this.Name = Name;
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
    }
}

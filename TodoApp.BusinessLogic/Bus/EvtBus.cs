using System;
using System.Collections.Generic;

namespace TodoApp.BusinessLogic.Bus
{
    public class EvtBus : IObservable
    {
        private List<IObserver> observers;
        private Message evt;

        public EvtBus()
        {
            observers = new List<IObserver>();
            evt = new Message();
        }

        public void RegisterObserver(IObserver o)
        {
            observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            observers.Remove(o);
        }
        public void NotifyObservers(Message msg)
        {
            foreach (IObserver o in observers)
            {
                o.Update(msg);
            }
        }
    }

    public class NewDataEvent
    {
        public object Message { get; set; }
    }

    public interface IObserver
    {
        void Update(Message msg);
    }
    public interface IObservable
    {
        void RegisterObserver(IObserver o);
        void RemoveObserver(IObserver o);
        void NotifyObservers(Message msg);
    }

    public class Message
    {
        public string email { get; set; }
        public string msg { get; set; }
    }
}

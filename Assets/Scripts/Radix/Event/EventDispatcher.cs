﻿using Radix.ErrorMangement;
using System;
using System.Collections.Generic;
using System.Linq;
// Define Complex Type
using EventDictionnary = System.Collections.Generic.Dictionary<System.Enum, System.Collections.Generic.IList<Radix.Event.EventListener>>;
using EventPair = System.Collections.Generic.KeyValuePair<System.Enum, System.Collections.Generic.IList<Radix.Event.EventListener>>;

namespace Radix.Event
{
    internal class EventDispatcher
    {
        private static EventDictionnary mEventDictionnary = new EventDictionnary();

        #region Register
        internal void RegisterEventListener(EventListener _listener)
        {
            Assert.CheckNull(_listener);

            if (!mEventDictionnary.ContainsKey(_listener.Event))
            {
                mEventDictionnary[_listener.Event] = new List<EventListener>();
            }

            if (!ContainListener(_listener))
            {
                mEventDictionnary[_listener.Event].Add(_listener);
            }
        }
        #endregion

        #region Unregister
        private void UnregisterEventListener(EventListener _listener, IList<EventListener> _list)
        {
            if (_listener != null)
            {
                _listener.Dispose();
                _list.Remove(_listener);
                _listener = null;
            }
        }

        internal void UnregisterEventListener(Enum _event, Type _listenerParent)
        {
            if (mEventDictionnary.ContainsKey(_event))
            {
                EventListener eventListener = mEventDictionnary[_event].FirstOrDefault((currentEventListener) => { return currentEventListener.Listener == _listenerParent; });
                UnregisterEventListener(eventListener, mEventDictionnary[_event]);
            }
        }

       /* internal void UnregisterAllEventsListeners(Type _listenerParent)
        {
            foreach (EventPair eventPair in mEventDictionnary)
            {
                EventListener eventListener = eventPair.Value.FirstOrDefault((currentEventListener) => { return currentEventListener.Listener == _listenerParent; });
                UnregisterEventListener(eventListener, eventPair.Value);
            }
        }*/

        internal void UnregisterAllEventsListeners(Type _Event)
        {
            foreach (EventPair eventPair in mEventDictionnary)
            {
                EventListener eventListener = eventPair.Value.FirstOrDefault((currentEventListener) => 
                {
                    return currentEventListener.Event.GetType() == _Event; 
                });
                UnregisterEventListener(eventListener, eventPair.Value);
            }
        }
        #endregion

        #region Dispatch
        internal void DispatchEvent(Enum _event, object _args, Type _listenerType)
        {
            if (mEventDictionnary.ContainsKey(_event))
            {

                foreach (EventListener listener in mEventDictionnary[_event])
                {
                    if (_listenerType == null || _listenerType == listener.Listener)
                    {
                        Assert.CheckNull(listener.Callback);
                        listener.Callback.DynamicInvoke(_event, _args);
                    }
                }
            }
        }

		internal void DispatchEvent(Enum _event, object _arg1, object _arg2, Type _listenerType)
		{
			if (mEventDictionnary.ContainsKey(_event))
			{
				
				foreach (EventListener listener in mEventDictionnary[_event])
				{
					if (_listenerType == null || _listenerType == listener.Listener)
					{
						Assert.CheckNull(listener.Callback);
						listener.Callback.DynamicInvoke(_event, _arg1, _arg2);
					}
				}
			}
		}
        #endregion

        #region Utility
        private bool ContainListener(EventListener _listener)
        {
            Assert.CheckNull(_listener);
            return (mEventDictionnary[_listener.Event].FirstOrDefault((eventListener) => { return eventListener.Equals(_listener); }) != null);
        }
        #endregion
    }
}

using System;
using System.ComponentModel;
using System.Collections;

//class MyEventManager:IDisposable
//{
//	EventHandlerList eventList = new EventHandlerList();
//	Hashtable eventObjectList = new Hashtable();
//	public void AddEvent(Event control, string eventname, EventHandler eventhandler)
//	{
//		string keystr = control.Name + eventname;
//		if (!eventObjectList.Contains(keystr)) eventObjectList.Add(keystr, new object());
//		object eventObject = eventObjectList[keystr];
//		switch (eventname)
//		{
//		case "Click":
//			control.Click += eventhandler;
//			break;
//		case "Enter":
//			control.Enter += eventhandler;
//			break;
//			//...
//			//这里可以添加更多的事件支持，这都是因为C# 不支持宏替换而采用的无奈之举
//			//当然用反射也可以，不过用反射就没必要用这种方法了。
//		}
//		eventList.AddHandler(eventObject, eventhandler);
//	}
//	public void DelEvent(Control control, string eventname)
//	{
//		string keystr = control.Name + eventname;
//		object eventObject = eventObjectList[keystr];
//		Delegate d = eventList[eventObject];
//		if (d == null) return;
//		foreach (Delegate dd in d.GetInvocationList())
//		{
//			switch (eventname)
//			{
//			case "Click":
//				control.Click -= (EventHandler)dd;
//				break;
//			case "Enter":
//				control.Enter -= (EventHandler)dd;
//				break;
//				//...
//				//这里可以添加更多的事件支持，这都是因为C# 不支持宏替换而采用的无奈之举
//				//当然用反射也可以，不过用反射就没必要用这种方法了。
//			}
//
//		}
//
//		eventList.RemoveHandler(eventObject, d);
//		eventObjectList.Remove(eventObject);
//	}
//
//
//
//	#region IDisposable Members
//
//	public void Dispose()
//	{
//		eventList.Dispose();            
//	}
//
//	#endregion
//} 
using System;
using System.Collections.Generic;

namespace PureMVC.Patterns
{
	
	public class Facade
	{
		private Dictionary<string,Proxy> ProxyMap = new Dictionary<string,Proxy> ();
		private Dictionary<string,Command> CommandMap = new Dictionary<string,Command> ();
		private Dictionary<string,Mediator> MediatorMap = new Dictionary<string,Mediator> ();
		private Dictionary<string,Mediator> MediNotiMap = new Dictionary<string,Mediator> ();

		protected static Facade _Facade;

		public static Facade Instance {
			get {
				if (_Facade == null) {
					_Facade = new Facade ();
				}
				return _Facade;
			}
		}

		public Facade ()
		{
			InitializeFacade ();
		}

		static Facade ()
		{
		}

		protected virtual void InitializeFacade ()
		{
			InitializeModel ();
			InitializeController ();
			InitializeView ();
		}

		protected virtual void InitializeController ()
		{
		}

		protected virtual void InitializeModel ()
		{
		}

		protected virtual void InitializeView ()
		{
		}


		public void RegisterProxy (Proxy proxy)
		{
			ProxyMap.Add (proxy.ProxyName, proxy);
			proxy.OnRegister ();
		}

		public Boolean HasProxy (string proxyName)
		{
			return ProxyMap.ContainsKey (proxyName);
		}

		public Proxy RetrieveProxy (string proxyName)
		{
			return ProxyMap [proxyName];
		}

		public void RemoveProxy (string proxyName)
		{
			var proxy = ProxyMap [proxyName] as Proxy;
			ProxyMap.Remove (proxyName);
			proxy.OnRemove ();
			proxy = null;
		}

		public void RegisterCommand (string cmdName, Command cmdClass)
		{
			CommandMap.Add (cmdName, cmdClass);
		}


		public bool HasCommand (string notificationName)
		{
			return CommandMap.ContainsKey (notificationName);
		}

		public void RemoveCommand (string notificationName)
		{
			CommandMap.Remove (notificationName);
		}

		public void RegisterMediator (Mediator mediator)
		{
			if (MediatorMap.ContainsKey (mediator.MediatorName) == false) {
				MediatorMap.Add (mediator.MediatorName, mediator);
			}
			foreach (var notiName in mediator.ListNotificationInterests()) {
				MediNotiMap.Add (notiName, mediator);
			}
			mediator.OnRegister ();
		}

		public bool HasMediator (string mediatorName)
		{
			return MediatorMap.ContainsKey (mediatorName);
		}

		public void RemoveMediator (string mediatorName)
		{
			var mediator = MediatorMap [mediatorName] as Mediator;
			MediatorMap.Remove (mediatorName);
			foreach (string notiName in mediator.ListNotificationInterests()) {
				MediNotiMap.Remove (notiName);
			}
			mediator.OnRemove ();
			mediator = null;
		}



		public Mediator RetrieveMediator (string mediatorName)
		{
			return MediatorMap [mediatorName] as Mediator;
		}

		public void SendNotification (string notificationName, object body = null, string type = null)
		{
			if (HasCommand (notificationName)) {
				var cmd = CommandMap [notificationName] as Command;
				cmd.Execute (notificationName, body, type);
			} 
			if (MediNotiMap.ContainsKey (notificationName)) {
				var mediator = MediNotiMap [notificationName] as Mediator;
				if (mediator != null) {
					mediator.HandleNotification (notificationName, body, type);
				}
			}
		}


	}
	

}
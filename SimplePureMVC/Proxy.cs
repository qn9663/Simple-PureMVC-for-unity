using System;
using System.Collections.Generic;

namespace PureMVC.Patterns
{
	public class Proxy
	{
		private string _ProxyName;

		public string ProxyName {
			get {
				return _ProxyName;
			}
		}

		public object Data;

		public Proxy (string name, object data)
		{
			this._ProxyName = name;
			this.Data = data;
		}

		public void SendNotification (string notificationName, object body = null, string type = null)
		{
			Facade.SendNotification (notificationName, body, type);
		}

		public virtual void OnRegister ()
		{
		}

		public virtual void OnRemove ()
		{
		}

		protected Facade Facade {
			get {
				return PureMVC.Patterns.Facade.Instance;
			}
		}
	}
}
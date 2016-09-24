using System;
using System.Collections;
using System.Collections.Generic;


namespace PureMVC.Patterns
{

	public class Mediator
	{
		private string _MediatorName;

		public string MediatorName {
			get {
				return _MediatorName;
			}
		}

		protected object _View;

		public object View {
			get {
				return _View;
			}
		}

		public Mediator (string name, object view = null)
		{
			this._MediatorName = name;
			this._View = view;
		}

		public void SendNotification (string notificationName, object body = null, string type = null)
		{
			Facade.Instance.SendNotification (notificationName, body, type);
		}

		public virtual void HandleNotification (string notificationName, object body = null, string type = null)
		{
		}

		public virtual IList<string> ListNotificationInterests ()
		{
			return new List<string> ();
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
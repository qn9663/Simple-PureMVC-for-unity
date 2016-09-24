using PureMVC.Patterns;

namespace PureMVC.Patterns
{

	public class Command
	{

		public virtual void Execute (string notificationName, object body = null, string type = null)
		{
		}

		public void sendNotification (string notificationName, object body = null, string type = null)
		{
			Facade.SendNotification (notificationName, body, type);
		}

		protected Facade Facade {
			get {
				return PureMVC.Patterns.Facade.Instance;
			}
		}
	}
}
这是一个基于MVC模式的单线程框架.可以很好的运行在Unity3D下.
去除了Command的反射机制所以运行不再创建新的Command对象.

启动方法:
using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	void Awake()
	{
		ApplicationFacade facade = ApplicationFacade.Instance as ApplicationFacade;
		facade.Startup ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


using UnityEngine;
using System.Collections;
using PureMVC.Patterns;

public class ApplicationFacade : Facade
{

	protected override void InitializeController()	{
		base.InitializeController();
		RegisterCommand(NotificationEnum.STARTUP, new StartupCommand());
	}



	public new static Facade Instance
	{
		get
		{
			if(_Facade == null)
			{
				if (_Facade == null)
				{
					_Facade = new ApplicationFacade();
				}
			}
			return _Facade;
		}
	}


	public void Startup()	{
		SendNotification(NotificationEnum.STARTUP, null);
	}

	protected ApplicationFacade()	{
	}


	static ApplicationFacade()	{
	}


}


	

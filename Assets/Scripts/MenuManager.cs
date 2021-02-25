using UnityEngine;

public class MenuManager : MonoBehaviour
{
	private void Awake()
	{
		MenuManager.Instance = this;
	}

	public void OpenMenu(string menuName)
	{
		for (int i = 0; i < this.menus.Length; i++)
		{
			bool flag = this.menus[i].menuName == menuName;
			if (flag)
			{
				this.OpenMenu(this.menus[i]);
			}
			else
			{
				bool open = this.menus[i].open;
				if (open)
				{
					this.CloseMenu(this.menus[i]);
				}
			}
		}
	}

	public void OpenMenu(Menu menu)
	{
		for (int i = 0; i < this.menus.Length; i++)
		{
			bool open = this.menus[i].open;
			if (open)
			{
				this.CloseMenu(this.menus[i]);
			}
		}
		menu.Open();
	}

	public void CloseMenu(Menu menu)
	{
		menu.Close();
	}

	public static MenuManager Instance;

	[SerializeField]
	private Menu[] menus;
}

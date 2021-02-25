using UnityEngine;

public class Menu : MonoBehaviour
{
	public void Open()
	{
		this.open = true;
		base.gameObject.SetActive(true);
	}

	public void Close()
	{
		this.open = false;
		base.gameObject.SetActive(false);
	}

	public string menuName;

	public bool open;
}

using UnityEngine;
using System.Collections;

[System.Serializable]
public class User 
{
	public string username;
	public string email;
	public string password;
	public bool isGuest = false;


	public User ShallowCopy()
	{
		return (User)this.MemberwiseClone();
	}
}










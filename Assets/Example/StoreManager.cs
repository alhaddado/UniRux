using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour {

	private static StoreManager _instance;
	public static StoreManager Instance
	{
		get
		{
			if (_instance == null)
			{
				Debug.Log("No instance of Storemanager!");
			}
			return _instance;
		}
		set
		{
			_instance = value;
		}
	}

	public Dictionary<string, Text> UITextBindings = new Dictionary<string, Text>();

	IEnumerator Start()
	{
		_instance = this;

		// create a sample store using the UserState class as a model for the state
		var userStore = new RuxStore<UserState>(UserState.GetDefaultState<UserState>(), new UserProvider());

		// create an instance of our Actions
		var userActions = new UserActions(userStore);

		yield return new WaitForSeconds(3);
		// subscribe to state changes
		userStore.onStateChanged += (UserState obj) => {
			UITextBindings["Text"].text = obj.ToString();;
			// Debug.Log("CALLBACK <color=green>" + obj.ToString() + "</color>");
		};


		// fire some actions!
		userActions.Login("adminwrong","test");
		userActions.Login("adminwrong","test123123");
		userActions.Login("admin","test");
		userActions.Login("admin","test");		
	}

}

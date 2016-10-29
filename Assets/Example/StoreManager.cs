using UnityEngine;
using System.Collections;

public class StoreManager : MonoBehaviour {

	void Start()
	{
		// create a sample store using the UserState class as a model for the state
		var userStore = new RuxStore<UserState>(UserState.GetDefaultState<UserState>(), new UserProvider());

		// create an instance of our Actions
		var userActions = new UserActions(userStore);

		// subscribe to state changes
		userStore.onStateChanged += (UserState obj) => {
			// Debug.Log("CALLBACK <color=green>" + obj.ToString() + "</color>");
		};

		// fire some actions!
		userActions.Login("adminwrong","test");
		userActions.Login("adminwrong","test123123");
		userActions.Login("admin","test");
		userActions.Login("admin","test");		
	}

}

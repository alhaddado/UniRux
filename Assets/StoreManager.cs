using UnityEngine;
using System.Collections;

public class StoreManager : MonoBehaviour {

	void Start()
	{
		var userProvider = new UserProvider();
		var userStore = new UserStore(UserState.defaultState, userProvider);
		var userActions = new UserActions(userStore);



		userStore.onStateChanged += (UserState obj) => {
			Debug.Log(obj.ToString());
		};


		userActions.LoginRequest("adminwrong","test");
		userActions.LoginRequest("admin","test");
	}

}

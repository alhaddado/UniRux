using UnityEngine;
using System;

public class UserActions : StoreActionsBase<UserState>
{
	public UserActions (RuxStore<UserState> store) : base (store)
	{
	}
	
	public class ResponseData : IPayload
	{
		public bool isSuccess = false;
		public string token = null;
		public User user = null;
		public string message = null;
	}

	#region Action Creators
	public void Login (string user, string password)
	{
		// First we make & dispatch the LoginRequest action right away.
		// this lets evyerone subscribed to the store know about the state change from the state provider.
		// which is helpful to show loading indicators and such..
		var responseData =  new ResponseData ();
		responseData.message = "Logging in...";
		var con = MakeContainer (StoreActionType.LoginRequest, responseData);
		Dispatch (con);
	
		// The state is available to us here in the (_store.state)
		// we can check it to see if we are already logged in and show error if we are.
		if (_store.state.isAuthed) {
			RequireLoggedOutAction(StoreActionType.LoginFailed);
			// no need to continue beyond this
			return;
		}

		// --
		// you can do your API or async magic operations at this point
		bool loginSuccess = user == "admin" && password == "test";

		// Make a new data response structure to fill with response data
		// You can use your own Response strucure to pass to the provider
		var data = new ResponseData ();

		// Handle api response and dispatch appropriate actions with data payloads
		if (loginSuccess) {
			data.token = "token";
			data.user = new User () {email = "email@email.o" , password = "why", username = user}; 
			data.message = "Logged in successfully.";
		} else {
			data.message = "Error: Wrong user or pass";
		}

		data.isSuccess = loginSuccess;

		// reusing the same con var from above, this time we fill it with data from the reponse
		con.type = loginSuccess ? StoreActionType.LoginSuccess : StoreActionType.LoginFailed;
		con.data = data;

		// Dispatch the response action
		Dispatch (con);
	}

	public void Register (string username, string password)
	{
		var responseData =  new ResponseData ();
		responseData.message = "Registering...";
		var con = MakeContainer (StoreActionType.RegisterRequest, responseData);
		Dispatch (con);

		if (_store.state.isAuthed) 
		{
			RequireLoggedOutAction(StoreActionType.RegisterFailed);
			return;
		}

		// --
		bool registerSuccess = true;
		var data = new ResponseData ();

		if(registerSuccess)
		{
			data.token = "token";
			data.user = new User () {email = "email@email.o" , password = "why", username = username}; 
			data.message = "Logged in successfully.";
		}
		else
			data.message = "Error: Wrong user or pass";
		data.isSuccess = registerSuccess;
		
		con.type = registerSuccess ? StoreActionType.RegisterSuccess : StoreActionType.RegisterFailed;
		con.data = data;
		
		// Dispatch the response action
		Dispatch (con);
	}


	private void RequireLoggedOutAction(StoreActionType action)
	{
		var containerData = new ResponseData () { message = "Error: You are already logged in. Please log out first then try again." };
		Dispatch (MakeContainer (action, containerData));
	}
	#endregion

}







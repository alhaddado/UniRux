using UnityEngine;
using System;


public class UserActions : StoreActionsBase<UserState>
{
	public UserActions(RuxStore<UserState> store) : base (store) {}
	
	public class LoginResponseData : IPayload
	{
		public bool isSuccess = false;
		public string token = null;
		public User user = null;
		public string message = null;
	}

	#region Action Creators
	public void Login (string user, string password)
	{
		// First we make & dispatch the login request action right away.
		// this lets evyerone subscribed to the store know about the state change from the state provider.
		// which is helpful to show loading indicators and such..
		var con = new StoreActionContainer ();
		con.type = StoreActionType.LoginRequest;

		// It is a good practise to write a description of what we are doing to pass with the data every time!
		con.data = new LoginResponseData () 
		{ message = "Logging in..." };

		Dispatch(con);
	
		// The state is available to us here in the (_store.state)
		// here we check it to see if we are already logged in and show error if we are
		if (_store.state.isAuthed)
		{
			// make a new action container to fill with our response data
			var conFailed = new StoreActionContainer ();

			// since data flow is unidirectional we should dispatch a noraml LoginResponseFailed action and let UI/Listeners read from other side
			conFailed.type = StoreActionType.LoginResponseFailed;
			conFailed.data = new LoginResponseData () { message = "Error: You are already logged in. Please log out first then try again." };
			
			Dispatch(conFailed);

			// no need to continue beyond this
			return;
		}

		// --
		// you can do your API or async magic operations at this point
		bool loginSuccess = user == "admin" && password == "test";

		// Make a new data response structure to fill with response data
		// You can use your own Response strucure to pass to the provider
		var data = new LoginResponseData ();

		// Handle api response and dispatch appropriate actions with data payloads
		if (loginSuccess)
		{
			data.token = "token";
			data.user = new User() {email = "email@email.o" , password = "why", username = user}; 
			data.message = "Logged in successfully.";
		}
		else
		{
			data.message = "Error: Wrong user or pass";
		}

		data.isSuccess = loginSuccess;

		// reusing the same con var from above this time we fill it with data from the reponse
		con.type = loginSuccess ? StoreActionType.LoginResponseSuccess : StoreActionType.LoginResponseFailed;
		con.data = data;

		// Dispatch the response action
		Dispatch(con);
	}
	#endregion
}







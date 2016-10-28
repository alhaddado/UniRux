using UnityEngine;


public class UserActions : StoreActionsBase<UserState>
{
	public UserActions(Store<UserState> store) : base (store)
	{}


	public class LoginResponseData : IPayload
	{
		public bool isSuccess;
		public string token = null;
		public User user = null;
		public string error = null;
	}

	public void LoginRequest (string user, string password)
	{
		// get shit with API
		var data = new LoginResponseData ();
		var con = new StoreActionContainer ();
		con.data = data;
		bool loginSuccess = user == "admin" && password == "test";
		con.type = loginSuccess ? StoreActionType.LoginResponseSuccess : StoreActionType.LoginResponseFailed;
		Dispatch(con);
	}

	public void LoginResponseSuccess (string user, string token)
	{
		var data = new LoginResponseData();

		// API bullshit

		data.isSuccess = true;
		data.token = "token";
		data.user = new User() {email = "email@email.o" , password = "why", username = user}; 

		var con = new StoreActionContainer ();
		con.data = data;
		con.type = StoreActionType.LoginResponseSuccess;


		Dispatch (con);
	}

	public void LoginResponseFailed (string failureReason)
	{
	}
}



public class UserState
{
	public User currentUser = null;
	public bool isAuthed = false;
	public bool isGuest = false;

	public static UserState defaultState {
		get {
			return new UserState () { currentUser = new User (), isAuthed = false, isGuest = false };
		}
	}

	public override string ToString()
	{
		return string.Format("--USER STATE--CurrentUser: {0}, isAuthed: {1}, isGuest: {2}", currentUser == null ? "null" : currentUser.username, isAuthed, isGuest);
	}
}


public class UserStore : Store<UserState>
{
	public UserStore(UserState initialState, StateProvider<UserState> provider) : base (initialState, provider)
	{
		
	}
}



public class UserProvider : StateProvider<UserState>
{
	public override UserState Provide (UserState state, StoreActionContainer action)
	{
		switch (action.type) {
		case StoreActionType.LoginRequest:
			return UserState.defaultState;
		case StoreActionType.LoginResponseFailed:
			return new UserState (){ isAuthed = false, currentUser = null, isGuest = false };
		case StoreActionType.LoginResponseSuccess:
			return new UserState (){ isAuthed = true, currentUser = ((UserActions.LoginResponseData)action.data).user, isGuest = false };
			default:
			break;
		}
		return base.Provide (state, action);
	}
}
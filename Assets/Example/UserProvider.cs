using UnityEngine;
using System;

public class UserProvider : RuxProvider<UserState>
{
	public override UserState Provide (UserState state, StoreActionContainer action)
	{
		UserState newState = base.Provide(state, action);


		var actionPayload = (UserActions.LoginResponseData) action.data;
		// Compute the next state using actionPayload
		// use action.data to fill values for next state
		switch (action.type) {
		case StoreActionType.LoginRequest:
			newState.isLoading = true;
			newState.message = actionPayload.message;
			break;
		case StoreActionType.LoginResponseFailed:
			newState.isLoading = false;
			newState.message = actionPayload.message;
			break;
		case StoreActionType.LoginResponseSuccess:
			newState.isAuthed = true; 
			newState.isLoading = false;
			newState.currentUser = actionPayload.user;
			newState.isGuest = actionPayload.user.isGuest;
			newState.message = actionPayload.message;
			break;
		default:
			break;
		}

		return newState;
	}
}

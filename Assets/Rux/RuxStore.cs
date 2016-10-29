using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// Rux Generic main store. 
/// </summary>
public class RuxStore<TState> where TState : RuxState
{
	// Main Store State
	// Holds the Data Structure/Model of ALL the CURRENT state of this store. 
	// Usually a deep nested object 
	// EXAMPLE: FriendsStore<FriendsState> (FriendsState has FriendsList..etc)
	public TState state = default(TState);

	// provider/reduxer
	RuxProvider<TState> provider;

	// listeners/bound stuff
	public Action<TState> onStateChanged = delegate {};

	public bool isDispatching { get; internal set;}

	bool Logging = true;

	public RuxStore(TState initialState, RuxProvider<TState> sProv)
	{
		state = initialState;
		provider = sProv;
	}

	public void Dispatch(StoreActionContainer action) 
	{

		isDispatching = true;

		// call provider on action with state to compute next state
		state = provider.Provide(state, action);
		++state.stateId;

		isDispatching = false;

		if (Logging) 
		{
			var c ="orange";
			if (action.type.ToString().Contains("Success"))
			    c = "lime";
			else if (action.type.ToString().Contains("Failed"))
				c = "red";

			Debug.Log(string.Format("<color=white>ActionType</color>: <color={0}>{1}</color>\n" +
			                        "<color=yellow>{2}</color> <color=white>State ID</color>: <color=yellow>{3}</color>\n{4}",
			                        c, action.type, state.GetType() ,state.stateId, state));
			
		}
		// inform listeners of changes
		onStateChanged(state);
	}
}

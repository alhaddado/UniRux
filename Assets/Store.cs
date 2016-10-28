using UnityEngine;
using System.Collections;
using System;

public class Store<TState> {

	// Main Store State
	// Holds the Data Structure/Model of ALL the CURRENT state of this store. 
	// Usually a deep nested object (JSON/XML) Like strucure 
	// EXAMPLE: FriendsStore<FriendsState>
	TState state = default(TState);

	// provider/reduxer
	StateProvider<TState> provider;

	// listeners/bound stuff
	public Action<TState> onStateChanged = delegate {};

	public bool isDispatching { get; internal set;}

	public Store(TState initialState, StateProvider<TState> sProv)
	{
		state = initialState;
		provider = sProv;
	}

	public void Dispatch(StoreActionContainer action) 
	{
		// sanity / error checks
		if (isDispatching)
		{
			// queue actions or allow multi/threaded?
		}

		// call provider on action with state to compute next state
		isDispatching = true;

		state = provider.Provide(state, action);

		isDispatching = false;

		// inform listeners of changes
		onStateChanged(state);
	}

}

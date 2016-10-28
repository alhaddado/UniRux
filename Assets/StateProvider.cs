using UnityEngine;
using System.Collections;

public class StateProvider<TState>
{
	public virtual TState Provide(TState state, StoreActionContainer action) 
	{
		TState nextState = default(TState);
		// switch on (action.type) and give new state for each action in StoreActionTypes Enum
		// use action.data to fill values for next state
		return nextState;
	}
}


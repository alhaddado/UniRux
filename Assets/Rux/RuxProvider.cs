using UnityEngine;
using System.Collections;

public class RuxProvider<TState> where TState : RuxState
{
	/// <summary>
	/// Provide the next state from current State and Action
	/// switch on (action.type) in your override method 
	/// and set new state for each action type you defined in StoreActionTypes Enum
	/// </summary>
	/// <param name="state">State.</param>
	/// <param name="action">Action.</param>
	public virtual TState Provide(TState state, StoreActionContainer action) 
	{
		// Make a deep clone of the state object
		return Extentions.DeepClone<TState>(state);

	}
}


using UnityEngine;
using System.Collections;

public enum StoreActionType
{
	LoginRequest,
	LoginResponseSuccess,
	LoginResponseFailed

}

public interface IPayload
{
	
}

public class StoreActionContainer
{
	public StoreActionType type;
	public IPayload data;
}

public class StoreActionsBase <TState>  where TState : RuxState
{
	protected RuxStore<TState> _store;

	public StoreActionsBase(RuxStore<TState> store)
	{
		_store = store;
	}

	protected void Dispatch (StoreActionContainer container) 
	{
		_store.Dispatch (container);
	}
}
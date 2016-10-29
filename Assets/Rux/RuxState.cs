using UnityEngine;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[System.Serializable]
public class RuxState
{
	public int stateId = 0;
	
	public override string ToString()
	{
		return JsonConvert.SerializeObject(
			this, Formatting.Indented,
			new JsonConverter[] {new StringEnumConverter()});
	}

	public static TState GetDefaultState<TState>() where TState : new()
	{
		return new TState();
	}
}











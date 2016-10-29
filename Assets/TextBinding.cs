using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TextBinding : MonoBehaviour
{
	public string TextInitialValue = "Hey dude. I Have some Initial Value"	;

	public Text TextComponent;

	// Use this for initialization
	IEnumerator Start ()
	{
		yield return new WaitForSeconds(1);
		TextComponent.text = TextInitialValue;
		StoreManager.Instance.UITextBindings.Add("Text", TextComponent);
	}
}
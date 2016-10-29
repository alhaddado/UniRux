using UnityEngine;
using System;

[System.Serializable]
public class UserState : RuxState
{
	public User currentUser {get; set;}
	public bool isAuthed 	{get; set;}
	public bool isGuest 	{get; set;}
	public bool isLoading 	{get; set;}
	public string message 	{get; set;}
}

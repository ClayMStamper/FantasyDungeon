using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {

	[SerializeField]
	private int baseStat;

	private List<int> modifiers = new List<int>();

	public int GetValue(){
		int modifiedStat = baseStat;
		// add each modifier value x in modifers<> to modified stat
		modifiers.ForEach (x => modifiedStat += x); 
		return modifiedStat;
	}

	public void AddModifier(int modifier){
		if (modifier != 0) {
			modifiers.Add (modifier);
		}
	}

	public void RemoveModifier (int Modifier){
		if (Modifier != 0) {
			modifiers.Remove (Modifier);
		}
	}
}

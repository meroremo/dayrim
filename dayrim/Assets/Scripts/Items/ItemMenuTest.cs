using UnityEngine;
using System.Collections;

public class ItemMenuTest : MonoBehaviour {

	public ItemManager manager;

	// Use this for initialization
	void Start () {
	
		manager = new ItemManager ();

		manager.addItem(new Item("Gummiball", "Supersaftiger Gummiball", 11, null));
		manager.addItem(new Item("Dose", "Rote Sprühdose", 12, null));
		manager.addItem(new Item("Plastikpflanze", "Ein Plastikflaschen-Konstrukt. Irgendwie schön!", 12, null));

		Debug.Log ("Vor dem Kombinieren");
		foreach (Item i in manager.inventar) {
			Debug.Log (i.getName());
		}

		Debug.Log ("Nach Kombinationsversuch Ball mit Ball");
		manager.possibleItemCombinations(manager.getItem(0), manager.getItem(0));
		foreach (Item i in manager.inventar) {
			Debug.Log (i.getName());
		}

		// Weg 1
		/*
		Debug.Log ("Nach Kombinationsversuch Ball mit Dose");
		manager.kombinieren (manager.getItem(0), manager.getItem(1));
		foreach (Item i in manager.inventar) {
			Debug.Log (i.getName());
		}
		manager.kombinieren (manager.getItem (1), manager.getItem (2));
		foreach (Item i in manager.inventar) {
			Debug.Log (i.getName());
		}
		*/

		//Weg 2
		manager.possibleItemCombinations(manager.getItem(0), manager.getItem(2));
		foreach (Item i in manager.inventar) {
			Debug.Log (i.getName());
		}
		manager.possibleItemCombinations(manager.getItem (0), manager.getItem (1));
		foreach (Item i in manager.inventar) {
			Debug.Log (i.getName());
		}

			

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

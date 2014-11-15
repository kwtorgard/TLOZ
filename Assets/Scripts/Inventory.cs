﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Inventory : MonoBehaviour {

	public Transform anchor;
	public float itemDistance = 0.5f;
	public List<GameObject> items = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonUp("Jump")){
			removeItem(items[1]);
		}
	}

	public void addItem(GameObject item){
		item.transform.parent = anchor;
		item.transform.localPosition = new Vector3(0 + itemDistance * items.Count, 0, 0);
		item.layer = LayerMask.NameToLayer("UI");
		item.tag = "Inventory";
		items.Add(item);
		//settleItems();
	}

	public void removeItem(GameObject item){
		// Move/destroy item?
		item.SetActive(false);


		items.Remove(item);
		settleItems();
	}

	/**
	 * After adding, moving or removing an item, set all items into their place
	 */
	private void settleItems(){
		for(int n = 0 ; n < items.Count ; n++){
			iTween.MoveTo(items[n], iTween.Hash("x", 0 + itemDistance * n, "y", 0, "z", 0, "time", 0.5f, "islocal", true));
			//items[n].transform.localPosition = new Vector3( * n, 0, 0);
		}
	}

}
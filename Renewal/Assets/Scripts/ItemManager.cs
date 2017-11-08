using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    public int numItems; //the number of pickup items in the scene
    public GameObject clickable; //the clicable scene change object that activates when all the items are picked up

    private MenuClickable clicker; 

	// Use this for initialization
	void Start () {
        clicker = clickable.GetComponent<MenuClickable>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DecrementItems()
    {
        numItems -= 1;
        if (numItems <= 0)
        {
            clicker.condition = true;
        }
    }
}

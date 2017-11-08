using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupScript : MonoBehaviour {

    public float activeDistance;
	public float textDuration;
    public GameObject player;

    private bool visible;
    private float dist;

    private bool activate;
    private float timer;

	public int startL;
	public int endL;
	
    public GameObject manager; //the manager that this item signals when it is picked up
	public GameObject textManager;
    private ItemManager managerScript;
	private TextBoxManager manageTexts;

	// Use this for initialization
	void Start () {
        visible = false;
        dist = Mathf.Infinity;
        activate = false;
        timer = 0;
        managerScript = manager.GetComponent<ItemManager>();
		manageTexts = textManager.GetComponent<TextBoxManager>();
		
    }
	
	// Update is called once per frame
	void Update () {

        visible = GetComponent<Renderer>().isVisible;
        dist = Vector3.Distance(transform.position, player.transform.position);

        if (visible && dist <= activeDistance)
        {
			if(!activate)
			{
				manageTexts.getNumbers(startL, endL);
                managerScript.DecrementItems();
		
			}
            activate = true;
        }

        if (activate)
        {
            timer += Time.deltaTime;
            if(timer >= textDuration)
            {
                Destroy(this.gameObject);
            }
        }
    }

 /*   private void OnGUI()
    {
        if (activate)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = textCol;
			Vector3 x = new Vector3(transform.position.x + 50, transform.position.y + 15, transform.position.z);
            GUI.Label(new Rect(x, rectSize), message, style);
        }
    }*/
}

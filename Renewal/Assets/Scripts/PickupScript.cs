using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupScript : MonoBehaviour {

    public string message;
    public float activeDistance;
    public float textDuration;
    public Vector2 rectSize;
    public Color textCol;
    public GameObject player;

    private bool visible;
    private float dist;

    private bool activate;
    private float timer;

    public GameObject manager; //the manager that this item signals when it is picked up
    private ItemManager managerScript;

	// Use this for initialization
	void Start () {
        visible = false;
        dist = Mathf.Infinity;
        activate = false;
        timer = 0;
        managerScript = manager.GetComponent<ItemManager>();
    }
	
	// Update is called once per frame
	void Update () {

        visible = GetComponent<Renderer>().isVisible;
        dist = Vector3.Distance(transform.position, player.transform.position);

        if (visible && dist <= activeDistance)
        {
            activate = true;
        }

        if (activate)
        {
            timer += Time.deltaTime;
            if(timer >= textDuration)
            {
                managerScript.DecrementItems();
                Destroy(this.gameObject);
            }
        }
    }

    private void OnGUI()
    {
        if (activate)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = textCol;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            GUI.Label(new Rect(screenPos, rectSize), message, style);
        }
    }
}

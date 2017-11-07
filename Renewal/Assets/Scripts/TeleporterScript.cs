using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour {

    public float activateTime; //time it takes for the player to be on the teleporter for the teleporter to activate
    public GameObject destination; //the location that the teleporter sends the player to
    public GameObject player; //the player
    public Vector3 teleportPointOffest; //vector from this teleporter's position that the player will travel to

    public Color activeCol;
    public Color chargingCol;
    public Color inactiveCol;
    
    private float chargeup;
    private bool active;
    private TeleporterScript destScript;
    private Material mat;
    private bool onTeleporter;

	// Use this for initialization
	void Start () {
        chargeup = 0;
        active = true;
        destScript = destination.GetComponent<TeleporterScript>();
        onTeleporter = false;
        mat = GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update () {

        if(onTeleporter)
        {
            chargeup += Time.deltaTime;

            if (chargeup >= activateTime && active)
            {
                chargeup = 0;
                destScript.RecieveTeleport();
            }
        }
        else if(!onTeleporter)
        {
            chargeup -= Time.deltaTime;
            if (chargeup <= 0)
            {
                active = true;
            }
        }
        
        chargeup = Mathf.Clamp(chargeup, 0, activateTime);

        if (!active)
        {
            mat.color = Color.Lerp(activeCol, inactiveCol, chargeup / activateTime);
        }
        else
        {
            mat.color = Color.Lerp(activeCol, chargingCol, chargeup / activateTime);
        }
    }

    public void RecieveTeleport()
    {
        player.transform.position = transform.position + teleportPointOffest;
        active = false;
        chargeup = activateTime;
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.Equals(player))
        {
            onTeleporter = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.Equals(player))
        {
            onTeleporter = false;
        }
    }
}

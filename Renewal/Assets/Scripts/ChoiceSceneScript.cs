using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChoiceSceneScript : MonoBehaviour {

    public string initialMsg;
    public string successMsg;
    public string failMsg;

    public GameObject OptionButton;
    public string[] buttonMsgs;
    public bool[] buttonPressIsSucess;
    public Vector2[] buttonLocs;

    public string successScene;
    public string failScene;

    public Vector2 mainBoxLoc;
    public Vector2 mainBoxSize;
    public Color mainBoxTextCol;

    private Button[] buttons;

    private bool sucess;
    private bool fail;

    // Use this for initialization
    void Start () {
        sucess = false;
        fail = false;

        for(int i=0; i<buttonMsgs.Length; i++)
        {

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = mainBoxTextCol;
        if (!sucess && !fail)
        {
            GUI.Label(new Rect(mainBoxLoc, mainBoxSize), initialMsg, style);
        }
        else if(sucess)
        {
            GUI.Label(new Rect(mainBoxLoc, mainBoxSize), successMsg, style);
        }
        else if (fail)
        {
            GUI.Label(new Rect(mainBoxLoc, mainBoxSize), failMsg, style);
        }
    }

    public void CreateButton(Transform panel, Vector3 position, Vector2 size, UnityEngine.Events.UnityAction method)
    {
        GameObject button = new GameObject();
        button.transform.parent = panel;
        button.AddComponent<RectTransform>();
        button.AddComponent<Button>();
        button.transform.position = position;
        button.GetComponent<RectTransform>().SetSize(size);
        button.GetComponent<Button>().onClick.AddListener(method);
    }

    public void RecievePush(bool result)
    {
        if(!sucess && !fail)
        {
            if (result)
            {
                sucess = true;
                fail = false;
            }
            else
            {
                fail = true;
                sucess = false;
            }
        }
    }
}

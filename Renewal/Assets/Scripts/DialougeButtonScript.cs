using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeButtonScript : MonoBehaviour {

    public bool isSuccessButton;
    public string optionText;
    public ChoiceSceneScript controller;

    // Use this for initialization
    void Start () {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //start over game when pressing button
    void TaskOnClick()
    {
        controller.RecievePush(isSuccessButton);
    }

    public void Setup(bool suc, string txt, ChoiceSceneScript con)
    {
        isSuccessButton = suc;
        optionText = txt;
        controller = con;
    }
}

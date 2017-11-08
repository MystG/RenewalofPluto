using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {
	
	public GameObject textBox;
	public Text theText;
	public TextAsset textfiles;
	public string[] textLines;
	public int currentLine;
	public int endAtLine;
	
	// Use this for initialization
	void Start () {
		if(textfiles != null){
			textLines = (textfiles.text.Split('\n'));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(currentLine > endAtLine){
				theText.text = "";
		}else{
			theText.text = textLines[currentLine]; 
			if(Input.GetKeyDown(KeyCode.Return)){
				currentLine += 1;
			}
		}
	}
	public void getNumbers(int x, int y){
		currentLine = x;
		endAtLine = y;
		Update();
	}
}

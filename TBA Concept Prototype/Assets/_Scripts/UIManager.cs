using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UIManager : Manager<UIManager> {

    Canvas canvas;
    //Button combatBtn;
    List<Button> uiButtons;
    List<Text> uiLabels;
    
    void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();

        //combatBtn = canvas.transform.FindChild("CombatBtn").GetComponent<Button>();
        //combatBtn.onClick.AddListener(() => OnClickButton(combatBtn));

        uiButtons = (canvas.transform.GetComponentsInChildren<Button>()).ToList();
        foreach(Button btn in uiButtons)
        {
            btn.onClick.AddListener(() => OnClickButton(btn));
        }
        print("number of buttons: " + uiButtons.Count);

        uiLabels = new List<Text>();
        GameObject[] panels = GameObject.FindGameObjectsWithTag("Panel");
        foreach (GameObject obj in panels)
        {
            foreach(Transform child in obj.transform)
            {
                if (child.GetComponent<Text>() != null)
                    uiLabels.Add(child.GetComponent<Text>());
            }            
        }
        print("number of labels: " + uiButtons.Count);
    }

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () {
        Text text;
        //Button btn;

        if (!GameManager.Instance.IsInCombat())
        {
            text = GetButton("CombatBtn").transform.FindChild("Text").GetComponent<Text>();
            text.text = "Start Combat";

            text = GetLabel("CombatStateLabel");
            text.text = "-- Not in Combat --";
        }            
        else
        {
            text = GetButton("CombatBtn").transform.FindChild("Text").GetComponent<Text>();
            text.text = "End Combat";

            text = GetLabel("CombatStateLabel");
            text.text = "-- In Combat -- ";
        }
            
	}

    void OnClickButton(Button btn)
    {
        print("clicked " + btn.name);

        switch(btn.name)
        {
            case "CombatBtn":
                GameManager.Instance.CombatBtnUsed();
                break;
        }
    }

    Button GetButton(string buttonName)
    {
        Button btn;

        btn = uiButtons.Find(p => p.name == buttonName);

        return btn;
    }

    Text GetLabel(string labelName)
    {
        Text text;

        text = uiLabels.Find(p => p.name == labelName);

        return text;
    }
}

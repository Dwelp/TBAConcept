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
    Dictionary<Unit, GameObject> unitPanels;

    // Conditions
    public bool unitCombatUIEnabled;
    
    void Awake()
    {
        unitPanels = new Dictionary<Unit, GameObject>();        
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();

        //combatBtn = canvas.transform.FindChild("CombatBtn").GetComponent<Button>();
        //combatBtn.onClick.AddListener(() => OnClickButton(combatBtn));

        uiButtons = (canvas.transform.GetComponentsInChildren<Button>()).ToList();
        foreach(Button btn in uiButtons)
        {
            Button capturedBtn = btn;
            capturedBtn.onClick.AddListener(() => OnClickButton(capturedBtn));
        }
        print("number of buttons: " + uiButtons.Count);

        Transform[] children = canvas.GetComponentsInChildren<Transform>();
        Transform[] labels = children.Where(p => p.tag == "Label").ToArray();
        uiLabels = (labels.ToList()).Select(p => p.GetComponent<Text>()).ToList();
        print("number of labels: " + uiLabels.Count);
    }

	// Use this for initialization
	void Start () 
    {

	}
	
  
	// Update is called once per frame
	void Update () {
        Text text;
        Button btn;

        if (!GameManager.Instance.IsInCombat())
        {
            unitCombatUIEnabled = false;

            text = GetButton("CombatBtn").transform.FindChild("Text").GetComponent<Text>();
            text.text = "Start Combat";

            text = GetLabel("CombatStateLabel");
            text.text = "-- Not in Combat --";

            text = GetLabel("TurnControllerLabel");
            text.text = CombatManager.Instance.turnController.ToString();

            btn = GetButton("EndTurnBtn");
            btn.gameObject.SetActive(false);
        }            
        else
        {
            unitCombatUIEnabled = true;

            text = GetButton("CombatBtn").transform.FindChild("Text").GetComponent<Text>();
            text.text = "End Combat";

            text = GetLabel("CombatStateLabel");
            text.text = "-- In Combat -- ";

            if (CombatManager.Instance.turnController == CombatManager.TurnController.Player)
            {
                btn = GetButton("EndTurnBtn");
                btn.gameObject.SetActive(true);
            }
            else
            {
                btn = GetButton("EndTurnBtn");
                btn.gameObject.SetActive(false);
            }
        }
            
	}

    void OnClickButton(Button btn)
    {
        //print("clicked " + btn.name);

        switch(btn.name)
        {
            case "CombatBtn":
                GameManager.Instance.CombatBtnUsed();
                break;

            case "EndTurnBtn":
                CombatManager.Instance.EndTurn();
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

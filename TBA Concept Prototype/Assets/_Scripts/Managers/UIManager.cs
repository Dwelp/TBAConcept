using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class UIManager : Manager<UIManager> {

    Canvas canvas;
    //Button combatBtn;
    List<Button> uiButtons;
    List<Text> uiLabels;
    Dictionary<Unit, GameObject> unitPanels;
    Dictionary<Button, string> buttonBinds;

    bool inTargetSelection;
    TargetSelectionDecalObject tsDecalObject;

    GameObject skillBtnPanel;
    GameObject debugPanel;

    // Conditions
    public bool unitCombatUIEnabled;
    
    void Awake()
    {
        uiButtons = new List<Button>();
        unitPanels = new Dictionary<Unit, GameObject>();
        buttonBinds = new Dictionary<Button, string>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        skillBtnPanel = canvas.transform.FindChild("UnitActions_Buttons").gameObject;
        debugPanel = canvas.transform.FindChild("Debug").gameObject;

        //combatBtn = canvas.transform.FindChild("CombatBtn").GetComponent<Button>();
        //combatBtn.onClick.AddListener(() => OnClickButton(combatBtn));

        List<Button> buttonsOnCanvas = (canvas.transform.GetComponentsInChildren<Button>()).ToList();
        foreach (Button btn in buttonsOnCanvas)
        {
            //print("found button " + btn.GetInstanceID());
            AddButton(btn);
        }
        print("number of buttons: " + uiButtons.Count);

        Transform[] children = canvas.GetComponentsInChildren<Transform>();
        Transform[] labels = children.Where(p => p.tag == "Label").ToArray();
        uiLabels = (labels.ToList()).Select(p => p.GetComponent<Text>()).ToList();
        print("number of labels: " + uiLabels.Count);

        // Add Skills to buttons
        Button skillBtn;
        ButtonData[] btnDataArray = skillBtnPanel.transform.GetComponentsInChildren<ButtonData>();
        foreach(ButtonData btnData in btnDataArray)
        {
            skillBtn = GetButton(btnData.name);
            buttonBinds.Add(skillBtn, btnData.attachedAction);

            /*
            if(btnData.attachedAction != "")
            {
                skillBtn = GetButton(btnData.name);
                buttonBinds.Add(skillBtn, btnData.attachedAction);
            }
             * */
        }           
    }

	// Use this for initialization
	void Start () 
    {

	}	
  
	// Update is called once per frame
	void Update () {
        Text text;
        Button btn;
        Unit activeUnit;

        if (!GameManager.Instance.IsInCombat())
        {
            unitCombatUIEnabled = false;

            text = GetButton("CombatBtn").transform.FindChild("Text").GetComponent<Text>();
            text.text = "Start Combat";

            text = GetLabel("CombatStateLabel");
            text.text = "-- Not in Combat --";

            text = GetLabel("TurnControllerLabel");
            text.transform.parent.gameObject.SetActive(false);

            btn = GetButton("EndTurnBtn");
            btn.gameObject.SetActive(false);

            btn = GetButton("MoveBtn");
            btn.gameObject.SetActive(false);

            skillBtnPanel.SetActive(false);
        }            
        else
        {
            unitCombatUIEnabled = true;
            activeUnit = CombatManager.Instance.GetActiveUnit();

            text = GetButton("CombatBtn").transform.FindChild("Text").GetComponent<Text>();
            text.text = "End Combat";

            text = GetLabel("CombatStateLabel");
            text.text = "-- In Combat -- ";

            text = GetLabel("TurnControllerLabel");
            text.transform.parent.gameObject.SetActive(true);
            text.text = CombatManager.Instance.turnController.ToString();

            if (CombatManager.Instance.turnController == CombatManager.TurnController.Player)
            {
                btn = GetButton("EndTurnBtn");
                btn.gameObject.SetActive(true);

                if(!activeUnit.hasMoved)
                {
                    btn = GetButton("MoveBtn");
                    btn.gameObject.SetActive(true);
                    btn.interactable = true;
                }
                else
                {
                    btn = GetButton("MoveBtn");
                    btn.gameObject.SetActive(true);
                    btn.interactable = false;
                }

                skillBtnPanel.SetActive(true);
                UpdateUnitSkillPanel();
            }
            else
            {
                btn = GetButton("EndTurnBtn");
                btn.gameObject.SetActive(false);

                btn = GetButton("MoveBtn");
                btn.gameObject.SetActive(false);

                skillBtnPanel.SetActive(false);
            }
        }

        if(inTargetSelection)
        {
            if (CombatManager.Instance.turnController == CombatManager.TurnController.Player)
            {
                if (tsDecalObject.targetSelectionDecal.followMouse)
                {
                    Vector3 mousePos = Utilities.GetMouseWorldLocation();
                    tsDecalObject.targetSelectionDecal.AimAt(mousePos);
                    //mousePos.y = tsDecalObject.transform.position.y;
                    //tsDecalObject.transform.LookAt(mousePos);
                    //Vector3 currentRot = tsDecalObject.transform.rotation.eulerAngles;
                }
            }
        }

        if (GameManager.Instance.IsInCombat())
        {
            debugPanel.SetActive(true);
            DrawDebug();
        }
        else
        {
            debugPanel.SetActive(false);
        }
	}

    void UpdateUnitSkillPanel()
    {
        Unit unit = CombatManager.Instance.GetActiveUnit();
        //List<UnitAction> skills = unit.unitActions.Where(p => typeof(UA_Skill).IsAssignableFrom(p.GetType())).ToList();

        foreach(Button btn in buttonBinds.Keys)
        {
            string actionName = buttonBinds[btn];
            UnitAction action = unit.GetAction(actionName);

            if (action != null && action.CanBeUsed())
            {
                btn.gameObject.SetActive(true);

                Text text = btn.GetComponentInChildren<Text>();
                text.text = actionName;
            }
            else
            {
                btn.gameObject.SetActive(false);
            }
        }
    }

    void AddButton(Button btn)
    {
        if(uiButtons.Find(p => p.GetInstanceID() == btn.GetInstanceID()) == null)
        {
            uiButtons.Add(btn);
            btn.onClick.AddListener(() => OnClickButton(btn));
        }
    }

    void RemoveButton(Button btn)
    {
        if (uiButtons.Find(p => p.GetInstanceID() == btn.GetInstanceID()) != null)
        {
            uiButtons.Remove(btn);
        }
    }

    public void EnterTargetSelection(UnitAction action)
    {
        if (action == null)
            return;

        if (!inTargetSelection)
        {
            inTargetSelection = true;
            //print("entered target selection");

            tsDecalObject = action.CrateTSDecal();
        }
    }

    public void ExitTargetSelection()
    {
        if (inTargetSelection)
        {
            inTargetSelection = false;
            //print("exit target selection");

            Destroy(tsDecalObject.gameObject);
        }
    }

    public void OnClickButton(string buttonName)
    {
        Button btn = GetButton(buttonName);

        if (btn != null)
            OnClickButton(btn);
    }

    public void OnClickButton(Button btn)
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

            // Actions
            default:
                ButtonData btnData = btn.GetComponent<ButtonData>();
                CombatManager.Instance.UseActionButton(btnData.attachedAction);
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

    void DrawDebug()
    {
        Text debugText = GetLabel("DebugText");
        
        List<Unit> combatUnits;
        combatUnits = (GameObject.FindObjectsOfType<Unit>()).ToList();
        combatUnits = combatUnits.OrderBy(p => p.speed).ToList();

        debugText.text = "Units in combat: " + combatUnits.Count + "\n";

        foreach(Unit unit in combatUnits)
        {
            debugText.text += "Unit Name: " + unit.name + " | " + "HP: " + unit.currentHealth + " / " + unit.maxHealth + " | " + "Combo Step: " + unit.comboStep + "\n";
        }
    }
}

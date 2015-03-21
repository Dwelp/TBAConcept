using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    Vector3 fillPos;
    Vector3 emptyPos;

    public Image barImage;
    public RectTransform barTransform;

    public Unit unit;
    
    void Awake()
    {
        fillPos = barTransform.localPosition;
        emptyPos = new Vector3(fillPos.x - barTransform.rect.width, fillPos.y, fillPos.z);
    }

	// Use this for initialization
	void Start () 
    {
	        
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (unit != null)
            UpdateHealthBar();
	}

    public void UpdateHealthBar()
    {
        float percentage = unit.currentHealth / unit.maxHealth;
        //print(percentage);
        barTransform.localPosition = Vector3.Lerp(emptyPos, fillPos, percentage);        
    }
}

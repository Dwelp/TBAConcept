using UnityEngine;
using System.Collections;

public class GameManager : Manager<GameManager>
{
    bool inCombat;

    void Awake()
    {

    }

     // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartCombat()
    {
        inCombat = true;

        CombatManager.Instance.InitCombat();
    }

    void EndCombat()
    {
        inCombat = false;
    }


    public bool IsInCombat()
    {
        return inCombat;
    }

    // UI Events

    public void CombatBtnUsed()
    {
        if (!inCombat)
            StartCombat();
        else
            EndCombat();
    }

    public GameCamera GetGameCamera()
    {
        GameCamera gameCamera = Camera.main.gameObject.GetComponent<GameCamera>();

        return gameCamera;
    }
}



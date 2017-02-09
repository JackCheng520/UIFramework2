using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TinyTeam.UI;
using Assets.UISystem;

public class GameMain : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        GameUI.ShowUI<GameUI_Mgr_MainUI>();
    }
}

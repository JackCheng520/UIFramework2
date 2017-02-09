using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// ================================
//* 功能描述：MainUI  
//* 创 建 者：chenghaixiao
//* 创建日期：2017/2/8 17:24:24
// ================================
namespace Assets.UISystem
{
    public class GameUI_Mgr_MainUI : GameUI
    {
        public GameUI_Mgr_MainUI() :
            base(UIType.Normal, true, UICollider.None)
        {
            uiPath = "UIPrefab/UIMain";
        }

        private Button btnClose;

        private Button btnSkill;

        private Button btnInfo;

        private Button btnBattle;

        public override void Init()
        {
            base.Init();
            btnClose = this.gameObject.transform.FindChild("Topbar/btn_back").GetComponent<Button>();
            btnClose.onClick.RemoveAllListeners();
            btnClose.onClick.AddListener(() => {
                GameUI.CloseUI<GameUI_Mgr_MainUI>();
            });

            btnSkill = this.gameObject.transform.FindChild("btn_skill").GetComponent<Button>();
            btnSkill.onClick.RemoveAllListeners();
            btnSkill.onClick.AddListener(() => {
                GameUI.ShowUI<GameUI_Mgr_Skill>();
            });

            btnInfo = this.gameObject.transform.FindChild("Topbar/btn_notice").GetComponent<Button>();
            btnInfo.onClick.RemoveAllListeners();
            btnInfo.onClick.AddListener(() =>
            {
                GameUI.ShowUI<GameUI_Mgr_MessageBox>();
            });

            btnBattle = this.gameObject.transform.FindChild("btn_battle").GetComponent<Button>();
            btnBattle.onClick.RemoveAllListeners();
            btnBattle.onClick.AddListener(() =>
            {
                GameUI.ShowUI<GameUI_Mgr_Battle>();
            });
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

// ================================
//* 功能描述：GameUI_Mgr_Battle  
//* 创 建 者：chenghaixiao
//* 创建日期：2017/2/9 13:07:01
// ================================
namespace Assets.UISystem
{
    public class GameUI_Mgr_Battle : GameUI
    {
        public GameUI_Mgr_Battle()
            : base(UIType.Normal, true, UICollider.Normal)
        {
            uiPath = "UIPrefab/UIBattle";
        }

        private Button btnClose;

        public override void Init()
        {
            base.Init();
            btnClose = this.gameObject.transform.FindChild("btn_close").GetComponent<Button>();
            btnClose.onClick.RemoveAllListeners();
            btnClose.onClick.AddListener(() =>
            {
                GameUI.CloseUI<GameUI_Mgr_Battle>();
            });
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

// ================================
//* 功能描述：GameUI_Mgr_MessageBox  
//* 创 建 者：chenghaixiao
//* 创建日期：2017/2/9 13:14:33
// ================================
namespace Assets.UISystem
{
    public class GameUI_Mgr_MessageBox : GameUI
    {
        public GameUI_Mgr_MessageBox() : base(UIType.Message, false, UICollider.Normal) {
            uiPath = "UIPrefab/Notice";
        }

        private Button btnConfirm;

        public override void Init()
        {
            btnConfirm = this.gameObject.transform.FindChild("content/btn_confim").GetComponent<Button>();
            btnConfirm.onClick.RemoveAllListeners();
            btnConfirm.onClick.AddListener(() =>
            {
                GameUI.CloseUI<GameUI_Mgr_MessageBox>();
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// ================================
//* 功能描述：UISkill  
//* 创 建 者：chenghaixiao
//* 创建日期：2017/2/9 9:23:27
// ================================
namespace Assets.UISystem
{
    public class GameUI_Mgr_Skill : GameUI
    {
        public GameUI_Mgr_Skill()
            : base(UIType.Normal, true, UICollider.Normal)
        {
            uiPath = "UIPrefab/UISkill";
        }

        private Transform transParent;

        private GameObject goBaseItem;

        private List<GameObject> listItems = new List<GameObject>();

        private GameObject goDesc;

        private Button btnClose;

        private Button btnInfo;

        public override void Init()
        {
            base.Init();
            transParent     = this.gameObject.transform.FindChild("list/Viewport/Content");
            goBaseItem      = this.gameObject.transform.FindChild("list/item").gameObject;
            goDesc          = this.gameObject.transform.FindChild("desc").gameObject;
            btnClose        = this.gameObject.transform.FindChild("btn_close").GetComponent<Button>();
            btnClose.onClick.RemoveAllListeners();
            btnClose.onClick.AddListener(
                () =>
                {
                    GameUI.CloseUI<GameUI_Mgr_Skill>();
                }
            );

            btnInfo = this.gameObject.transform.FindChild("btn_info").GetComponent<Button>();
            btnInfo.onClick.RemoveAllListeners();
            btnInfo.onClick.AddListener(
                () =>
                {
                    GameUI.ShowUI<GameUI_Mgr_MessageBox>();
                }
            );
        }

        public override void SetData()
        {
            base.SetData();

            if (listItems.Count > 0)
            {
                for (int i = 0; i < listItems.Count; i++)
                {
                    GameObject go = listItems[i];
                    GameObject.Destroy(go);
                    go = null;
                }
                listItems.Clear();
            }

            List<int> ids = DataCache.Ins.listIds;
            for (int i = 0; i < ids.Count; i++)
            {
                GameObject go = GameObject.Instantiate(goBaseItem);
                go.transform.SetParent(transParent, false);
                go.SetActive(true);
                listItems.Add(go);

                GameUI_Ctrl_SkillItem item = go.GetComponent<GameUI_Ctrl_SkillItem>();
                if (item == null)
                {
                    item = go.AddComponent<GameUI_Ctrl_SkillItem>();
                }
                item.id = ids[i];
                item.Init();

                Button btn = item.GetComponent<Button>();
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    ChooseItem(item.id);
                }
                );
            }

            ChooseItem(ids[0]);
        }

        private void ChooseItem(int _id)
        {
            Debug.Log("---ChooseItem---");
            GameUI_Ctrl_SkillDesc desc = goDesc.GetComponent<GameUI_Ctrl_SkillDesc>();
            if (desc == null)
            {
                desc = goDesc.AddComponent<GameUI_Ctrl_SkillDesc>();
            }
            desc.Init(_id);
        }
    }
}

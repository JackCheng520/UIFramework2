using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// ================================
//* 功能描述：GameUI_Ctrl_SkillDesc  
//* 创 建 者：chenghaixiao
//* 创建日期：2017/2/9 9:48:04
// ================================
namespace Assets.UISystem
{
    public class GameUI_Ctrl_SkillDesc : Components
    {
        private int id;

        private Text txtContent;

        private Button btnUpdate;
        public override void FindChild()
        {
            base.FindChild();
            txtContent = transform.FindChild("content").GetComponent<Text>();
            btnUpdate = transform.FindChild("btn_upgrade").GetComponent<Button>();

            btnUpdate.onClick.RemoveAllListeners();
            btnUpdate.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickUpdate));
        }
        SkillData data;
        public void Init(int _id)
        {
            Check();
            this.id = _id;

            data = DataCache.Ins.GetSkill(this.id);
            txtContent.text = data.name + "\n" + data.desc + "\n" + data.level;

        }

        private void OnClickUpdate()
        {
            Debug.Log("---OnClickUpdate---");

            DataCache.Ins.SetLevel(this.id, data.level+1);

            data = DataCache.Ins.GetSkill(this.id);
            txtContent.text = data.name + "\n" + data.desc + "\n" + data.level;
        }
    }
}

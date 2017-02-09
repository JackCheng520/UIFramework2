using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// ================================
//* 功能描述：GameUI_Ctrl_SkillItem  
//* 创 建 者：chenghaixiao
//* 创建日期：2017/2/9 9:33:38
// ================================
namespace Assets.UISystem
{
    public class GameUI_Ctrl_SkillItem : Components
    {
        public int id;

        private bool beFind = false;

        private Text txtName;


        public override void FindChild()
        {
            txtName = transform.FindChild("title").GetComponent<Text>();

        }

        public void Init()
        {
            Check();
            SkillData data = DataCache.Ins.GetSkill(id);
            txtName.text = data.name;
        }

    }
}

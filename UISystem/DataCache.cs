using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ================================
//* 功能描述：DataCache  
//* 创 建 者：chenghaixiao
//* 创建日期：2017/2/9 9:57:26
// ================================
namespace Assets.UISystem
{
    public class DataCache
    {
        private static DataCache ins;

        public DataCache() { }

        public static DataCache Ins
        {
            get
            {
                if (ins == null)
                {
                    ins = new DataCache();
                    ins.Init();
                }
                return ins;
            }
        }

        private Dictionary<int, SkillData> dic = new Dictionary<int, SkillData>();
        public List<int> listIds = new List<int>();

        private void Init()
        {
            for (int i = 0; i < 10; i++)
            {
                SkillData d = new SkillData();
                d.id = i;
                d.name = "技能名字 -- " + i;
                d.desc = "技能描述 -- " + i;
                d.level = 0;
                dic.Add(d.id, d);
                listIds.Add(i);
            }
        }

        public SkillData GetSkill(int _id)
        {
            if (dic.ContainsKey(_id))
                return dic[_id];
            return null;
        }

        public void SetLevel(int _id, int _level)
        {
            if (dic.ContainsKey(_id))
            {
                dic[_id].level = _level;
            }
        }
    }

    public class SkillData
    {
        public int id;

        public string name;

        public string desc;

        public int level;
    }
}

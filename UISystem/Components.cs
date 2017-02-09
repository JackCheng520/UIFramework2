using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// ================================
//* 功能描述：Components  
//* 创 建 者：chenghaixiao
//* 创建日期：2017/2/9 9:50:58
// ================================
namespace Assets.UISystem
{
    public abstract class Components:MonoBehaviour
    {
        public bool beFind = false;

        public virtual void FindChild() { }

        public virtual void Check() {
            if (!beFind)
            {
                FindChild();
                beFind = true;
            }
        }
    }
}

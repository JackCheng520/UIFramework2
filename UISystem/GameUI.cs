using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// ================================
//* 功能描述：GameUI  
//* 创 建 者：chenghaixiao
//* 创建日期：2017/2/8 13:13:36
// ================================
namespace Assets.UISystem
{
    public enum UIType
    {
        Normal,
        Message,
        None,
    }

    public enum UICollider
    {
        None,
        Normal,
        WithBg,
    }

    public abstract class GameUI
    {
        #region 单个UI属性

        public int id = -1;

        public string name = string.Empty;

        public UIType uiType = UIType.None;

        public bool isStack = false;

        public UICollider uiCollider = UICollider.None;

        public string uiPath = string.Empty;

        public GameObject gameObject;

        protected object data;
        #endregion

        #region 全局属性
        public static Dictionary<string, GameUI> dic = new Dictionary<string, GameUI>();

        public static List<GameUI> stack = new List<GameUI>();
        #endregion

        //-----------------------------------------------------

        #region virtual api
        public virtual void Init() { }

        public virtual void SetData() { }

        public virtual void Disapper() { this.gameObject.SetActive(false); }

        public virtual void Appear() { this.gameObject.SetActive(true); }

        #endregion
        

        #region internal api

        public GameUI(UIType _uiType, bool _isStack, UICollider _uiCollider)
        {
            this.uiType = _uiType;
            this.isStack = _isStack;
            this.uiCollider = _uiCollider;
            this.name = this.GetType().ToString();
        }

        private void Show()
        {
            GameObject go = null;
            if (this.gameObject == null)
            {
                go = GameObject.Instantiate(Resources.Load<GameObject>(this.uiPath));

                if (go == null)
                {
                    Debug.LogError(uiPath + ": --> can not find ");
                    return;
                }

                this.gameObject = go;

                SetUIAnchor(go);

                Init();

            }

            SetData();

            Appear();

            PushUI(this);

            HideOldUIs();
        }

        private void SetUIAnchor(GameObject _go)
        {
            if (this.uiType == UIType.Normal)
            {
                _go.transform.SetParent(CanvasRoot.Ins.normalRoot, false);
            }
            else if (this.uiType == UIType.Message)
            {
                _go.transform.SetParent(CanvasRoot.Ins.messageRoot, false);
            }

        }

        private bool IsActive()
        {
            return this.gameObject.activeSelf;
        }

        #endregion


        #region static api

        private static void PushUI(GameUI _ui)
        {
            if (stack == null)
            {
                stack = new List<GameUI>();
            }
            if (_ui == null)
            {
                Debug.LogError("ui is null");
                return;
            }

            if (!_ui.isStack)
            {
                return;
            }

            bool isFound = false;
            for (int i = 0; i < stack.Count; i++)
            {
                if (stack[i].Equals(_ui))
                {
                    stack.RemoveAt(i);
                    stack.Add(_ui);
                    isFound = true;
                    break;
                }
            }

            if (!isFound)
            {
                stack.Add(_ui);
            }

        }

        private static void HideOldUIs()
        {
            if (stack.Count <= 1)
                return;
            GameUI topUI = stack[stack.Count - 1];
            if (topUI.isStack)
            {
                for (int i = stack.Count - 2; i >= 0; i--)
                {
                    stack[i].Disapper();
                }
            }
        }

        private static void ClearNodes()
        {
            stack.Clear();
        }


        public static void ShowUI<T>() where T : GameUI, new()
        {
            ShowUI<T>(null, null, false);
        }

        public static void ShowUI<T>(Action _callBack, object _data, bool _isAsync) where T : GameUI, new()
        {
            Type t = typeof(T);
            string key = t.ToString();

            if (dic != null && dic.ContainsKey(key))
            {
                ShowUI(key, dic[key], _data, _callBack, _isAsync);
            }
            else
            {
                T ui = new T();
                ShowUI(key, ui, _data, _callBack, _isAsync);
            }
        }

        public static void ShowUI<T>(object _data) where T : GameUI, new()
        {
            ShowUI<T>(null, _data, false);
        }

        public static void ShowUI<T>(Action _callBack) where T : GameUI, new()
        {
            ShowUI<T>(_callBack, null, true);
        }

        public static void ShowUI<T>(Action _callBack, object _data) where T : GameUI, new()
        {
            ShowUI<T>(_callBack, _data, false);
        }

        public static void ShowUI(string _name, GameUI _ui)
        {
            ShowUI(_name, _ui, null, null, false);
        }

        public static void ShowUI(string _name, GameUI _ui, object _data)
        {
            ShowUI(_name, _ui, _data, null, false);
        }

        public static void ShowUI(string _name, GameUI _ui, object _data, Action _callBack)
        {
            ShowUI(_name, _ui, _data, _callBack, true);
        }

        public static void ShowUI(string _name, GameUI _ui, object _data, Action _callback, bool _isAsync)
        {
            if (string.IsNullOrEmpty(_name) || _ui == null)
            {
                Debug.LogError("ui is null");
                return;
            }

            if (dic == null)
            {
                dic = new Dictionary<string, GameUI>();
            }

            GameUI ui = null;
            if (dic.ContainsKey(_name))
            {
                ui = dic[_name];
            }
            else
            {
                dic.Add(_name, _ui);
                ui = _ui;
            }

            ui.data = _data;
            ui.Show();
        }


        //--------------------------------------


        public static void CloseUI(GameUI _ui)
        {
            if (_ui == null)
                return;

            if (_ui.isStack)
            {
                if (stack != null)
                {
                    for (int i = 0; i < stack.Count; i++)
                    {
                        if (stack[i] == _ui)
                        {
                            stack.RemoveAt(i);
                            break;
                        }
                    }

                    if (stack.Count > 0)
                    {
                        GameUI topUI = stack[stack.Count - 1];
                        ShowUI(topUI.name, topUI);
                    }
                }
            }

            _ui.Disapper();
        }

        public static void CloseUI(string _name)
        {
            if (dic.ContainsKey(_name))
            {
                CloseUI(dic[_name]);
            }
        }

        public static void CloseUI<T>() where T : GameUI, new()
        {
            Type t = typeof(T);
            string name = t.ToString();

            CloseUI(name);
        }

        #endregion
    }
}

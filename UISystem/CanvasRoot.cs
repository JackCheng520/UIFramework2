using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ================================
//* 功能描述：UIRoot  
//* 创 建 者：chenghaixiao
//* 创建日期：2017/2/8 13:40:32
// ================================
namespace Assets.UISystem
{
    public class CanvasRoot : MonoBehaviour
    {
        private static CanvasRoot ins;

        public static CanvasRoot Ins
        {
            get
            {
                if (ins == null)
                {
                    Init();
                }
                return ins;
            }
        }

        public GameObject root;

        public Transform normalRoot;

        public Transform messageRoot;

        public Camera camera;


        static void Init()
        {
            Canvas canvas = null;
            GameObject uiRoot = new GameObject("UIRoot");
            uiRoot.layer = LayerMask.NameToLayer("UI");
            RectTransform rt = uiRoot.AddComponent<RectTransform>();
            rt.anchoredPosition = Vector2.zero;
            ins = uiRoot.AddComponent<CanvasRoot>();
            ins.root = uiRoot;

            canvas = ins.root.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.pixelPerfect = true;
            canvas.planeDistance = 0;
            canvas.sortingOrder = 0;

            CanvasScaler cs = uiRoot.AddComponent<CanvasScaler>();
            uiRoot.AddComponent<GraphicRaycaster>();
            cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            cs.referenceResolution = new Vector2(1136, 640);
            cs.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            cs.referencePixelsPerUnit = 100;


            GameObject c = new GameObject("Camera");
            Camera camera = c.AddComponent<Camera>();
            camera.transform.SetParent(uiRoot.transform, false);
            camera.clearFlags = CameraClearFlags.Color;
            camera.cullingMask = 1 << 5;
            camera.orthographic = true;
            camera.orthographicSize = 5;
            camera.nearClipPlane = -500;
            camera.farClipPlane = 500;

            canvas.worldCamera = camera;
            ins.camera = camera;

            GameObject eventSys = GameObject.Find("EventSystem");
            if (eventSys != null)
            {
                Destroy(eventSys);
            }
            eventSys = new GameObject("EventSystem");
            eventSys.transform.SetParent(uiRoot.transform, false);
            eventSys.AddComponent<EventSystem>();
            

            if (Application.isMobilePlatform)
            {
                eventSys.AddComponent<TouchInputModule>();
            }
            else
            {
                eventSys.AddComponent<StandaloneInputModule>();
            }

            ins.normalRoot = CreateChildRoot(uiRoot.transform).transform;
            ins.normalRoot.name = "NormalRoot";

            ins.messageRoot = CreateChildRoot(uiRoot.transform).transform;
            ins.messageRoot.name = "MessageRoot";

        }

        static GameObject CreateChildRoot(Transform _parent)
        {
            Canvas canvas = null;
            GameObject go = new GameObject();
            go.transform.SetParent(_parent, false);
            go.layer = LayerMask.NameToLayer("UI");
            go.AddComponent<GraphicRaycaster>();

            canvas = go.GetComponent<Canvas>();
            canvas.pixelPerfect = true;
            canvas.overrideSorting = false;

            RectTransform rt = go.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.pivot = Vector2.one * 0.5f;
            rt.anchoredPosition = Vector2.zero;
            rt.sizeDelta = Vector2.zero;


            return go;
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManage : MonoBehaviour
{
    UIBase LeftView;
    UIBase RightView;
    const string UIPATH = "Prefabs/";

    public static UIManage Instance;
    public void Start()
    {
        Instance = this;
        UIBase view = CreateView<MainView>();
    }

    public void ShowLeft<T>() where T : UIBase
    {
        if (LeftView == null)
        {
            LeftView = CreateView<T>();
        }
        else
        {
            if(LeftView.GetType().ToString()== typeof(T).Name)
            {
                LeftView.Destory();
                return;
            }
            LeftView.Destory();
            LeftView = CreateView<T>();
        }
    }

    public void ShowRight<T>() where T : UIBase
    {
        if (RightView == null)
        {
            RightView = CreateView<T>();
        }
        else
        {
            if (RightView.GetType().ToString() == typeof(T).Name)
            {
                RightView.Destory();
                return;
            }
            RightView.Destory();
            RightView = CreateView<T>();
        }
    }

    public void DeleteLeft()
    {
        LeftView = null;
    }

    public void DeleteRight()
    {
        RightView = null;
    }

    public static UIBase CreateView<T>() where T : UIBase
    {
        string name = typeof(T).Name;
        UIBase go = Instantiate(Resources.Load(UIPATH + name)) as UIBase;
        go.InitView();
        return go;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManage : MonoBehaviour
{
    public static Transform Root;
    UIBaseView LeftView;
    UIBaseView RightView;

    const string UIPATH = "Prefabs/";

    public static UIManage Instance;
    public void Start()
    {
        Instance = this;

        UIBaseView view = CreateView(new MainController());
    }

    public void ShowLeft(BaseController baseController) 
    {
        if (LeftView == null)
        {
            LeftView = CreateView(baseController);
        }
        else
        {
            if(LeftView.GetType().ToString()== baseController.name)
            {
                LeftView.Destory();
                return;
            }
            LeftView.Destory();
            LeftView = CreateView(baseController);
        }
    }

    public void ShowRight(BaseController baseController) 
    {
        if (RightView == null)
        {
            RightView = CreateView(baseController);
        }
        else
        {
            if (RightView.GetType().ToString() == baseController.name)
            {
                RightView.Destory();
                return;
            }
            RightView.Destory();
            RightView = CreateView(baseController);
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

    public static UIBaseView CreateView(BaseController baseController)
    {
        string name = baseController.name;
        UIBaseView go = Instantiate(Resources.Load(UIPATH +name)) as UIBaseView;
        go.transform.parent = Root;
        go.InitView(baseController);
        return go;
    }
}

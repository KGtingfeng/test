using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManage : MonoBehaviour
{
    UIBaseView LeftView;
    UIBaseView RightView;

    const string UIPATH = "Prefabs/";

    public static UIManage Instance;
    public void Start()
    {
        Instance = this;
        MainController mainController = new MainController();
        UIBaseView view = CreateView(mainController);
    }

    public void ShowLeft(BaseController baseController) 
    {
        if (LeftView == null)
        {
            LeftView = CreateView(baseController);
        }
        else
        {
            if(LeftView.controller .GetPath() == baseController.GetPath())
            {
                LeftView.CloseView();
                return;
            }
            LeftView.CloseView();
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
            if (RightView.controller.GetPath() == baseController.GetPath())
            {
                RightView.CloseView();
                return;
            }
            RightView.CloseView();
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
        string name = baseController.GetPath();
        Debug.LogError(name);
        GameObject go = Instantiate(Resources.Load(UIPATH + name)) as GameObject;
        UIBaseView view = go.GetComponent<UIBaseView>();
        go.transform.parent = Instance.transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        view.InitView(baseController);
        return view;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
public class EscView : UIBaseView
{
    public override void InitView(BaseController controller)
    {
        base.InitView(controller);        
    }

    public void OnClickReturn()
    {
        EditorSceneManager.LoadSceneAsync("start");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}

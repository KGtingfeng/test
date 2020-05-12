using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
public class EndView : UIBaseView
{
    public UILabel sroce;
    public override void InitView(BaseController controller)
    {
        base.InitView(controller);
        sroce.text = (GameManage.Instance.round + GameManage.Instance.score)+"";
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

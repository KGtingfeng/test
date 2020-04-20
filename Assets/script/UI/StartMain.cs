using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class StartMain :MonoBehaviour
{
    public UILabel num;
    public Transform Root;
    AsyncOperation async;
    const string UIPATH = "Prefabs/";
    private void Start()
    {
        num.text = XMLData.GameDatas[0].score+"";
    }

    public void OnClickStart()
    {
        StartCoroutine(BeginLoading());
    }

    IEnumerator BeginLoading()
    {
        async = EditorSceneManager.LoadSceneAsync("test");
        yield return async;
    }

    public void OnClickTalent()
    {
        TalentController controller = new TalentController();
        string name = controller.GetPath();
        GameObject go = Instantiate(Resources.Load(UIPATH + name)) as GameObject;
        UIBaseView view = go.GetComponent<UIBaseView>();
        go.transform.parent = Root;
        go.transform.localScale = Vector3.one;
        view.InitView(controller);
    }


    public void OnClickOut()
    {
        Application.Quit();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class StartMain :MonoBehaviour
{
    public static StartMain Instance;
    public UILabel num;
    public Transform Root;
    AsyncOperation async;
    const string UIPATH = "Prefabs/";
    private void Start()
    {
        Instance = this;
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
        CreateView(new TalentController());
    }


    public void OnClickOut()
    {
        Application.Quit();
    }

    public void UpdateScore()
    {
        num.text = XMLData.GameDatas[0].score + "";
    }

    public UIBaseView CreateView(BaseController baseController)
    {
        string name = baseController.GetPath();
        Debug.LogError(name);
        GameObject go = Instantiate(Resources.Load(UIPATH + name)) as GameObject;
        UIBaseView view = go.GetComponent<UIBaseView>();
        go.transform.parent = Root;
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        view.InitView(baseController);
        return view;
    }

    public void CreateTips(string tip)
    {
        TipsView tipsView = (TipsView)CreateView(new TipsController());
        tipsView.Tips(tip);
    }
}

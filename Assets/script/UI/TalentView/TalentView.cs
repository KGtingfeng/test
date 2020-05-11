using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class TalentView : UIBaseView
{
    public UILabel nameLabel;
    public UILabel des;
    public UILabel num;
    public GameObject button;
    public GameObject grid;
    public GameObject item;
    int id;

    public override void InitView(BaseController controller)
    {
        base.InitView(controller);
        CreateItem(XMLData.TalentConfs);
    }

    public void CreateItem(List<TalentConf> talentConfs)
    {
        foreach(TalentConf conf in talentConfs)
        {
            GameObject go = NGUITools.AddChild(grid, item);
            go.GetComponent<TalentItem>().Init(conf);
            UIEventListener.Get(go).onClick = OnClickTalent;
            UIEventListener.Get(go).parameter = conf.id;
            go.SetActive(true);
        }
        grid.GetComponent<UIGrid>().Reposition();
    }

    public void OnClickTalent(GameObject go)
    {

        id = (int)UIEventListener.Get(go).parameter;
        TalentConf conf = XMLData.TalentConfs.Find(a => a.id == id);
        //Debug.LogError(id);
        nameLabel.text = conf.talentName;
        des.text = conf.introduction;
        num.text = conf.num+"";
        nameLabel.gameObject.SetActive(true);
        des.gameObject.SetActive(true);
        num.gameObject.SetActive(true);
        if (XMLData.GameDatas[0].talents.Find(a => a.id == id) == null)
            button.SetActive(true);
        else
            button.SetActive(false);
    }

    public void OnClickBuy()
    {
        if (XMLData.GameDatas[0].talents.Find(a => a.id == id) != null)
            StartMain.Instance.CreateTips("该天赋已购买！");
        TalentConf conf = XMLData.TalentConfs.Find(a => a.id == id);
        if (conf.num > XMLData.GameDatas[0].score)
        {
            StartMain.Instance.CreateTips("天赋点不足！");
        }
        else
        {
            XMLData.GameDatas[0].score -= conf.num;
            XMLData.GameDatas[0].talent += conf.id+ "；";
            XMLData.SetGameData(XMLData.GameDatas[0].score, XMLData.GameDatas[0].talent);
        }
    }
}
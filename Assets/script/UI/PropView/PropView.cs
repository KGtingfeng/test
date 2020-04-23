using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropView : UIBaseView
{
    public GameObject grid;
    public GameObject item;

    public UILabel propName;
    public UILabel des;
    public UISprite icon;
    public GameObject useItem;
    int id;

    public override void InitView(BaseController controller)
    {
        base.InitView(controller);
        CreateItem(GameManage.Instance.userData.props);
    }

    public void CreateItem(List<Prop> props)
    {
        foreach (Prop p in props)
        {
            GameObject go = NGUITools.AddChild(grid, item);
            go.GetComponent<PropItem>().Init(p);
            UIEventListener.Get(go).onClick = OnClickItem;
            UIEventListener.Get(go).parameter = p.conf.id;
            go.SetActive(true);
        }
        grid.GetComponent<UIGrid>().Reposition();
    }

    public void OnClickItem(GameObject go)
    {

        id = (int)UIEventListener.Get(go).parameter;
        TalentConf conf = XMLData.TalentConfs.Find(a => a.id == id);
        //Debug.LogError(id);
        propName.text = conf.talentName;
        des.text = conf.introduction;
        icon.spriteName = ""+conf.id;
        propName.gameObject.SetActive(true);
        des.gameObject.SetActive(true);
        icon.gameObject.SetActive(true);
        useItem.SetActive(true);

    }

    public void OnClickUse()
    {
        if (XMLData.GameDatas[0].talents.Find(a => a.id == id) != null)
            Debug.LogError("该天赋已购买！");
        TalentConf conf = XMLData.TalentConfs.Find(a => a.id == id);
        if (conf.num > XMLData.GameDatas[0].score)
        {
            Debug.LogError("天赋点不足！");
        }
        else
        {
            XMLData.GameDatas[0].score -= conf.num;
            XMLData.GameDatas[0].talent += conf.id + "；";
            XMLData.SetGameData(XMLData.GameDatas[0].score, XMLData.GameDatas[0].talent);
        }
    }
}

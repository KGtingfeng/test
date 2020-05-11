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
        CreateItem();
    }

    public void CreateItem()
    {
        List<Prop> props = GameManage.Instance.userData.props;
        GameTools.DeleteAllChild(grid.transform);
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
        PropsConf conf = XMLData.PropsConfs.Find(a => a.id == id);
        //Debug.LogError(id);
        propName.text = conf.propsName;
        des.text = conf.introduction;
        icon.spriteName = ""+conf.id;
        propName.gameObject.SetActive(true);
        des.gameObject.SetActive(true);
        icon.gameObject.SetActive(true);
        useItem.SetActive(true);

    }

    public void OnClickUse()
    {
        PropsConf conf = XMLData.PropsConfs.Find(a => a.id == id);
        GameTools.UseProp(conf);
        Prop prop = GameManage.Instance.userData.props.Find(a => a.conf.id == conf.id);
        if (prop.num == 1)
        {
            GameManage.Instance.userData.props.Remove(prop);
            propName.gameObject.SetActive(false);
            des.gameObject.SetActive(false);
            icon.gameObject.SetActive(false);
            useItem.SetActive(false);
        }
        else
        {
            prop.num--;
        }
        CreateItem();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRoleView : UIBaseView
{
    public GameObject grid;
    public GameObject item;
    int id;
    public override void InitView(BaseController controller)
    {
        base.InitView(controller);
        List<TalentConf> talents = XMLData.GameDatas[0].talents.FindAll(a=>a.type==TalentType.Role);
        CreateItem(talents);
    }

    public void CreateItem(List<TalentConf> talents)
    {
        CharacterConf chara = XMLData.CharacterConfs.Find(a => a.id == 2001);
        GameObject g = NGUITools.AddChild(grid, item);
        g.GetComponent<RoleItem>().Init(chara);
        UIEventListener.Get(g).onClick = OnClickTalent;
        UIEventListener.Get(g).parameter = chara.id;
        g.SetActive(true);
        foreach (TalentConf conf in talents)
        {
            CharacterConf character = XMLData.CharacterConfs.Find(a => a.id == int.Parse(conf.buff));
            GameObject go = NGUITools.AddChild(grid, item);
            go.GetComponent<RoleItem>().Init(character);
            UIEventListener.Get(go).onClick = OnClickTalent;
            UIEventListener.Get(go).parameter = character.id;
            go.SetActive(true);
        }
        grid.GetComponent<UIGrid>().Reposition();
    }

    public void OnClickTalent(GameObject go)
    {
        id = (int)UIEventListener.Get(go).parameter;       
    }

    public void OnClickSelect()
    {
        GameManage.Instance.CreateRole(id);
        CloseView();
    }


}

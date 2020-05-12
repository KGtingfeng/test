using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRoleView : UIBaseView
{
    public GameObject grid;
    public GameObject item;
    public UILabel roleName;
    public GameObject button;
    int id;
    public override void InitView(BaseController controller)
    {
        base.InitView(controller);
        List<TalentConf> talents = XMLData.GameDatas[0].talents.FindAll(a=>a.type==TalentType.Role);
        CreateItem(talents);
    }

    public void CreateItem(List<TalentConf> talents)
    {
        //Debug.LogError(talents.Count);
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
        roleName.text= XMLData.CharacterConfs.Find(a => a.id == id).name;
        roleName.gameObject.SetActive(true);
        button.SetActive(true);
    }

    public void OnClickSelect()
    {
        GameManage.Instance.CreateRole(id);
        CloseView();
    }


}

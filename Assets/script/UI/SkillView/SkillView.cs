using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillView : UIBaseView
{
    public GameObject grid;
    public GameObject item;


    public override void InitView(BaseController controller)
    {
        base.InitView(controller);
        CreateItem(GameManage.Instance.userData.skills);
    }

    public void CreateItem(List<Skill> talents)
    {
        
        foreach (Skill conf in talents)
        {
            GameObject go = NGUITools.AddChild(grid, item);
            go.GetComponent<SkillItem>().Init(conf);
            go.SetActive(true);
        }
        grid.GetComponent<UIGrid>().Reposition();
    }


}

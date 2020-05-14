using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillView : UIBaseView
{
    public GameObject grid;
    public UIScrollView scrollView;
    public GameObject item;
    public static SkillView Instance;

    public override void InitView(BaseController controller)
    {
        base.InitView(controller);
        Instance = this;
        CreateItem();
    }

    public void CreateItem()
    {
        List<Skill> talents = GameManage.Instance.userData.skills;
        GameTools.DeleteAllChild(grid.transform);

        foreach (Skill conf in talents)
        {
            GameObject go = NGUITools.AddChild(grid, item);
            go.GetComponent<SkillItem>().Init(conf);
            go.SetActive(true);
        }
        
        grid.GetComponent<UIGrid>().Reposition();
    }


}

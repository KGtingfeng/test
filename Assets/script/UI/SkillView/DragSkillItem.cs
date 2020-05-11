using UnityEngine;
using UnityEditor;

public class DragSkillItem : UIDragDropItem
{
    public UISprite icon;
    public GameObject root;

    Skill skill;
    public void Init(Skill skill)
    {
        this.skill = skill;
        icon.spriteName = skill.id+"";
    }

    protected override void OnDragDropRelease(GameObject surface)
    {
        if (surface.tag == "Skill")
        {
            surface.GetComponent<MainSkillItem>().Change(skill);
        }
        base.OnDragDropRelease(surface);
    }

    protected override void OnDragStart()
    {               
        base.OnDragStart();
        clone.transform.parent = root.transform;
        clone.GetComponent<DragSkillItem>().skill = skill;
    }
}
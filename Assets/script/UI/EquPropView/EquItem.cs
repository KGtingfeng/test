using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


public class EquItem : UIDragDropItem
{
    public GameObject root;
    public GameObject grid;
    public UILabel nameLabel;
    public UISprite sprite;
    public UILabel level;
    Dictionary<int, string> color = new Dictionary<int, string>() {
        {0,"" },
        {1,"[00ff00]" },
        {2,"[0000ff]" },
        {3,"[ff0000]" },

    };
    Equipment equipment;

    public void Init(Equipment p)
    {
        equipment = p;
        nameLabel.text =color[p.color]+ p.Conf.equName;
        sprite.spriteName = p.Conf.id + "";
        level.text = "Lv." + p.level;
    }

    protected override void OnDragDropRelease(GameObject surface)
    {
        if (surface.tag == "Equ")
        {
            if (surface.GetComponent<EquipmentItem>().ChangeEqu(equipment))
            {
                transform.parent = grid.transform;
                NGUITools.MarkParentAsChanged(gameObject);
                mCollider.enabled = true;
                EquPropView.Instance.CreateItem();
                return;
            }
        }
        transform.parent = grid.transform;
        NGUITools.MarkParentAsChanged(gameObject);
        mCollider.enabled = true;
        EquPropView.Instance.grid.GetComponent<UIGrid>().Reposition();
    }

    protected override void OnDragDropStart()
    {
        transform.parent = root.transform;
        base.OnDragDropStart();
    }

}
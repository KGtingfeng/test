using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


public class EquItem : MonoBehaviour
{
    public UILabel nameLabel;
    public UISprite sprite;
    public UILabel level;
    Dictionary<int, string> color = new Dictionary<int, string>() {
        {0,"" },
        {1,"[00ff00]" },
        {2,"[0000ff]" },
        {3,"[ff0000]" },

    };
 

    public void Init(Equipment p)
    {
        nameLabel.text =color[p.color]+ p.Conf.equName;
        sprite.spriteName = p.Conf.id + "";
        level.text = "Lv." + p.level;
    }
}
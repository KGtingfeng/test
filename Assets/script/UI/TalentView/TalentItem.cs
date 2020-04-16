using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentItem :MonoBehaviour
{
    public UILabel nameLabel;
    public UISprite sprite;

    public void Init(TalentConf conf)
    {
        nameLabel.text = conf.talentName;
        sprite.spriteName = conf.id+"";
    }
}

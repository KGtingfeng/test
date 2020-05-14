using UnityEngine;
using UnityEditor;

public class SkillItem : MonoBehaviour
{
    public UILabel nameLabel;
    public DragSkillItem item;
    public UILabel level;
    public UILabel des;
    Skill skill;

    public void Init(Skill skill)
    {
        this.skill = skill;
        SkillConf conf = XMLData.SkillConfigs.Find(a => a.id == skill.id);
        SkillLevelConf levelConf= XMLData.SkillLevelConfs.Find(a => a.id == skill.id&&a.level==skill.level);
        nameLabel.text = conf.skillName;
        item.Init(skill);
        level.text = "LV." + skill.level;
        string text = string.Format(conf.des,Mathf.Abs(levelConf.original).ToString(), levelConf.multiple.ToString());
        des.text = text;
    }

    public void OnClickUpLevel()
    {
        if (skill.level == 5)
        {
            UIManage.CreateTips("该技能已升到满级！");
        }
        if( GameManage.Instance.userData.props.Find(a=>a.conf.type== PropType.SKILL&& a.conf.buff == skill.id.ToString()) != null)
        {
            GameManage.Instance.userData.skills.Find(a => a.id == skill.id).level++;
            Prop prop = GameManage.Instance.userData.props.Find(a => a.conf.type == PropType.SKILL && a.conf.buff == skill.id.ToString());
            if (prop.num == 1)
            {
                GameManage.Instance.userData.props.Remove(prop);
            }
            else
            {
                prop.num--;
            }
            UIManage.CreateTips("技能升级成功！");
            SkillView.Instance.CreateItem();
        }
        else
        {
            UIManage.CreateTips("技能书不足！");
        }
    }

   
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillColl : MonoBehaviour
{
    public Character character;
    public SkillConf skill;
    public SkillLevelConf skillLevelConf;
    public int skillNum;
    private void OnParticleCollision(GameObject other)
    {
        if (character.IsPlayer)
        {
            if (other.tag == "NPC"&&other.GetComponent<Character>().skillNum!=skillNum)
            {
                float damage = GameTools.CalculateDamage(character, skillLevelConf,skill);
                GameTools.Damage(other.GetComponent<Character>(), (int)damage,skill.skillEffectType);
                character.skillNum = skillNum;
            }
        }
        else
        {
            if (other.tag == "NPC" && other.GetComponent<Character>().skillNum != skillNum)
            {
                float damage = GameTools.CalculateDamage(character, skillLevelConf,skill);
                GameTools.Damage(other.GetComponent<Character>(), (int)damage, skill.skillEffectType);
                character.skillNum = skillNum;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTools : MonoBehaviour
{
    public static float CalculateDamage(Character character,SkillConf skill)
    {
        int skillAddition = 0;
        switch (skill.skillAdditionType)
        {
            case SkillAdditionType.energy:
                skillAddition = character.energy;
                break;
            case SkillAdditionType.strength:
                skillAddition = character.strength;
                break;
            case SkillAdditionType.speed:
                skillAddition = character.speed;
                break;
            case SkillAdditionType.level:
                skillAddition = character.level;
                break;
        }
        return skill.original + skill.multiple * skillAddition;
    }

    public static float CalculateDamage(Character character, Buff skill)
    {
        int skillAddition = 0;
        switch (skill.skillAdditionType)
        {
            case SkillAdditionType.energy:
                skillAddition = character.energy;
                break;
            case SkillAdditionType.strength:
                skillAddition = character.strength;
                break;
            case SkillAdditionType.speed:
                skillAddition = character.speed;
                break;
            case SkillAdditionType.level:
                skillAddition = character.level;
                break;
        }
        return skill.original + skill.multiple * skillAddition;
    }

    public static void Damage(Character character,int damage,SkillEffectType skillEffect)
    {
        switch (skillEffect)
        {
            case SkillEffectType.blood:
                if (character.blood - damage < 0)
                    character.blood = 0;
                else
                    character.blood -= damage;
                break;
            case SkillEffectType.gas:
                if (character.gas - damage < 0)
                    character.gas = 0;
                else
                    character.gas -= damage;
                break;
            case SkillEffectType.moves:
                if (character.moves - damage < 0)
                    character.moves = 0;
                else
                    character.moves -= damage;
                break;

                break;
        }
    }
}

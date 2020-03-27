using UnityEngine;
using System.Collections;

public class NPC : Character
{
    public int id;
    int skill;
    public void Create(int level, CharacterConf character)
    {
        this.id = character.id;
        this.level = level;
        speed = character.speed+((level-1)* character.levelSpeed);
        strength = character.strength + ((level - 1) * character.levelStrength);
        energy = character.energy + ((level - 1) * character.levelEnergy);
        blood = character.blood + ((level - 1) * character.levelBlood)+(strength*20);
        totalBlood = blood;
        moves = speed/5;
        roundGas = energy / 5;
        gas = roundGas;
        totalGas = roundGas * 2;
        this.skill = character.skill;
    }

    public void Move()
    {
        
    }

    public void StartRound()
    {
        
    }

}

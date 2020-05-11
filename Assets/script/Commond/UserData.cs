using System.Collections.Generic;

public class UserData 
{
    public Equipment helmet;
    public Equipment armor;
    public Equipment shoes;
    public Dictionary<int, Skill> skill = new Dictionary<int, Skill>() {
        {0,new Skill(0,0) },
        {1,new Skill(0,0) },
        {2,new Skill(0,0) },
        {3,new Skill(0,0) },
        {4,new Skill(0,0) },
        {5,new Skill(0,0) },
    };

    public List<Skill> skills=new List<Skill>();
    public List<Prop> props=new List<Prop>();
    public List<Equipment> equipments = new List<Equipment>();
}
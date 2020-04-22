using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManage : MonoBehaviour
{
    public static GameManage Instance;
    static string ROLEPATH = "GameObject/Role/";
    static string TERRAINPATH = "GameObject/Terrain/";
    public static int row = 40;
    public static int col = 40;

    public Transform NPCManage;

    List<CharacterConf> npcConf;
    public UserData userData=new UserData();
    public GameObject ground;
    public List<List<mapPoint>> mapPoints=new List<List<mapPoint>>();
    public List<List<Ground>> groundList=new List<List<Ground>>();

    public List<NPC> npcList;     



    public GameObject roleGameObject;
    public Role role;

    public bool IsMyRound;
    public bool IsSkill;
    public bool IsWalk;
    public bool IsChangeEEquipment;

    public int round = 1;
    public int score = 0;

    private void Start()
    {
        List<GameData> talentConfs= XMLData.GameDatas;
        Instance = this;
        CreateGround();

        //UIManage.CreateView(new SelectRoleController());
        CreateRole(2001);
        IsMyRound = true;
    }

    private Ray ray;
    private RaycastHit hit;
    LayerMask whatIsGround = ~(1 << 0);
    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && !IsWalk && IsMyRound)
        {
            
        }
        if (Input.GetMouseButtonUp(1)&&IsMyRound&&!IsWalk )
        {
            if (IsSkill)
            {
                role.DelectSkillArea();
            }
            else
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "ground")
                    {
                        role.Goto(hit);
                    }                      
                }
            }          
        }
        if (Input.GetKeyUp(KeyCode.Alpha1) && IsMyRound && !IsWalk )
        {
            if (!IsSkill)
            {
                userData.skill_1 = new Skill(1001, 1);
                if (userData.skill_1!=null)
                    role.CreateSkillArea(userData.skill_1);
            }
            else
            {
                role.DelectSkillArea();
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha2) && IsMyRound && !IsWalk)
        {
            if (!IsSkill)
            {
                if (userData.skill_2 != null)
                    role.CreateSkillArea(userData.skill_2);
            }
            else
            {
                role.DelectSkillArea();
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha3) && IsMyRound && !IsWalk)
        {
            if (!IsSkill)
            {
                if (userData.skill_3 != null)
                    role.CreateSkillArea(userData.skill_3);
            }
            else
            {
                role.DelectSkillArea();
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha4) && IsMyRound && !IsWalk)
        {
            if (!IsSkill)
            {
                if (userData.skill_4 != null)
                    role.CreateSkillArea(userData.skill_4);
            }
            else
            {
                role.DelectSkillArea();
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha5) && IsMyRound && !IsWalk)
        {
            //Debug.LogError("skill5");
            if (!IsSkill)
            {
                if (userData.skill_5 != null)
                    role.CreateSkillArea(userData.skill_5);
            }
            else
            {
                role.DelectSkillArea();
            }
        }
        if (Input.GetKeyUp(KeyCode.Q)&&IsMyRound && !IsWalk)
        {
            IsMyRound = false;
            Debug.LogError("下一回合");
            foreach(var npc in npcList)
            {
                npc.StartRound();
            }
            if (npcList.Count < 50)
            {
                Vector2 pos = GameTools.GetPoint(role.GetPosition());
                //Debug.LogError("mapPointvaule" + mapPoints[(int)pos.x][(int)pos.y].vaule);
                GameTools.CreateNPC(npcConf, (int)pos.x, (int)pos.y, groundList[(int)pos.x][(int)pos.y].transform, role.level);
                mapPoints[(int)pos.x][(int)pos.y].vaule = 1;

            }
            round++;
            role.StartRound();
            IsMyRound = true;
        }
    }

    int[,] map;
    void CreateGround()
    {
        List<CharacterConf> smallChar = XMLData.CharacterConfs.FindAll(a => a.id > 2200 && a.id <=2400);
        List<CharacterConf> bossChar = XMLData.CharacterConfs.FindAll(a => a.id > 2500 && a.id <= 3000);
        npcConf= XMLData.CharacterConfs.FindAll(a => a.id > 2400 && a.id <= 2500);
        CreateMap();
        for (int i = 0; i < row; i++)
        {
            List<mapPoint> intRow = new List<mapPoint>();
            List<Ground> pointRow = new List<Ground>();           
            for (int j = 0; j < col; j++)
            {
                GameObject go = Instantiate(ground);
                go.GetComponent<Ground>().Init(i, j);
                go.name = i + "," + j;
                go.transform.parent = this.transform;
                pointRow.Add(go.GetComponent<Ground>());
                int r = map[i, j];
                mapPoint intCol;
                switch (r)
                {
                    case 1:
                        GameObject terrainGo = Instantiate(Resources.Load(TERRAINPATH + "3001")) as GameObject;
                        intCol = new mapPoint(i, j, 1);
                        terrainGo.transform.parent = go.transform;
                        terrainGo.transform.localPosition = Vector3.zero;
                        break;
                    case 2:
                        GameTools.CreateNPC(smallChar, i, j, go.transform,10);
                        intCol = new mapPoint(i, j, 1);
                        break;
                    case 3:
                        GameTools.CreateNPC(bossChar, i, j, go.transform,20);
                        intCol = new mapPoint(i, j, 1);
                        break;
                    default:
                        intCol = new mapPoint(i, j, 0);
                        break;
                }
                intRow.Add(intCol);
            }
            mapPoints.Add(intRow);
            groundList.Add(pointRow);
        }
    }

    public void CreateRole(int id)
    {
        roleGameObject = Resources.Load(ROLEPATH + id) as GameObject;
        roleGameObject = Instantiate(roleGameObject);
        roleGameObject.transform.localPosition = groundList[0][0].transform.position;
        role = roleGameObject.GetComponent<Role>();
        role.Create(XMLData.CharacterConfs.Find(a=>a.id== id));
        role.SetPosition(0, 0);
        roleGameObject.SetActive(true);
        mapPoints[0][0].vaule = 1;
        groundList[0][0].character = role;
    }

    private List<MapConf> GetMap()
    {
        List<MapConf> bigMap = XMLData.MapConfs.FindAll(a => a.mapType == 0);
        List<MapConf> midMap = XMLData.MapConfs.FindAll(a => a.mapType == 1);
        List<MapConf> smaMap = XMLData.MapConfs.FindAll(a => a.mapType == 2);
        for(int i=0; i < 8; i++)
        {
            System.Random random = new System.Random(i  * DateTime.Now.Millisecond);
            int r = random.Next(midMap.Count - 1);
            bigMap.Add(midMap[r]);
        }
        for(int i = 0; i < 13; i++)
        {
            System.Random random = new System.Random(i * DateTime.Now.Millisecond);
            int r = random.Next(smaMap.Count - 1);
            bigMap.Add(smaMap[r]);
        }
        return bigMap;
    }

    private void CreateMap()
    {
        List<MapConf> mstr= GetMap();
        map=new int[row, col];
        for(int i = 0; i < row/10; i++)
        {
            for(int j = 0; j < col/10; j++)
            {
                if (i == 0 && j == 0)
                    continue;
                System.Random random = new System.Random(i * j * DateTime.Now.Millisecond);
                int r = random.Next(mstr.Count-1);
                MapConf mapConf = mstr[r];
                mstr.Remove(mapConf);
                
                string[] mapstr = mapConf.map.Split(';');
                List<string[]> m = new List<string[]>();
                for(int strl = 0; strl < mapstr.Length; strl++)
                {
                    string[] mastr = mapstr[strl].Split(',');
                    m.Add(mastr);
                }
                for(int a=0;a< 10; a++)
                {
                    for(int b=0;b< 10; b++)
                    {
                        map[i * 10  + a, j * 10  + b] = int.Parse(m[a][b]);
                    }
                }                
            }
        }
    }
}


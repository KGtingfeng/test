using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManage : MonoBehaviour
{
    public static GameManage Instance;
    static string ROLEPATH = "GameObject/Role/";
    static string TERRAINPATH = "GameObject/Terrain/";
    public GameObject ground;
    public List<List<mapPoint>> mapPoints;
    public List<List<Ground>> groundList;

    public  static int row = 50;
    public  static int col = 50;

    List<NPC> characterList; 
    
    public GameObject roleGameObject;
    public Role role;

    public bool IsMyRound;
    public bool IsSkill;
    public bool IsWalk;
    public bool IsChangeEEquipment;
    List<mapPoint> walkables;

    public int skillNum=0;

    private void Start()
    {
        Instance = this;
        CreateGround();
        CreateRole();
        IsMyRound = true;
    }

    private Ray ray;
    private RaycastHit hit;
    LayerMask whatIsGround = ~(1 << 0);
    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && !IsWalk && IsMyRound)
        {

            if (IsSkill)
            {

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, 200, whatIsGround))
                {
                    Vector3 target = hitInfo.point;
                    target.y = transform.position.y;
                    role.CreateSkill(target);
                }
            }
        }
        if (Input.GetMouseButtonUp(1)&&IsMyRound&&!IsWalk && IsMyRound)
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
        if (Input.GetKeyUp(KeyCode.Alpha1) && IsMyRound && !IsWalk && IsMyRound)
        {
            if (!IsSkill)
            {
                role.CreateSkillArea();
                IsSkill = true;
            }
            else
            {
                role.DelectSkillArea();
                IsSkill = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.KeypadEnter)&&IsMyRound)
        {
            IsMyRound = false;
            foreach(var npc in characterList)
            {
                npc.StartRound();
            }
            IsMyRound = true;
        }
    }

    int[,] map;
    void CreateGround()
    {
        CreateMap();
        mapPoints = new List<List<mapPoint>>();
        groundList = new List<List<Ground>>();
        for (int i = 0; i < row; i++)
        {
            List<mapPoint> intRow = new List<mapPoint>();
            List<Ground> pointRow = new List<Ground>();
            List<CharacterConf> smallChar = XMLData.CharacterConfs.FindAll(a => a.id > 2200 && a.id <=2500);
            List<CharacterConf> bossChar = XMLData.CharacterConfs.FindAll(a => a.id > 2500 && a.id <= 3000);
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
                        System.Random random = new System.Random(i + j + DateTime.Now.Millisecond);
                        int npcr = random.Next(smallChar.Count - 1);
                        GameObject npcGo = Instantiate(Resources.Load(ROLEPATH + smallChar[npcr].id.ToString())) as GameObject;
                        intCol = new mapPoint(i, j, 1);
                        npcGo.GetComponent<NPC>().Create(10, smallChar[npcr]);
                        npcGo.GetComponent<NPC>().SetPosition(i, j);
                        npcGo.transform.parent = go.transform;
                        npcGo.transform.localPosition = Vector3.zero;
                        break;
                    case 3:
                        System.Random bossrandom = new System.Random(i + j + DateTime.Now.Millisecond);
                        int bossr = bossrandom.Next(bossChar.Count - 1);
                        GameObject boosGo = Instantiate(Resources.Load(ROLEPATH + bossChar[bossr].id.ToString())) as GameObject;
                        intCol = new mapPoint(i, j, 1);
                        boosGo.GetComponent<NPC>().Create(10, smallChar[bossr]);
                        boosGo.GetComponent<NPC>().SetPosition(i, j);
                        boosGo.transform.parent = go.transform;
                        boosGo.transform.localPosition = Vector3.zero;
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

    private void CreateRole()
    {
        roleGameObject = Resources.Load(ROLEPATH + "knight") as GameObject;
        roleGameObject = Instantiate(roleGameObject);
        roleGameObject.transform.localPosition = groundList[0][0].transform.position;
        role = roleGameObject.GetComponent<Role>();
        role.SetPosition(0, 0);
        roleGameObject.SetActive(true);
    }

    private List<MapConf> GetMap()
    {
        List<MapConf> bigMap = XMLData.MapConfs.FindAll(a => a.mapType == 0);
        List<MapConf> midMap = XMLData.MapConfs.FindAll(a => a.mapType == 1);
        List<MapConf> smaMap = XMLData.MapConfs.FindAll(a => a.mapType == 2);
        for(int i=0; i < 8; i++)
        {
            System.Random random = new System.Random(i  + DateTime.Now.Millisecond);
            int r = random.Next(midMap.Count - 1);
            bigMap.Add(midMap[r]);
        }
        for(int i = 0; i < 13; i++)
        {
            System.Random random = new System.Random(i + DateTime.Now.Millisecond);
            int r = random.Next(smaMap.Count - 1);
            bigMap.Add(smaMap[r]);
        }
        return bigMap;
    }

    private void CreateMap()
    {
        List<MapConf> mstr= GetMap();
        map=new int[row, col];
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                if (i == 0 && j == 0)
                    continue;
                System.Random random = new System.Random(i + j + DateTime.Now.Millisecond);
                int r = random.Next(mstr.Count-1);
                MapConf mapConf = mstr[0];
                mstr.Remove(mapConf);
                string[] ystring =mapConf.y.Split(',');
                int y;
                if(ystring.Length>1)
                {
                    System.Random srandom = new System.Random(ystring.Length + DateTime.Now.Millisecond);
                    int sr = srandom.Next(ystring.Length - 1);
                    y = int.Parse(ystring[sr]);
                }
                else
                {
                    y = int.Parse(mapConf.y);
                }
                string[] mapstr = mapConf.map.Split(';');
                List<string[]> m = new List<string[]>();
                for(int strl = 0; strl < mapstr.Length; strl++)
                {
                    string[] mastr = mapstr[strl].Split(',');
                    m.Add(mastr);
                }
                for(int a=0;a< mapConf.size; a++)
                {
                    for(int b=0;b< mapConf.size; b++)
                    {
                        map[i * 10 + mapConf.x + a, j * 10 + y + b] = int.Parse(m[a][b]);
                    }
                }                
            }
        }
    }
}


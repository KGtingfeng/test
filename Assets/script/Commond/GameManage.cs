using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManage : MonoBehaviour
{
    public static GameManage Instance;
    static string ROLEPATH = "GameObject/Role/";
    public GameObject ground;
    public List<List<mapPoint>> mapPoints;
    public List<List<Ground>> groundList;

    public  static int row = 10;
    public  static int col = 10;

    List<Character> characterList;    
    public GameObject roleGameObject;
    public Role role;
    public bool IsMyRound;
    public bool IsSkill;
    public bool IsWalk;
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
        if (Input.GetMouseButtonUp(0) && !IsWalk)
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
        if (Input.GetMouseButtonUp(1)&&IsMyRound&&!IsWalk)
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
        if (Input.GetKeyUp(KeyCode.Alpha1) && IsMyRound && !IsWalk)
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
    }

    void DoNpcRound()
    {

    }

    void CreateGround()
    {
        mapPoints = new List<List<mapPoint>>();
        groundList = new List<List<Ground>>();
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

                System.Random random = new System.Random(i+j+ DateTime.Now.Millisecond);
                int r = random.Next(10);
                if (r < 8)
                {
                    r = 0;
                }
                else
                {
                    r = 1;
                    go.GetComponent<Ground>().ChangeMaterial0();
                }
                mapPoint intCol = new mapPoint(i,j,r);
                intRow.Add(intCol);
            }
            mapPoints.Add(intRow);
            groundList.Add(pointRow);
        }
    }

    void CreateRole()
    {
        roleGameObject = Resources.Load(ROLEPATH + "knight") as GameObject;
        roleGameObject = Instantiate(roleGameObject);
        roleGameObject.transform.localPosition = groundList[0][0].transform.position;
        role = roleGameObject.GetComponent<Role>();
        role.SetPosition(0, 0);
        roleGameObject.SetActive(true);
    }


}


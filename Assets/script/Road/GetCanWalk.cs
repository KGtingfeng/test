using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetCanWalk : MonoBehaviour
{

    static List<mapPoint> open = new List<mapPoint>();
    static List<mapPoint> close = new List<mapPoint>();

    /// <summary>
    /// 获得可走位置
    /// </summary>
    /// <param name="start"></param>
    /// <param name="walkable"></param>
    /// <returns></returns>
    public static  List<mapPoint> getWalk(Ground start, int walkable)
    {
        open.Clear();
        close.Clear();
        List<List<mapPoint>> mapp = GameManage.Instance.mapPoints;
        mapPoint startPoint = new mapPoint(start.x,start.y,1);
        open.Add(startPoint);

        for (int i = 0; i < walkable; i++)
        {
            foreach (mapPoint now in open)
            {
                if (now.y + 1 < GameManage.col)
                    addOpen(mapp[now.x][now.y + 1], open);
                if (now.y - 1 >= 0)
                    addOpen(mapp[now.x][now.y - 1], open);
                if (now.x + 1 < GameManage.row)
                    addOpen(mapp[now.x + 1][now.y], open);
                if (now.x - 1 >= 0)
                    addOpen(mapp[now.x - 1][now.y], open);
                if (now.x % 2 != 0)
                {
                    if (now.y + 1 < GameManage.col && now.x + 1 < GameManage.row)
                        addOpen(mapp[now.x + 1][now.y + 1], open);
                    if (now.y + 1 < GameManage.col && now.x - 1 >= 0)
                        addOpen(mapp[now.x - 1][now.y + 1], open);
                }
                else
                {
                    if (now.y - 1 >= 0 && now.x + 1 < GameManage.row)
                        addOpen(mapp[now.x + 1][now.y - 1], open);
                    if (now.y - 1 >= 0 && now.x - 1 >= 0)
                        addOpen(mapp[now.x - 1][now.y - 1], open);
                }
                close.Add(now);
                open.Remove(now);
            }
            
        }
        open.AddRange(close);
        return open;
    }

   static  void addOpen(mapPoint p, List<mapPoint> pointList)
    {
        if (open.Find(a => a.x == p.x && a.y == p.y) == null && p.vaule != 0)
        {
            pointList.Add(p);
        }

    }

   
}

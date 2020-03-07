using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetCanWalk : MonoBehaviour
{ 

    /// <summary>
    /// 获得可走位置
    /// </summary>
    /// <param name="start"></param>
    /// <param name="walkable"></param>
    /// <returns></returns>
   public static  List<Point> getWalk(Ground start, int walkable)
    {
        List<Point> open =new List<Point>();
        List<Point> close = new List<Point>();
        List<List<Point>> mapp = GetMapPoint();

        Point startPoint = new Point(start.x,start.y,1);
        open.Add(startPoint);

        for (int i = 0; i < walkable; i++)
        {
            foreach (Point now in open)
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

   static  void addOpen(Point p, List<Point> pointList)
    {
        if (!p.isFind && p.vaule == 0)
        {
            p.isFind = true;
            pointList.Add(p);
        }

    }

    static List<List<Point>> GetMapPoint()
    {
        List<List<Point>> maps = new List<List<Point>>();
        for (int i = 0; i < GameManage.Instance.intMap.Count; i++)
        {
            List<Point> points = new List<Point>();
            for (int j = 0; j < GameManage.Instance.intMap[i].Count; j++)
            {
                Point point = new Point(i, j, GameManage.Instance.intMap[i][j]);
            }
        }
        return maps;

    }
}

public  class Point
{
    public int x;
    public int y;
    public int vaule;
    public bool isFind = false;

    public Point(int x, int y, int vaule)
    {
        this.x = x;
        this.y = y;
        this.vaule = vaule;
    }
}
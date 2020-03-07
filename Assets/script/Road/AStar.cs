using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour {

    static  int finishx;
    static  int finishy;
    public static List<mapPoint> find(Ground start, Ground finish)
    {
        List<mapPoint> open = new List<mapPoint>();
        List<mapPoint> close = new List<mapPoint>();
        int startx = start.x;
        int starty = start.y;
        finishx = finish.x;
        finishy = finish.y;
        bool haveRoad = false;

        List<List<mapPoint>> mapp = GetMapPoint();

        mapp[startx][starty].f = 0;
        mapp[startx][starty].parent = null;
        mapp[startx][starty].isFind = true;
        mapPoint now = new mapPoint(mapp[startx][starty]);

        close.Add(now);
        while (true)
        {
            if (now.y + 1 < GameManage.col)
                addOpen(mapp[now.x][now.y + 1],  now,open);
            if (now.y - 1 >= 0)
                addOpen(mapp[now.x][now.y - 1],  now,open);
            if (now.x + 1 < GameManage.row)
                addOpen(mapp[now.x + 1][now.y],  now,open);
            if (now.x - 1 >= 0)
                addOpen(mapp[now.x - 1][now.y],  now,open);
            if (now.x % 2 != 0)
            {
                if (now.y + 1 < GameManage.col && now.x + 1 < GameManage.row)
                    addOpen(mapp[now.x + 1][now.y + 1],  now,open);
                if (now.y + 1 < GameManage.col && now.x - 1 >= 0)
                    addOpen(mapp[now.x - 1][now.y + 1],  now,open);
            }
            else
            {
                if (now.y - 1 >= 0 && now.x + 1 < GameManage.row)
                    addOpen(mapp[now.x + 1][now.y - 1],  now,open);
                if (now.y - 1 >= 0 && now.x - 1 >= 0)
                    addOpen(mapp[now.x - 1][now.y - 1],  now,open);
            }
            SortRobMessage(open);
            now =open[0];
            mapp[now.x][now.y].isColse = true;
            close.Add(open[0]);
            open.Remove(open[0]);
            if(now.x==finishx&&now.y==finishy)
            {
                haveRoad = true;
                break;
            }
            if (open.Count == 0)
                break;

        }
        List<mapPoint> road=new List<mapPoint>();
       
        if (haveRoad)
        {
            while (now.parent != null)
            {
                GameManage.Instance.groundList[now.x][now.y].ChangeMaterial();
               // Debug.LogError("       x       "+now.x+"       y      "+now.y);
                road.Add(now);
                now = now.parent;               
            }
            return road;
        }
        return new List<mapPoint>();
    }

   static void addOpen(mapPoint point, mapPoint start,List<mapPoint> open)
    {
        if (point.isColse)
            return;
        int f, g, h;
        g = start.g + 1;
        h = GetH( point.x,point.y, finishx, finishy);
        f = g + h;
        //Debug.LogError("  point.x  " + point.x + "  point.y  " + point.y + "   start.x  " + start.x+ "   start.y    " + start.y + "   g   " + g + "  h    " + h + "   f   " + f);
        if (point.isFind)
        {
            if (point.f > f)
            {
                point.f = f;
                point.g = g;
                point.h = h;
                point.parent = start;
            }

        }
        else
        {
            point.f = f;
            point.g = g;
            point.h = h;
            point.parent = start;
            open.Add(point);
            point.isFind=true;
        }

    }

    static int GetH(int posx,int posy, int endposx,int endposy)
    {
        int x =posx;
        int y = posy;
        int endx = endposx;
        int endy = endposy;
        int gapx = Mathf.Abs(endx - x);
        if (gapx % 2 == 0)
        {
            if (endy >= y - gapx / 2 && endy <= y + gapx / 2)
            {
                return gapx;
            }
            else if (endy < y - gapx / 2)
            {
                return gapx + Mathf.Abs(endy - y + gapx / 2);
            }
            else if (endy > y + gapx / 2)
            {
                return gapx + Mathf.Abs(endy - y - gapx / 2);
            }
        }
        else
        {
            if (Mathf.Abs(x) % 2 == 0)
            {
                if (endy >= y - gapx / 2 + 0.5 && endy <= y + gapx / 2 + 0.5)
                {
                    return gapx;
                }
                else if (endy < y - gapx / 2 + 0.5)
                {
                    return gapx + (int)Mathf.Abs(endy - y + gapx / 2 - 0.5f);
                }
                else if (endy > y + gapx / 2 + 0.5)
                {
                    return gapx + (int)Mathf.Abs(endy - y - gapx / 2 - 0.5f);
                }
            }
            else
            {
                if (endy >= y - gapx / 2 - 0.5 && endy <= y + gapx / 2 - 0.5)
                {
                    return gapx;
                }
                else if (endy < y - gapx / 2 - 0.5)
                {
                    return gapx + (int)Mathf.Abs(endy - y + gapx / 2 + 0.5f);
                }
                else if (endy > y + gapx / 2 - 0.5)
                {
                    return gapx + (int)Mathf.Abs(endy - y - gapx / 2 + 0.5f);
                }
            }

        }
        return 0;
    }

    static void SortRobMessage(List<mapPoint> list)
    {
        var length = list.Count - 1;
        for (var i = 0; i < length; i++)
        {
            for (var j = length; j > i; j--)
            {
                if (list[j].f < list[j - 1].f)
                {
                    var temp = list[j];
                    list[j] = list[j - 1];
                    list[j - 1] = temp;
                }
            }
        }
    }

    static List<List<mapPoint>> GetMapPoint()
    {
        List<List<mapPoint>> maps = new List<List<mapPoint>>();
        for(int i = 0; i < GameManage.Instance.intMap.Count;i++)
        {
            List<mapPoint> points = new List<mapPoint>();
            for(int j=0;j< GameManage.Instance.intMap[i].Count; j++)
            {
                mapPoint point = new mapPoint(i,j, GameManage.Instance.intMap[i][j]);
                points.Add(point);
            }
            maps.Add(points);
        }
        return maps;

    }


   
}

public  class mapPoint
{
    public int x;
    public int y;
    public int vaule = 0;
    public bool isFind = false;
    public bool isColse = false;
    public int f;
    public int g;
    public int h;
    public mapPoint parent = null;

    public mapPoint(mapPoint mapPoint)
    {
        this.x = mapPoint.x;
        this.y = mapPoint.y;
        this.vaule = mapPoint.vaule;

    }
    public mapPoint(int x, int y, int vaule)
    {
        this.x = x;
        this.y = y;
        this.vaule = vaule;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour {

    static int finishx;
    static int finishy;
    static List<mapPoint> open = new List<mapPoint>();
    static List<mapPoint> close = new List<mapPoint>();
    static List<List<mapPoint>> mapp = GameManage.Instance.mapPoints;

    public static List<mapPoint> find(Ground start, Ground finish)
    {
        Debug.LogError(finish.x + "    " + finish.y);        
        open.Clear();
        close.Clear();
        finishx = finish.x;
        finishy = finish.y;
       
        mapp[start.x][start.y].f = 0;
        mapp[start.x][start.y].parent = null;
        mapPoint now = mapp[start.x][start.y];
        now.parent = null;
        close.Add(now);
        while (true)
        {
            if (now.y + 1 < GameManage.col)
                if (addOpen(mapp[now.x][now.y + 1], now, open,now))
                    break;
            if (now.y - 1 >= 0)
                if (addOpen(mapp[now.x][now.y + 1], now, open, now))
                    break;
            if (now.x + 1 < GameManage.row)
                if (addOpen(mapp[now.x][now.y + 1], now, open, now))
                    break;
            if (now.x - 1 >= 0)
                if (addOpen(mapp[now.x][now.y + 1], now, open, now))
                    break;
            if (now.x % 2 != 0)
            {
                if (now.y + 1 < GameManage.col && now.x + 1 < GameManage.row)
                    if (addOpen(mapp[now.x][now.y + 1], now, open, now))
                        break;
                if (now.y + 1 < GameManage.col && now.x - 1 >= 0)
                    if (addOpen(mapp[now.x][now.y + 1], now, open, now))
                        break;
            }
            else
            {
                if (now.y - 1 >= 0 && now.x + 1 < GameManage.row)
                    if (addOpen(mapp[now.x][now.y + 1], now, open, now))
                        break;
                if (now.y - 1 >= 0 && now.x - 1 >= 0)
                    if (addOpen(mapp[now.x][now.y + 1], now, open, now))
                        break;
            }
            if(open.Count==0)
            {
                break;
            }
            SortRobMessage(open);
            now =open[0];
            //Debug.LogError( "  nowx  " +now.x + " nowy  " + now.y);
            close.Add(open[0]);
            open.Remove(open[0]);
            
        }

        List<mapPoint> road=new List<mapPoint>();
        while (now.parent != null)
        {
            //GameManage.Instance.groundList[now.x][now.y].ChangeMaterial();
            // Debug.LogError("       x       "+now.x+"       y      "+now.y);
            road.Add(now);
            now = now.parent;
        }
        if (road.Count == 0)
        {
            return null;
        }
        return road;

    }

    static bool addOpen(mapPoint point, mapPoint start,List<mapPoint> open, mapPoint now)
    {
        if (point.x == finishx && point.y == finishy)
        {
            now = point;
            point.parent = start;
            return true;
        }           
        if (point.vaule == 1)
            return false;
        if (close.Find(a=>a.x==point.x&&a.y==point.y)!=null)
            return false;
        int f, g, h;
        g = start.g + 1;
        h = GameTools.GetH( point.x,point.y, finishx, finishy);
        f = g + h;
        //Debug.LogError("  point.x  " + point.x + "  point.y  " + point.y + "   start.x  " + start.x+ "   start.y    " + start.y + "   g   " + g + "  h    " + h + "   f   " + f);
        if (open.Find(a => a.x == point.x && a.y == point.y)!= null)
        {
            if (point.f > f)
            {
                point.f = f;
                point.g = g;
                point.parent = start;
            }
        }
        else
        {
            point.f = f;
            point.g = g;
            point.parent = start;
            open.Add(point);
        }
        return false;
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

}

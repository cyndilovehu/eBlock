using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    public static int w = 10;
    public static int h = 20;
    public static Transform[,] grid = new Transform[w, h];
    
    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }
    //是否边界
    public static bool issideboder(Vector2 pos)
    {
        //Debug.Log(pos.x);
        bool isboder = false;
        if ((int)pos.x >= 0 && (int)pos.x < w && (int)pos.y >= 0)
            isboder = true;
        else
            isboder = false;
        return (isboder);
    }
    public static void decreaseRowsAbove(int y)
    {
        for(int i=y;i<h;i++)
        {
            decreaseRow(i);
        }
    }
    public static void decreaseRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            if(grid[x,y]!=null)
            { 
            grid[x, y - 1] = grid[x, y];
            grid[x, y] = null;

            //把上一行向下移动一次
            grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }
    public static void deleteRow(int y)
    {
        for(int x=0;x<w;x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;

            
        }
    }
    public static bool isFullRow(int y)
    {
        for(int x=0;x<w;x++)
        {
            if(grid[x,y]==null)
            {
                return false;
            }
        }
        return true;
    }
    //删除已经被填满的行
    public static void deleteFullRow(ref int point)
    {
        for(int i=0;i<h;i++)
        {
            //是否填满
            if(isFullRow(i))
            {
                point++;
                //删除
                deleteRow(i);
                //把当前行以上的行向下移动
                decreaseRowsAbove(i+1);
                i--;
            }
        }
    }
}

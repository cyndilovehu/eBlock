using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Group : MonoBehaviour
{
    Text point;
    float lastFall = 0;
    // Use this for initialization
    void Start () {
        point = GameObject.Find("Text").GetComponent<Text>();
        if (!IsValidGridPos())
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Modify position
            transform.position += new Vector3(-1, 0, 0);
            if (IsValidGridPos())
                // Its valid. Update grid.
                updategrid();
            else
                // Its not valid. revert.
                transform.position += new Vector3(1, 0, 0);

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Modify position
            transform.position += new Vector3(1, 0, 0);
            if (IsValidGridPos())
                // Its valid. Update grid.
                updategrid();
            else
                // Its not valid. revert.
                transform.position += new Vector3(-1, 0, 0);

        }
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            transform.Rotate(0, 0 ,-90);

            // See if valid
            if (IsValidGridPos())
                // It's valid. Update grid.
                updategrid();
            else
                // It's not valid. revert.
                transform.Rotate(0, 0, 90);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time-lastFall >= 0.5f)
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if valid
            if (IsValidGridPos())
            {
                // It's valid. Update grid.
                updategrid();
            }
            else
            {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                // Clear filled horizontal lines
                int i =Convert.ToInt32(point.text);
                Grid.deleteFullRow(ref i);
                point.text = i.ToString();
                // Spawn next Group
                FindObjectOfType<Spawn>().spawnNext();

                // Disable script
                enabled = false;
            }
            lastFall = Time.time;
        }
    }
    bool IsValidGridPos()
    {
        //判断是否到达边界和一个网格里是否有多个方块
        foreach(Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);
            if (!Grid.issideboder(v))
                return false;
            if (Grid.grid[(int)v.x, (int)v.y] != null && Grid.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }
    void updategrid()
    {
        //更新网格的位置
        for(int y=0;y<Grid.h;y++)
        {
            for(int x=0;x<Grid.w;x++)
            {
                if(Grid.grid[x,y]!=null)
                {
                    //判断方块是否在同一个方块组
                    if (Grid.grid[x, y].parent == transform)
                        Grid.grid[x, y] = null;
                }
            }
        }
        foreach(Transform child in transform)
        {
            //重新把方块的位置加入到网格中
            Vector2 v = Grid.roundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateMap : MonoBehaviour
{
    public int mapWidth = 100;
    public int maxHeight = 12;
    Tilemap collTilemap;
    public RuleTile groundTile;
    int startX = 1;
    int startY = -6;
    int currentWidth = 0;
    int collumnHeight;
    int minHeight;
    public float amplitude = 0.1f;

    private void Awake()
    {
        collTilemap = GetComponent<Tilemap>();
        collumnHeight = maxHeight - startY;
        currentWidth = startX;
    }

    void Start()
    {
        int[] fc = GenerateFirstCollumn();
        int[] sc = GenerateNextCollumn(fc); ;
        ApplyCollumn(fc);
        ApplyCollumn(sc);
        for (int i = 0; i < 200; i++)
        {
            sc = GenerateNextCollumn(sc);
        }
    }

    private void ApplyCollumn(int[] collumn)
    {
        Vector3Int pos = new Vector3Int
        {
            x = currentWidth++,
            z = 0
        };
        for (int i = 0; i < collumn.Length; i++)
        {
            if (collumn[i] == 1)
            {
                pos.y = i + startY;
                collTilemap.SetTile(pos, groundTile);
            }
        }
    }

    private int[] GenerateFirstCollumn()
    {
        int[] first = new int[collumnHeight];
        int top = 12 + Random.Range(1, 8);
        minHeight = top;
        int bottom = Mathf.Abs(startY) - 1;
        for (int i = 0; i < first.Length; i++)
        {
            if (i < bottom)
                first[i] = 1;
            else
            if (i >= bottom && i <= bottom + 3)
                first[i] = 0;
            else
            if (i < top)
            {
                first[i] = 1;
            }
            else
            {
                first[i] = 0;
            }
        }

        return first;
    }

    private int[] GenerateNextCollumn(int[] prevCollumn)
    {
        bool path = false;
        int safeMargin = (int)(0.25f * collumnHeight);
        int[] nextCollumn = new int[collumnHeight];
        if (minHeight + 1 < collumnHeight)
            minHeight++;
        //direction: 0 - goes down, 1 - stay flat, - 2 go up
        

        for (int i = 0; i < collumnHeight; i++)
        {
            nextCollumn[i] = 1;
            if (path == true && i >= minHeight)
                nextCollumn[i] = 0;
            if (prevCollumn[i] == 0 && path == false)
            {
                float ranVal = Random.value;
                float keepHeight = 1 / (i + 1);
                if(keepHeight >= 1f / safeMargin)
                {
                    ranVal += 0.34f;

                }
                else
                if(keepHeight < 1f / (collumnHeight - safeMargin))
                {
                    ranVal -= 0.15f;
                }
                int direction = 0;
                if (ranVal < 0.66f && ranVal > 0.33f)
                    direction = 1;
                else
                if (ranVal >= 0.66f)
                    direction = 2;
                path = true;
                nextCollumn[i] = 0;
                switch (direction)
                {
                    case 0:
                        if (i - 1 > 1)
                            nextCollumn[i - 1] = 0;
                        if (++i < collumnHeight)
                            nextCollumn[i] = 0;
                        if (++i < collumnHeight)
                        {
                            nextCollumn[i] = 0;
                            //prevCollumn[i] = 0;
                        }
                        break;
                    case 1:
                        if (i + 1 < collumnHeight)
                            nextCollumn[i++ + 1] = 0;
                        if (i + 1 < collumnHeight)
                            nextCollumn[i++ + 1] = 0;
                        break;
                    case 2:
                        nextCollumn[i] = 1;
                        if (++i < collumnHeight)
                        {
                            prevCollumn[i] = 0;
                            nextCollumn[i] = 0;
                        }
                        if (++i < collumnHeight)
                        {
                            prevCollumn[i] = 0;
                            nextCollumn[i] = 0;
                        }
                        if (++i < collumnHeight)
                        {
                            prevCollumn[i] = 0;
                            nextCollumn[i] = 0;
                        }
                        if (++i < collumnHeight)
                        {
                            prevCollumn[i] = 0;
                            nextCollumn[i] = 0;
                        }
                        break;
                }
            }
        }
        ApplyCollumn(prevCollumn);
        return nextCollumn;
    }
}

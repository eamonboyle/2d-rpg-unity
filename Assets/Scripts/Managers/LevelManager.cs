using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Transform map;

    [SerializeField]
    private Texture2D[] mapData;

    [SerializeField]
    private MapElement[] mapElements;

    [SerializeField]
    private Sprite defaultTile;

    private Vector3 WorldStartPos
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        }
    }

    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        for (int i = 0; i < mapData.Length; i++)
        {
            for (int x = 0; x < mapData[i].width; x++)
            {
                for (int y = 0; y < mapData[i].height; y++)
                {
                    Color c = mapData[i].GetPixel(x, y);

                    MapElement newElement = Array.Find(mapElements, e => e.Color == c);

                    if (newElement != null)
                    {
                        float xPos = WorldStartPos.x + (defaultTile.bounds.size.x * x);
                        float yPos = WorldStartPos.y + (defaultTile.bounds.size.y * y);

                        GameObject go = Instantiate(newElement.ElementPrefab);
                        go.transform.position = new Vector2(xPos, yPos);
                        go.transform.parent = map;
                    }
                }
            }
        }
    }
}

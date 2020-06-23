using System;
using UnityEngine;

[Serializable]
public class MapElement
{
    [SerializeField]
    private string tileTag;

    [SerializeField]
    private Color color;

    [SerializeField]
    private GameObject elementPrefab;

    public string TileTag { get => tileTag; }
    public GameObject ElementPrefab { get => elementPrefab; }
    public Color Color { get => color; }
}

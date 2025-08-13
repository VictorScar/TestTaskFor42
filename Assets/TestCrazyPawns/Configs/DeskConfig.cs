using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameConfigs/Desk", fileName = "DeskConfig")]
public class DeskConfig : ScriptableObject
{
    [SerializeField] private Vector2 cellSize = new Vector2(1.5f, 1.5f);
    [SerializeField] private Color blackCelColor;
    [SerializeField] private Color whiteCelColor;
  
    public Vector2 CellSize => cellSize;
    public Color BlackCelColor => blackCelColor;
    public Color WhiteCelColor => whiteCelColor;
}
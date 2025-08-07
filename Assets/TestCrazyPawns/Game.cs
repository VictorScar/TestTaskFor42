using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private DeskGenerator deskGenerator;
    [SerializeField] private DeskConfig config;

    private void Start()
    {
        var generatorData = new DeskGeneratorData
        {
            DeskSize = config.DeskSize,
            CellSize = config.CellSize,
            BlackCelColor = config.BlackCelColor,
            WhiteCelColor = config.WhiteCelColor
        };
        
        deskGenerator.Generate(generatorData);
    }
}
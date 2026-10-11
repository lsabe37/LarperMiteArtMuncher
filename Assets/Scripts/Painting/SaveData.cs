using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveData // basically a global class, can be accessed from any scene, data exists independently of scenes/gameobjects 
{
    public static ColorIdentity current_mouse_color;

    public static int current_painting = 0;

    public static bool passed_painting; // every time player finishes a painting/runs out of time, store if they passed/failed here


}

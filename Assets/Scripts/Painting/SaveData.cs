using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveData // basically a global class, can be accessed from any scene, data exists independently of scenes/gameobjects 
{
    public static ColorIdentity current_mouse_color;

    public static int current_painting = 0;

    public static float painting_correctness_score = 0; // every time player finishes a painting/runs out of time, store the score to here. 
                        // min = 0, max = 7. Total of 8 paintings. 


}

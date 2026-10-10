using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorIdentity
{
    white = 0,
    black = 1,
    red = 2,
    yellow = 3,
    blue = 4,
    green = 5,
    orange = 6,
    purple = 7,
    brown = 8,
    gray = 9,
    gold = 10

}

public class ColorGridTile : MonoBehaviour
{
    public ColorIdentity current_color_identity;
    public ColorIdentity actual_color_identity;

    public bool IsCorrectColor()
    {
        return (current_color_identity == actual_color_identity);

    }



}

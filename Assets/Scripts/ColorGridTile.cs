using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorGridTile : MonoBehaviour
{
    public ColorIdentity current_color_identity;
    public ColorIdentity actual_color_identity;
    public SpriteRenderer tile_sprite;
    public int x_coordinate;
    public int y_coordinate;

    public bool IsCorrectColor()
    {
        return (current_color_identity == actual_color_identity);

    }



}

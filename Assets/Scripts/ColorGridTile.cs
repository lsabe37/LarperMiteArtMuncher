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

    public void SetSpriteColor(ColorIdentity identity)
    {
        tile_sprite.color = GetColorFromIdentity(identity);

    }

    public void SetSpriteColorToRealColor()
    {
        tile_sprite.color = GetColorFromIdentity(actual_color_identity);
    }

    public Color GetColorFromIdentity(ColorIdentity identity)
    {
        switch (identity)
        {
            case ColorIdentity.black:
                return RGBConverter(new Color(24, 20, 37));

            case ColorIdentity.white:
                return RGBConverter(new Color(235, 241, 242));

            case ColorIdentity.red:
                return RGBConverter(new Color(162, 38, 5));

            case ColorIdentity.green:
                return RGBConverter(new Color(38, 92, 66));

            default:
                return new Color(0, 0, 0);

        }

    }

    private Color RGBConverter(Color RGB255)
    {
        return (new Color(RGB255.r / 255f, RGB255.g / 255f, RGB255.b / 255f));

    }
}

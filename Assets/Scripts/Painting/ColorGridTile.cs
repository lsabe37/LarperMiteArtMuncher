using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ColorGridTile : MonoBehaviour
{
    public ColorIdentity current_color_identity;
    public ColorIdentity actual_color_identity;
    public ColorIdentity background_color_identity;
    public SpriteRenderer tile_sprite;
    public int x_coordinate;
    public int y_coordinate;

    private bool mouse_entered = false;

    private void Update()
    {
        if (mouse_entered)
        {
            if (Input.GetMouseButton(0))
            {
                StopAllCoroutines();
                StartCoroutine(ChangeTileColor(MouseData.current_mouse_color));
            }

        }        
    }


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

    private void OnMouseDown()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeTileColor(MouseData.current_mouse_color));
    }

    private void OnMouseExit()
    {
        mouse_entered = false;
    }

    private void OnMouseOver()
    {
        mouse_entered = true;
    }

    public IEnumerator ChangeTileColor(ColorIdentity new_color_identity, float wait_time = 1f)
    {

        if (new_color_identity == ColorIdentity.nothing) yield break;

        Color color_current = tile_sprite.color;
        Color color_new = GetColorFromIdentity(new_color_identity);

        current_color_identity = new_color_identity;

        float timer = 0f;

        while (timer <= wait_time)
        {
            timer += Time.deltaTime;
            tile_sprite.color = Color.Lerp(color_current, color_new, timer / wait_time);
            yield return null;
        }

        transform.rotation = Quaternion.identity;
        yield return null;
    
    }


}

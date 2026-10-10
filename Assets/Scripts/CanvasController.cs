using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    ColorGridTile[,] canvas_array;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeSprite(int height, int width, ColorIdentity background_color = ColorIdentity.white)
    {
        canvas_array = new ColorGridTile[height, width];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                ColorGridTile color_tile = new ColorGridTile();
                color_tile.current_color_identity = background_color;
                canvas_array[y, x] = color_tile;
            }

        }

    }





}



using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject tile;
    [SerializeField] Palette palette;


    ColorGridTile[,] current_painting;

    // ==========================================

    // Painting Validation System - LeoNeo


    public bool CheckPainting()
    {
        if (current_painting == null)
        {
            return false;
        }

        int correctTiles = 0;
        int totalTiles = current_painting.GetLength(0) * current_painting.GetLength(1);


        if (totalTiles == 0)
        {
            return false;
        }

        for (int y = 0; y < current_painting.GetLength(0); y++)
        {
            for (int x = 0; x < current_painting.GetLength(1); x++)
            {
                if (current_painting[y, x].IsCorrectColor())
                {
                    correctTiles++;
                }
            }
        }
        float accuracy = ((float)correctTiles / totalTiles) * 100;
        Debug.Log(("Painting Accuracy: " + accuracy + "%"));
        Debug.Log("Correct Tiles: " + correctTiles + "/" + totalTiles);

        if (accuracy > 60)
        {
            Debug.Log("Painting Passed!"); 
            return true;
        }
        else
        {
            Debug.Log("Painting failed!"); 
            return false;
        }
    }

    // ==========================================


    // Start is called before the first frame update
    void Start()
    {
        InitializePainting0();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeTiles(int height, int width, float scale, float center_x = 0f, float center_y = 0f)
    {
        current_painting = new ColorGridTile[height, width];
        tile.transform.localScale = new Vector3(scale, scale);

        float starting_x_position = 0;
        float starting_y_position = 0;


        starting_y_position = center_y + (scale * (height / 2f)) - (scale / 2);
        starting_x_position = center_x - (scale * (width / 2f)) + (scale / 2);
       
        float x_position = starting_x_position;
        float y_position = starting_y_position;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                ColorGridTile instantiated_tile = Instantiate(tile, new Vector3(x_position, y_position, 0), Quaternion.identity).GetComponent<ColorGridTile>();
                x_position += scale;
                instantiated_tile.x_coordinate = x;
                instantiated_tile.y_coordinate = y;
                current_painting[y, x] = instantiated_tile;
            }

            x_position = starting_x_position;
            y_position -= scale;

        }

    }

    public void InitializePainting0()
    {
        int height = 3;
        int width = 5;
        float scale = 2f;

        List<ColorIdentity> colors_required = new List<ColorIdentity> {ColorIdentity.black, ColorIdentity.white };


        InitializeTiles(height, width, scale);

        ColorIdentity[,] painting_0_colors =
        {
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white},
            {ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white,},
            {ColorIdentity.black, ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white }
        };

        SetTilestoActualColors(height, width, painting_0_colors);
        palette.InitializePalette(colors_required);
    }

    public void SetTilestoActualColors(int height, int width, ColorIdentity[,] painting_colors)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                ColorGridTile tile = current_painting[y, x];
                tile.actual_color_identity = painting_colors[y, x];
                tile.current_color_identity = tile.actual_color_identity;
                tile.SetSpriteColorToRealColor();
            }

        }
    }

}



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

    public void InitializePainting(int height, int width, float scale, ColorIdentity background_color, List<ColorIdentity> colors_required, ColorIdentity[,] painting_colors, float wait_time)
    {
        InitializeTiles(height, width, scale);
        SetTilestoActualColors(height, width, painting_colors, background_color, 6);
        palette.InitializePalette(colors_required);
    }


    public void InitializePainting0()
    {
        ColorIdentity[,] painting_0_colors =
{
            {ColorIdentity.black, ColorIdentity.white, ColorIdentity.black, ColorIdentity.black},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black},
            {ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black},
            {ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.white}
        };
        InitializePainting(4, 4, 2f, ColorIdentity.white, new List<ColorIdentity> { ColorIdentity.black, ColorIdentity.white }, painting_0_colors, 6f);
    }

    public void SetTilestoActualColors(int height, int width, ColorIdentity[,] painting_colors, ColorIdentity background_color, float wait_time)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                ColorGridTile tile = current_painting[y, x];
                tile.actual_color_identity = painting_colors[y, x];
                tile.current_color_identity = tile.actual_color_identity;
                tile.background_color_identity = background_color;
                tile.SetSpriteColorToRealColor();

                wait_time *= Random.Range(0.8f, 1f);
                tile.StartCoroutine(tile.ChangeTileColor(tile.background_color_identity, wait_time));
            }

        }
    }

}



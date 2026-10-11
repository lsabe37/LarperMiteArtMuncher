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
        InitializePainting3();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializePainting(int height, int width, float scale, float center_x, float center_y, ColorIdentity background_color, List<ColorIdentity> colors_required, ColorIdentity[,] painting_colors, float wait_time)
    {
        InitializeTiles(height, width, scale, center_x, center_y);
        SetTilestoActualColors(height, width, painting_colors, background_color, wait_time);
        palette.InitializePalette(colors_required);
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

                
                float individual_wait_time = wait_time * Random.Range(0.3f, 1.35f);
                tile.StartCoroutine(tile.ChangeTileColor(tile.background_color_identity, individual_wait_time));
            }

        }
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
        InitializePainting(height: 4, width: 4, scale: 2f, center_x: 0, center_y: 0.5f, background_color: ColorIdentity.white, 
                           colors_required: new List<ColorIdentity> { ColorIdentity.black, ColorIdentity.white }, painting_colors: painting_0_colors, wait_time: 4f);
    }
    public void InitializePainting1()
    {
        ColorIdentity[,] painting_1_colors =
{
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.black, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white},
            {ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black}
        };
        InitializePainting(height: 5, width: 5, scale: 1.5f, center_x: 0, center_y: 0, background_color: ColorIdentity.white,
                           colors_required: new List<ColorIdentity> { ColorIdentity.black, ColorIdentity.white }, painting_colors: painting_1_colors, wait_time: 5f); 
    }

    public void InitializePainting2()
    {
        ColorIdentity[,] painting_1_colors =
{
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.black, ColorIdentity.white, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white},
            {ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white, ColorIdentity.black},
            {ColorIdentity.black, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black},
            {ColorIdentity.white, ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.black, ColorIdentity.white, ColorIdentity.white}
        };
        InitializePainting(height: 6, width: 6, scale: 1.25f, center_x: 0, center_y: 0.5f, background_color: ColorIdentity.white,
                           colors_required: new List<ColorIdentity> { ColorIdentity.black, ColorIdentity.white }, painting_colors: painting_1_colors, wait_time: 4.3f);
    }

    public void InitializePainting3()
    {
        ColorIdentity[,] painting_1_colors =
{
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.white},
            {ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black},
            {ColorIdentity.white, ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white}

        };
        InitializePainting(height: 7, width: 7, scale: 1.1f, center_x: 0, center_y: 0.5f, background_color: ColorIdentity.white,
                           colors_required: new List<ColorIdentity> { ColorIdentity.black, ColorIdentity.white }, painting_colors: painting_1_colors, wait_time: 4.3f);
    }


}



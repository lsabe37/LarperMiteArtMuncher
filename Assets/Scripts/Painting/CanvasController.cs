using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject tile; //each tile is a ColorGridTile and is one of the squares on screen 
    [SerializeField] Palette palette; // the palette starts below screen and moves up 
    [SerializeField] GameObject frame;
    [SerializeField] TextMeshProUGUI timer_text;


    ColorGridTile[,] current_painting; // player's painting is stored in this 2d array 


    // Start is called before the first frame update
    void Start()
    {
        switch (SaveData.current_painting) //save data contains global static data 
        {
            case 0:
                InitializePainting0();
                break;
            case 1:
                InitializePainting1();
                break;
            case 2:
                InitializePainting2();
                break;
            case 3:
                InitializePainting3();
                break;
            case 4:
                InitializePainting4();
                break;
            case 5:
                InitializePainting5();
                break;
            case 6:
                InitializePainting6();
                break;
            case 7:
                InitializePainting7();
                break;
        }
    }


    void InitializePainting(int height, int width, float scale, float center_x, float center_y,
                            ColorIdentity background_color, List<ColorIdentity> colors_required, ColorIdentity[,] painting_colors,
                            float wait_time, int time_limit, float intereference_count = 0, float wait_time_lower = 0.35f, float wait_time_upper = 1.35f,
                            float frame_scale=4) // height & width are in terms of tile count, painting_colors contains the actual color data of the painting
                                                 // wait_time is the number of seconds the actual painting's colors remain on screen before fading into background colors
                                                 // wait_time_lower & upper apply variance to the wait time so not all tiles fade out at once 
                                                 // interference count is WIP
                                                 // frame_scale is just the scale of the frame surrounding the canvas 
    {


        SaveData.current_mouse_color = ColorIdentity.nothing;
        InitializeTiles(height, width, scale, center_x, center_y);
        InitializeFrame(center_x, center_y, frame_scale);
        SetTilestoActualColors(height, width, painting_colors, background_color, wait_time, wait_time_lower, wait_time_upper);
        palette.InitializePalette(colors_required, wait_time / 2f, 0.75f);
        StartCoroutine(InitializeTimer(time_limit));
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

    public void InitializeFrame(float center_x, float center_y, float scale)
    {
        GameObject frame_object = Instantiate(frame, new Vector3(center_x, center_y), Quaternion.identity);
        frame_object.transform.localScale = new Vector3(scale, scale);



    }

    public void SetTilestoActualColors(int height, int width, ColorIdentity[,] painting_colors, ColorIdentity background_color, float wait_time, float wait_time_lower, float wait_time_upper)
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

                
                float individual_wait_time = wait_time * Random.Range(wait_time_lower, wait_time_upper);
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
        InitializePainting(height: 4, width: 4, scale: 1.7f, center_x: 0, center_y: 0.5f, background_color: ColorIdentity.white, 
                           colors_required: new List<ColorIdentity> { ColorIdentity.black, ColorIdentity.white }, 
                           painting_colors: painting_0_colors, wait_time: 4f, time_limit: 15, frame_scale: 1.29f);
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
                           colors_required: new List<ColorIdentity> { ColorIdentity.black, ColorIdentity.white }, 
                           painting_colors: painting_1_colors, wait_time: 5f, time_limit: 15, frame_scale: 1.42f); 
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
        InitializePainting(height: 6, width: 6, scale: 1.25f, center_x: 0, center_y: 0.45f, background_color: ColorIdentity.white,
                           colors_required: new List<ColorIdentity> { ColorIdentity.black, ColorIdentity.white }, 
                           painting_colors: painting_1_colors, wait_time: 4.3f, time_limit: 20, frame_scale: 1.42f);
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
        InitializePainting(height: 7, width: 7, scale: 1.1f, center_x: 0, center_y: 0.38f, background_color: ColorIdentity.white,
                           colors_required: new List<ColorIdentity> { ColorIdentity.black, ColorIdentity.white }, 
                           painting_colors: painting_1_colors, wait_time: 4.3f, time_limit: 20, frame_scale: 1.45f);
    }

    public void InitializePainting4()
    {
        ColorIdentity[,] painting_1_colors =
{
            {ColorIdentity.white, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white},
            {ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white, ColorIdentity.red,   ColorIdentity.white},
            {ColorIdentity.black, ColorIdentity.black, ColorIdentity.white, ColorIdentity.black, ColorIdentity.black, ColorIdentity.white, ColorIdentity.red,   ColorIdentity.white},
            {ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white}

        };
        InitializePainting(height: 8, width: 8, scale: 0.95f, center_x: 0, center_y: 0.35f, background_color: ColorIdentity.white,
                           colors_required: new List<ColorIdentity> { ColorIdentity.black, ColorIdentity.white, ColorIdentity.red },
                           painting_colors: painting_1_colors, wait_time: 5f, time_limit: 25, frame_scale: 1.44f);
    }

    public void InitializePainting5()
    {
        ColorIdentity[,] painting_1_colors =
{
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.red},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.red},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.red},
            {ColorIdentity.white, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.red},
            {ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.red},
            {ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.white},
            {ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.red,   ColorIdentity.white, ColorIdentity.white},
            {ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white}
        };
        InitializePainting(height: 9, width: 9, scale: 0.83f, center_x: 0, center_y: 0.48f, background_color: ColorIdentity.white,
                           colors_required: new List<ColorIdentity> { ColorIdentity.black, ColorIdentity.white, ColorIdentity.red },
                           painting_colors: painting_1_colors, wait_time: 5f, time_limit: 25, frame_scale: 1.41f);
    }
    public void InitializePainting6()
    {
        ColorIdentity[,] painting_1_colors =
{
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.red,   ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.red,   ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.red,   ColorIdentity.white, ColorIdentity.white, ColorIdentity.red,   ColorIdentity.white, ColorIdentity.white, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.white, ColorIdentity.white, ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.white, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.red,   ColorIdentity.white, ColorIdentity.red,   ColorIdentity.white, ColorIdentity.white, ColorIdentity.red,   ColorIdentity.white, ColorIdentity.red,   ColorIdentity.white},
            {ColorIdentity.red,   ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.red,   ColorIdentity.black, ColorIdentity.black, ColorIdentity.white, ColorIdentity.white, ColorIdentity.red,   ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.red,   ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.white, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.red,   ColorIdentity.red,   ColorIdentity.white, ColorIdentity.white, ColorIdentity.white}

        };
        InitializePainting(height: 10, width: 10, scale: 0.75f, center_x: 0, center_y: 0.47f, background_color: ColorIdentity.white,
                           colors_required: new List<ColorIdentity> { ColorIdentity.black, ColorIdentity.white, ColorIdentity.red},
                           painting_colors: painting_1_colors, wait_time: 8f, time_limit: 25, wait_time_lower: 0.6f, wait_time_upper: 1.35f, frame_scale: 1.42f);
    }

    public void InitializePainting7()
    {
        ColorIdentity[,] painting_1_colors =
{
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.yellow,ColorIdentity.white, ColorIdentity.yellow,ColorIdentity.white, ColorIdentity.yellow,ColorIdentity.white, ColorIdentity.white, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.yellow,ColorIdentity.yellow,ColorIdentity.yellow,ColorIdentity.yellow,ColorIdentity.yellow,ColorIdentity.white, ColorIdentity.white, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.blue,  ColorIdentity.blue,  ColorIdentity.blue,  ColorIdentity.blue,  ColorIdentity.blue,  ColorIdentity.white, ColorIdentity.white, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.blue,  ColorIdentity.black, ColorIdentity.blue,  ColorIdentity.black, ColorIdentity.blue,  ColorIdentity.white, ColorIdentity.red,   ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.blue,  ColorIdentity.blue,  ColorIdentity.blue,  ColorIdentity.blue,  ColorIdentity.blue,  ColorIdentity.white, ColorIdentity.black, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.black, ColorIdentity.black, ColorIdentity.white, ColorIdentity.red,   ColorIdentity.white, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.black, ColorIdentity.red,   ColorIdentity.black, ColorIdentity.black, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.black, ColorIdentity.white, ColorIdentity.black, ColorIdentity.white},
            {ColorIdentity.white, ColorIdentity.white, ColorIdentity.blue,  ColorIdentity.white, ColorIdentity.white, ColorIdentity.white, ColorIdentity.blue,  ColorIdentity.white, ColorIdentity.black, ColorIdentity.white}

        };
        InitializePainting(height: 10, width: 10, scale: 0.75f, center_x: 0, center_y: 0.47f, background_color: ColorIdentity.white,
                           colors_required: new List<ColorIdentity> { ColorIdentity.black, ColorIdentity.white, ColorIdentity.red, ColorIdentity.blue, ColorIdentity.yellow},
                           painting_colors: painting_1_colors, wait_time: 10f, time_limit: 27, wait_time_lower: 0.4f, wait_time_upper: 1f, frame_scale: 1.42f);
    }

    public IEnumerator InitializeTimer(int time_limit)
    {

        int time = time_limit;

        while (time > -1)
        {
            string timer_string = time.ToString();
            if (time < 10)
                timer_string = "0" + timer_string;
            timer_text.text = "Time Until *They* Find You: 0:" + timer_string;

            time -= 1;
            yield return new WaitForSeconds(1);
        }

        Debug.Log("TIME UP");
        StartCoroutine(FinishGame());
        yield return null;

    }

    public void FinishButtonPressed()
    {
        StopAllCoroutines();
        StartCoroutine(FinishGame());
    }

    public IEnumerator FinishGame()
    {
        float move_down_time = 0.3f;

        SaveData.current_mouse_color = ColorIdentity.nothing;
        palette.DisablePalette(move_down_time);
        yield return new WaitForSeconds(0.3f);

        Debug.Log("GAME OVER");
        SaveData.current_painting++; // such that the next time player enters this scene, the next painting will be displayed 
        yield return null;
    }

}



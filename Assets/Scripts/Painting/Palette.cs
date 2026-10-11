using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Palette : MonoBehaviour
{
    [SerializeField] Sprite black_splotch;
    [SerializeField] Sprite white_splotch;
    [SerializeField] Sprite blue_splotch;
    [SerializeField] Sprite red_splotch;
    [SerializeField] Sprite yellow_splotch;
    [SerializeField] Sprite green_splotch;



    [SerializeField] GameObject blotch_0;
    [SerializeField] GameObject blotch_1;
    [SerializeField] GameObject blotch_2;
    [SerializeField] GameObject blotch_3;
    [SerializeField] GameObject blotch_4;


    private List<GameObject> blotch_list;

    private void Awake()
    {
        blotch_list = new List<GameObject> { blotch_0, blotch_1, blotch_2, blotch_3, blotch_4};
    }

    public void InitializePalette(List<ColorIdentity> colors_required, float wait_time, float move_time)
    {
        for (int i = 0; i < colors_required.Count; i++) {

            ColorIdentity current_color = colors_required[i];
            GameObject blotch_object = blotch_list[i];
            blotch_object.SetActive(true);
            Blotch current_blotch = blotch_object.GetComponent<Blotch>();
            current_blotch.blotch_color = current_color;

            switch (current_color)
            {
                case (ColorIdentity.black):
                    current_blotch.sprite_renderer.sprite = black_splotch;
                    break;
                case (ColorIdentity.white):
                    current_blotch.sprite_renderer.sprite = white_splotch;
                    break;
                case (ColorIdentity.red):
                    current_blotch.sprite_renderer.sprite = red_splotch;
                    break;
                case (ColorIdentity.blue):
                    current_blotch.sprite_renderer.sprite = blue_splotch;
                    break;
                case (ColorIdentity.yellow):
                    current_blotch.sprite_renderer.sprite = yellow_splotch;
                    break;
                case (ColorIdentity.green):
                {
                    current_blotch.sprite_renderer.sprite = green_splotch;
                    break;
                }
            }
        
        }

        StartCoroutine(MoveUp(2.0f, wait_time, move_time));

    }
    public IEnumerator MoveUp(float distance, float wait_time, float move_time)
    {
        float timer = 0;

        yield return new WaitForSeconds(wait_time);

        Vector3 target_position = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);
        Vector3 current_position = transform.position;

        while (timer < move_time)
        {
            transform.position = Vector3.Lerp(current_position, target_position, timer / move_time);
            timer += Time.deltaTime;
            yield return null;
        }


    }

    public void DisablePalette(float move_down_time)
    {
        for (int i = 0; i < blotch_list.Count; i++)
        {
            Blotch blotch_component = blotch_list[i].GetComponent<Blotch>();
            blotch_component.blotch_enabled = false;

        }

        StopAllCoroutines();
        StartCoroutine(MoveUp(-2f, 0, move_down_time));
    }


}

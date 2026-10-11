using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palette : MonoBehaviour
{
    [SerializeField] Sprite black_splotch;
    [SerializeField] Sprite white_splotch;
    [SerializeField] Sprite green_splotch;
    [SerializeField] Sprite red_splotch;

    [SerializeField] GameObject blotch_0;
    [SerializeField] GameObject blotch_1;
    [SerializeField] GameObject blotch_2;
    [SerializeField] GameObject blotch_3;

    private List<GameObject> blotch_list;

    private void Start()
    {
        blotch_list = new List<GameObject> { blotch_0, blotch_1, blotch_2, blotch_3 };
    }

    public void InitializePalette(List<ColorIdentity> colors_required)
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
                case (ColorIdentity.green):
                    current_blotch.sprite_renderer.sprite = green_splotch;
                    break;
            }
        
        }




    }





}

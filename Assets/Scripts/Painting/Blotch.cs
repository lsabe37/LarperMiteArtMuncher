using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blotch : MonoBehaviour
{
    [SerializeField] 
    public SpriteRenderer sprite_renderer;

    public ColorIdentity blotch_color = ColorIdentity.nothing;

    private void OnMouseDown()
    {
        MouseData.current_mouse_color = blotch_color;
        Debug.Log(MouseData.current_mouse_color);
    }








}

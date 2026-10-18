
using UnityEngine;

public class Blotch : MonoBehaviour
{
    [SerializeField] 
    public SpriteRenderer sprite_renderer;

    public ColorIdentity blotch_color = ColorIdentity.nothing;
    public bool blotch_enabled = true;

    private Color current_color;

    private void Start()
    {
        current_color = sprite_renderer.color;
    }

    private void OnMouseDown()
    {
        if (blotch_enabled == true)
        {
            SaveData.current_mouse_color = blotch_color;
            Debug.Log(SaveData.current_mouse_color);
        }
    }

    private void OnMouseOver()
    {
        sprite_renderer.color = new Color(current_color.r + 0.1f, current_color.g + 0.1f, current_color.b + 0.1f, 0.95f);
    }

    private void OnMouseExit()
    {
        sprite_renderer.color = current_color;
    }

    




}

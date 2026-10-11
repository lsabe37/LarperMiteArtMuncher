using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] Animator title_animation;
    [SerializeField] GameObject play_button;

    [SerializeField] MovingBackground background1;
    [SerializeField] MovingBackground background2;
    [SerializeField] MovingBackground background3;
    [SerializeField] MovingBackground background4;
    [SerializeField] MovingBackground background5;

    private List<MovingBackground> background_list;

    // Start is called before the first frame update
    void Start()
    {
        background_list = new List<MovingBackground> { background1, background2, background3, background4, background5 };
        title_animation.speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonPressed()
    {
        play_button.SetActive(false);
        StartCoroutine(DissolveTitle());
    }

    private IEnumerator DissolveTitle()
    {
        title_animation.speed = 1f;
        yield return new WaitForSeconds(0.7f);
        title_animation.transform.gameObject.SetActive(false);


    }

}

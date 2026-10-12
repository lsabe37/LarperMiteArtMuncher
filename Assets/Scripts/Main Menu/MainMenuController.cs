using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] Animator title_animation;
    [SerializeField] GameObject play_button;

    [SerializeField] MovingBackground background1;
    [SerializeField] MovingBackground background2;
    [SerializeField] MovingBackground background3;
    [SerializeField] MovingBackground background4;
    [SerializeField] MovingBackground background5;
    [SerializeField] GameObject museum_entrance;

    [SerializeField] PlayerCs termiteCS;

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

        yield return new WaitForSeconds(1.0f);

        float timer = 0;
        float entrance_timer = 0;


        while (timer < 4)
        {
            if (timer > 2.5 && entrance_timer < 3.5)
            {
                entrance_timer += Time.deltaTime;
                museum_entrance.transform.position = Vector3.Lerp(new Vector3(8.08f, 0.66f, -9.67f), new Vector3(2f, 0.66f, -9.67f), entrance_timer / 1);
            }


                timer += Time.deltaTime;
            termiteCS.transform.position += Time.deltaTime * Vector3.right * 3;


            yield return null;
        }
        SceneManager.LoadScene(1);


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelWithTransition : MonoBehaviour
{
    public Animator transition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LoadLevelTransition());
        }
    }

    IEnumerator LoadLevelTransition()
    {
        //plays the animation
        transition.SetTrigger("Start");

        //wait for animation of transition to fade to black, this lasts for 1 second, hence we need to wait for 1 second before entering the next level
        yield return new WaitForSeconds(1f);

        //load the next level or end level here; you can also use a parameter to get the build index to go to the next scene
        SceneManager.LoadScene("Title Scene");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(LoadLevelTransition());
        }
    }
}

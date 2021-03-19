using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionToBlackEnd : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeToBlack()
    {
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        //change to the end scene
        SceneManager.LoadScene("EndScene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(FadeToBlack());
        }
    }
}

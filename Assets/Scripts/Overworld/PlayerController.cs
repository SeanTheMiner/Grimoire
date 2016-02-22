using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    //player variables

    private Rigidbody rb;
    public int speed;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //in game functions

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (moveHorizontal !=0f || moveVertical !=0f)
        {
            rb.AddForce(movement * speed);
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

       
    }


    //handles colliders

    void OnTriggerEnter(Collider other)
    {

        /*

        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            SetCountText();
        }

        if (other.gameObject.CompareTag("Lose Plane"))
        {

            if (winMenu.enabled == false)
            {
                other.gameObject.SetActive(false);
                count = 0;
                SetCountText();
                loseMenu.enabled = true;
            }
            else
            {
                winMenu.enabled = true;
            }
        }

    */

    }



    //Menu functions

    public void EnterBattle()
    {
        SceneManager.LoadScene(sceneName: "Battle");
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(sceneName: "StartMenu");
    }



}
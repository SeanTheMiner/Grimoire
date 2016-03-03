using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BattleSpawner : MonoBehaviour {

    public GameObject player;
    Rigidbody playerRigidbody;

    void Start() {
        playerRigidbody = player.GetComponent<Rigidbody>();
    }

   void OnTriggerEnter(Collider other) {

        if (other.tag == "Biome") {
            SceneManager.LoadScene(sceneName: "Battle");
        }

    } //end OnTriggerEnter()

} //end BattleSpawner class

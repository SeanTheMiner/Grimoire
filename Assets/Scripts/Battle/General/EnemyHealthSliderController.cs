using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Enemies;

public class EnemyHealthSliderController : MonoBehaviour {

    Slider enemyHealthSlider;
    public Enemy enemy;


	// Use this for initialization
	void Start () {

        enemyHealthSlider = gameObject.GetComponent<Slider>();
        enemyHealthSlider.maxValue = enemy.maxHealth;
        enemyHealthSlider.value = enemy.currentHealth;

	}

    // Update is called once per frame
    void Update() {

        if (enemy != null) {
            enemyHealthSlider.value = enemy.currentHealth;
        }
        else {
            Destroy(gameObject);
        }

	}

} //end EnemyHealthSliderController class

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

using Heroes;
using Enemies;
using Biomes;

public class BattleManager : MonoBehaviour {

    public BattleDisplayManager battleDisplayManager = new BattleDisplayManager();

    public Enemy enemyObjectOne;
    public Enemy enemyObjectTwo;
    public Enemy enemyObjectThree;
    private Enemy selectedEnemy;

    public Hero heroObjectOne;
    public Hero heroObjectTwo;
    public Hero selectedHero;

    public List<Enemy> enemyList = new List<Enemy>();
    public Queue<GameObject> enemyQueue;

    private bool selecterActive;

    // Use this for initialization
    void Start() {

        enemyList.Add(enemyObjectOne);
        enemyList.Add(enemyObjectTwo);
        enemyList.Add(enemyObjectThree);

        enemyObjectOne.currentHealth = enemyObjectOne.maxHealth;
        enemyObjectTwo.currentHealth = enemyObjectTwo.maxHealth;
        enemyObjectThree.currentHealth = enemyObjectThree.maxHealth;

        battleDisplayManager.UpdateHealthText();
        battleDisplayManager.NoHeroSelected();

        selecterActive = false;

    }

    

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            selectedHero = heroObjectOne;
            battleDisplayManager.UpdateSelectedHeroText();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            selectedHero = heroObjectTwo;
            battleDisplayManager.UpdateSelectedHeroText();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            selectedHero = null;
            battleDisplayManager.NoHeroSelected();
        }



        if (Input.GetButtonDown("Ability One")) {
            selecterActive = true;
        }

        if ((selecterActive) && (Input.GetMouseButtonDown(0))) {
            CastSelecterRay();
            selecterActive = false;
        }

        if(selectedEnemy != null) {
            selectedEnemy.currentHealth -= 100;
            CheckIfDead(selectedEnemy);
            selectedEnemy = null;

        }

        if (enemyList.Count <= 0) {
            BattleWon();
        }

        battleDisplayManager.UpdateHealthText();

    } //end Update

    void CastSelecterRay() {

        Debug.Log("ray cast");

        RaycastHit objectHit;
        Ray selectingRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(selectingRay, out objectHit)) {

            if((objectHit.collider.tag == "Enemy")) {

                selectedEnemy = objectHit.collider.gameObject.GetComponent<Enemy>();

            } //end if hero or enemy

            else {
                selectedEnemy = null;
                selecterActive = false;

            }

        }  //end if objectHit

    } //end CastSelecterRay


    void CheckIfDead(Enemy enemy) {
        Debug.Log("Checked if dead");
        if(enemy.currentHealth <= 0) {
            enemy.currentHealth = 0;
            enemyList.Remove(enemy);
            Destroy(enemy.gameObject);
            Destroy(enemy);
        }
    }

    void BattleWon() {
        Debug.Log("Battlewoncalled");
        SceneManager.LoadScene(sceneName: "Overworld");
    }

    void BattleLost() {
        SceneManager.LoadScene(sceneName: "Overworld");
    }
}


    

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Heroes;
using Artifacts;

public class ControlManager : MonoBehaviour {

    public GameObject mainMenuPanel;
    public GlobalInfoKeeper globalInfoKeeper;

    public Button heroButtonOne, heroButtonTwo, heroButtonThree, heroButtonFour,
        artifactButtonOne, artifactButtonTwo, artifactButtonThree,
        artifactButtonFour, artifactButtonFive, artifactButtonSix,
        artifactButtonSeven, artifactButtonEight, artifactButtonNine,
        artifactButtonTen, artifactButtonEleven, artifactButtonTwelve;
    
    // Use this for initialization
    void Start () {
        mainMenuPanel.SetActive(false);
        globalInfoKeeper = GameObject.Find("GlobalInfoKeeper").GetComponent<GlobalInfoKeeper>();
    }
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetKeyDown(KeyCode.Tab)) {
            mainMenuPanel.SetActive(!mainMenuPanel.activeSelf);
            ResetMainMenuPanel();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            UpdateButtonTexts();
        }

	} //end Update()

    
    void ResetMainMenuPanel() {
        UpdateButtonTexts();
    }


    void UpdateButtonTexts () {

        SetHeroButtonText(heroButtonOne, globalInfoKeeper.activeHeroList[1]);
        SetHeroButtonText(heroButtonTwo, globalInfoKeeper.activeHeroList[2]);
        SetHeroButtonText(heroButtonThree, globalInfoKeeper.activeHeroList[3]);
        SetHeroButtonText(heroButtonFour, globalInfoKeeper.activeHeroList[4]);

        SetArtifactButtonText(artifactButtonOne, globalInfoKeeper.activeHeroList[1].artifactOne);
        SetArtifactButtonText(artifactButtonTwo, globalInfoKeeper.activeHeroList[1].artifactTwo);
        SetArtifactButtonText(artifactButtonThree, globalInfoKeeper.activeHeroList[1].artifactThree);

        SetArtifactButtonText(artifactButtonFour, globalInfoKeeper.activeHeroList[2].artifactOne);
        SetArtifactButtonText(artifactButtonFive, globalInfoKeeper.activeHeroList[2].artifactTwo);
        SetArtifactButtonText(artifactButtonSix, globalInfoKeeper.activeHeroList[2].artifactThree);

        SetArtifactButtonText(artifactButtonSeven, globalInfoKeeper.activeHeroList[3].artifactOne);
        SetArtifactButtonText(artifactButtonEight, globalInfoKeeper.activeHeroList[3].artifactTwo);
        SetArtifactButtonText(artifactButtonNine, globalInfoKeeper.activeHeroList[3].artifactThree);

        SetArtifactButtonText(artifactButtonTen, globalInfoKeeper.activeHeroList[4].artifactOne);
        SetArtifactButtonText(artifactButtonEleven, globalInfoKeeper.activeHeroList[4].artifactTwo);
        SetArtifactButtonText(artifactButtonTwelve, globalInfoKeeper.activeHeroList[4].artifactThree);

    } //end UpdateButtonTexts()


    void SetHeroButtonText (Button button, Hero hero) {
        if (hero != null) {
            button.GetComponentInChildren<Text>().text = hero.heroName;
        }
    } //end SetHeroButtonText(2)


    void SetArtifactButtonText (Button button, Artifact artifact) {
        if (artifact != null) {
            button.GetComponentInChildren<Text>().text = artifact.artifactName;
        }
    } //end SetArtifactButtonText(2)



} //end ControlManager class

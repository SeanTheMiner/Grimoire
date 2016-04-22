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

        SetHeroButtonText(heroButtonOne, globalInfoKeeper.heroOne);
        SetHeroButtonText(heroButtonTwo, globalInfoKeeper.heroTwo);
        SetHeroButtonText(heroButtonThree, globalInfoKeeper.heroThree);
        SetHeroButtonText(heroButtonFour, globalInfoKeeper.heroFour);

        SetArtifactButtonText(artifactButtonOne, globalInfoKeeper.heroOne.artifactOne);
        SetArtifactButtonText(artifactButtonTwo, globalInfoKeeper.heroOne.artifactTwo);
        SetArtifactButtonText(artifactButtonThree, globalInfoKeeper.heroOne.artifactThree);

        SetArtifactButtonText(artifactButtonFour, globalInfoKeeper.heroTwo.artifactOne);
        SetArtifactButtonText(artifactButtonFive, globalInfoKeeper.heroTwo.artifactTwo);
        SetArtifactButtonText(artifactButtonSix, globalInfoKeeper.heroTwo.artifactThree);

        SetArtifactButtonText(artifactButtonSeven, globalInfoKeeper.heroThree.artifactOne);
        SetArtifactButtonText(artifactButtonEight, globalInfoKeeper.heroThree.artifactTwo);
        SetArtifactButtonText(artifactButtonNine, globalInfoKeeper.heroThree.artifactThree);

        SetArtifactButtonText(artifactButtonTen, globalInfoKeeper.heroFour.artifactOne);
        SetArtifactButtonText(artifactButtonEleven, globalInfoKeeper.heroFour.artifactTwo);
        SetArtifactButtonText(artifactButtonTwelve, globalInfoKeeper.heroFour.artifactThree);

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

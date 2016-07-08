using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Heroes;

public class GlobalInfoKeeper : MonoBehaviour {

    public int partyLevel, partyCount, partyExperience,
        playerGold;
    
    public List<Hero> availableHeroList = new List<Hero>();
    public List<Hero> activeHeroList = new List<Hero>();
    
	void Awake () {
        DontDestroyOnLoad(this);
	}
	





    /*

    public void MakeHeroActive(Hero heroToActivate, int ListPosition) {
        
        if (activeHeroSlot != null) {
            activeHeroSlot = heroToActivate;
            activeHeroList.Add()
        }

    } //End MakeHeroActive(2)

    */



} //end GlobalInfoKeeper class
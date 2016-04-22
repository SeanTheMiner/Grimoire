using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Heroes;

public class GlobalInfoKeeper : MonoBehaviour {

    public int partyLevel;

    public Hero heroOne;
    public Hero heroTwo;
    public Hero heroThree;
    public Hero heroFour;

    public List<Hero> heroList = new List<Hero>();
    
	void Awake () {
        DontDestroyOnLoad(this);
	}
	
} //end GlobalInfoKeeper class

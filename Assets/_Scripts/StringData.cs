using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Heroes;

namespace StringData {

    public class PrefabIDCodex {

        public Dictionary<int, string> heroDictionary = new Dictionary<int, string>() {

            {1, "1PunchieMcGeePrefab"},
            {2, "2FireMagePrefab"},
            {3, "3ChampionPrefab"},
            {4, "4MyshkaPrefab"}
            
        };

        public Dictionary<int, string> enemyDictionary = new Dictionary<int, string>() {

            {1, "BlueEnemyPrefab"},
            {2, "RedEnemyPrefab"}

        };
       

    } // end PrefabIDCodex class

} // end StringData namespace
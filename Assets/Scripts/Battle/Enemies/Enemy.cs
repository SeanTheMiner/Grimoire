using UnityEngine;
using System.Collections.Generic;
using Abilities;
using BattleObjects;

namespace Enemies {

    public class Enemy : BattleObject {

        public string enemyName {
            get; set;
        }
        
        public Vector3 battlePosition {
            get; set;
        }
        
        public Biome homeBiome {
            get; set;
        }

        public enum Biome {
            RedBiome,
            BlueBiome
        }


    } //end Enemy class

} //end Enemies namespace
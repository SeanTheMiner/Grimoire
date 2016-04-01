using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Enemies;

namespace Biomes {

    public class Biome : MonoBehaviour
    {
        public List<Enemy> enemyPool = new List<Enemy>();
        public int enemySpawnCap;

        public List<List<Vector3>> metaListToUse = new List<List<Vector3>>();
        public List<Vector3> positionListToUse = new List<Vector3>();
    }
}

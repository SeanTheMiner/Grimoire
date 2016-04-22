using UnityEngine;
using System.Collections;
using Heroes;

namespace Artifacts {

    public class Artifact {

        public string artifactName {
            get; set;
        }

        public virtual void ApplyArtifactEffect(Hero hero) {
            //Whatever it does goes in here.
        }

    } //end Artifact class

} //end Artifact namespace
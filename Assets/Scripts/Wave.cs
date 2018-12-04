using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave")]
public class Wave : ScriptableObject {

    [SerializeField] List<Formation> allFormations;

    public List<Formation> GetFormations() {

        return allFormations;
    }
}

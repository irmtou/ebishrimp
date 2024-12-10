using System.Collections.Generic;
using UnityEngine;

public class ShrimpManager : MonoBehaviour {
    public List<GameObject> shrimpTroupe = new List<GameObject>(); // All active shrimp in the troupe
    public int totalCookedShrimp = 0; // Total number of cooked shrimp

    public void AddShrimpToTroupe(GameObject shrimp) {
        if(!shrimpTroupe.Contains(shrimp)) {
            shrimpTroupe.Add(shrimp);
            if(GameManager.Instance!=null)
            {
                GameManager.Instance.changeShrimpCount(shrimpTroupe.Count);
            }
            Debug.Log($"Shrimp added. Troupe size: {shrimpTroupe.Count}");
        }
    }

    public void RemoveShrimpFromTroupe(GameObject shrimp) {
        if (shrimpTroupe.Contains(shrimp)) {
            shrimpTroupe.Remove(shrimp);
            if(GameManager.Instance!=null)
            {
                GameManager.Instance.changeShrimpCount(shrimpTroupe.Count);
            }
            Debug.Log($"Shrimp removed. Troupe size: {shrimpTroupe.Count}");
        }
    }

    public void IncrementCookedShrimpCount(int count) {
        totalCookedShrimp += count;
        if(GameManager.Instance!=null)
        {
            GameManager.Instance.changeCookedShrimpCount(totalCookedShrimp);
        }
        Debug.Log($"Total Cooked Shrimp: {totalCookedShrimp}");
    }
}

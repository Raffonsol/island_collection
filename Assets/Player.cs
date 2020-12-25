using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    #region "Singleton"

    private static Player _instance;
    public static Player Instance{
        get {
            if (_instance == null) {
                Debug.LogError("You have not instantiated the player in the scene, but you are trying to access it");
                return null;
            }
            return _instance;
        }
    }
   
   private void Awake() {
       _instance = this;
   }
   #endregion

    [HideInInspector]
    public Inventory inventory;

    private void Start() {
        inventory = new Inventory();
        inventory.items.Add(GameConstants.Instance.existingItems.Find(a => a.name == "stick"));
        inventory.items.Add(GameConstants.Instance.existingItems.Find(a => a.name == "stick"));
    }
}


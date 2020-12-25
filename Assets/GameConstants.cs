using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct  Item {
        public string name;
        public float weight;
        public Sprite icon;
}

public class Inventory {
    public float totalSpace;
    public List<Item> items;

    public Inventory() {
        totalSpace = GameConstants.Instance.pocketSpace;
        items = new List<Item>();
    }
}

public class GameConstants : MonoBehaviour
{
    #region "Singleton"

    private static GameConstants _instance;
    public static GameConstants Instance {
        get {
            if (_instance == null) {
                Debug.LogError("You have not instantiated the GameConstants in the scene, but you are trying to access it");
                return null;
            }
            return _instance;
        }
    }
   
   private void Awake() {
       _instance = this;
   }
   #endregion

    public float pocketSpace = 10f;

    [SerializeField]
    public List<Item> existingItems;
    

}

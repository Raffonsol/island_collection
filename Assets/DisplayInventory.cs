using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{

    float refreshTimer = 0f;

    float lastObj = -40f;

    public float refreshRate = 10f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       refreshTimer += Time.deltaTime;
       if (refreshTimer > refreshRate) {
           refreshInvetory();
           refreshTimer = 0;
       }
    }

    void refreshInvetory() {
         foreach (Transform child in transform){
            Destroy(child.gameObject);
            lastObj--;
        }
        for (int i = 0; i < Player.Instance.inventory.items.Count; i++)
        {
            Debug.Log(Player.Instance.inventory.items[i].name);
        
            GameObject imgObject = new GameObject(Player.Instance.inventory.items[i].name);
            RectTransform trans = imgObject.AddComponent<RectTransform>();
            Image image = imgObject.AddComponent<Image>();
            trans.transform.SetParent(transform); // setting parent
            trans.localScale = new Vector3(1, 1, 1);;
            trans.anchoredPosition = new Vector2(lastObj, 0f); // setting position, will be on center
            lastObj++;
            trans.sizeDelta= new Vector2(1, 12); // custom size

            image.sprite = Player.Instance.inventory.items[i].icon;

        }
    }
}

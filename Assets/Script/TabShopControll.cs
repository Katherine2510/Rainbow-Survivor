using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabShopControll : MonoBehaviour
{
    public GameObject HatShop;
    public GameObject BoxShop;
    void Start()
    {
        HatShop.SetActive(true);
        BoxShop.SetActive(false);
        
    }
    void Update()
    {
    }
    public void OnHatShop() {
        HatShop.SetActive(true);
        BoxShop.SetActive(false);
    }
    public void OnBoxShop() {
        HatShop.SetActive(false);
        BoxShop.SetActive(true);
    }
}

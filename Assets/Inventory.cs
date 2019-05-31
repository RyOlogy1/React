using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] Slots;
    private GameObject inventoryPrefab;

    public Inventory(int slots) {
        Slots = new GameObject[slots];
        for (int i = 0; i < slots; i++) {

        }
    }

    public GameObject getSlot(int slot) {
        return Slots[slot];
    }
    public GameObject setSlot(int slot, GameObject go) {
        GameObject temp = Slots[slot];
        Slots[slot] = go;
        return temp;
    }
}

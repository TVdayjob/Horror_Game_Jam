using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Weapon> weapons;

    private void Start()
    {
        weapons = new List<Weapon>();
    }

    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }

    public Weapon GetWeapon(int index)
    {
        if (index >= 0 && index < weapons.Count)
        {
            return weapons[index];
        }
        return null;
    }

    public bool HasItem(string newItem)
    {
        //do stuff
        return true;
    }

    public int GetWeaponCount()
    {
        return weapons.Count;
    }
}

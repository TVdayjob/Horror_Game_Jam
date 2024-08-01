using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Weapon> weapons;
    public Weapon SelectedWeapon;

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
            SelectedWeapon = weapons[index];
            return SelectedWeapon;
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

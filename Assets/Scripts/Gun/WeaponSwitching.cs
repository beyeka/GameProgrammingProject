using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{

    public int selectedWeapon = 0;
    
    void Start()
    {
        SelectWeapon();
        
    }

    // Update is called once per frame
    void Update()
    {
        int previousWeapon = selectedWeapon;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
        }

        if (previousWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int weaponNumber = 0;

        foreach (Transform weapon in transform)
        {
            if (weaponNumber == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            weaponNumber++;
        }
        
    }
}

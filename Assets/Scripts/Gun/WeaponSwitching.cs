// Handles weapon switching between multiple guns using number keys. Activates selected gun and deactivates the previous one.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponSwitching : MonoBehaviour
{
    private Gun _selectedGun;

    [SerializeField] private List<Gun> guns;

    private bool _isActive;

    // Listens for input to switch weapons. Updates active gun if selection changes.
    public void Update()
    {
        if (!_isActive)
            return;

        var previousGun = _selectedGun;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _selectedGun = guns[0];
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _selectedGun = guns[1];
        }

        if (previousGun != _selectedGun)
        {
            previousGun.Deactivate();
            previousGun.gameObject.SetActive(false);

            _selectedGun.Activate();
            _selectedGun.gameObject.SetActive(true);
        }
    }

    // Initializes with the first gun and enables switching.
    public void StartGameplay()
    {
        _selectedGun = guns[0];
        _selectedGun.Activate();
        _selectedGun.gameObject.SetActive(true);

        _isActive = true;
    }

    // Disables weapon switching input.
    public void FinishGameplay()
    {
        _isActive = false;
    }
}
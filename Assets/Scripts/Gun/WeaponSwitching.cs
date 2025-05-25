using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponSwitching : MonoBehaviour
{
    private Gun _selectedGun;

    [SerializeField] private List<Gun> guns;

    private bool _isActive;

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

    public void StartGameplay()
    {
        _selectedGun = guns[0];
        _selectedGun.Activate();
        _selectedGun.gameObject.SetActive(true);

        _isActive = true;
    }

    public void FinishGameplay()
    {
        _isActive = false;
    }
}
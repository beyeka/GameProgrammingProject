// Handles gun shooting mechanics including ammo, fire rate, reloading, hit detection, SFX, and power-ups.
using System;
using UnityEngine;
using System.Collections;
using TMPro;

public class Gun : MonoBehaviour
{
    public SFXKeys sfxKeys;

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float nextTimeToFire = 0f;

    public int magazine;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReload = false;
    private bool infiniteAmmo = false;

    public Animator animator;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private bool _isInitialized;

    private bool _isActive;

    // Sets initial values and states for the gun. Called once on first Activate.
    private void Initialize()
    {
        isReload = false;
        animator.SetBool("Reloading", false);
        currentAmmo = magazine;

        _isInitialized = true;
    }

    // Prepares gun for gameplay and prints current ammo.
    public void Activate()
    {
        if (!_isInitialized)
            Initialize();

        AmmoPrinter();

        _isActive = true;
    }

    // Disables gun logic, stops reload coroutine, resets states.
    public void Deactivate()
    {
        _isActive = false;

        StopCoroutine(Reload());
        animator.SetBool("Reloading", false);
        
        isReload = false;
    }

    // Main update loop: handles input, shooting logic, and reload triggers.
    public void CustomUpdate()
    {
        if (!_isActive)
            return;

        if (isReload)
        {
            return;
        }

        if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            currentAmmo = 0;
            AmmoPrinter();
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    // Plays shoot effects, handles ammo, raycasts for hit detection, and applies damage to enemies.
    void Shoot()
    {
        PlayShootSound();

        muzzleFlash.Play();

        var shouldDecreaseAmmo = true;

        if (GameManager.IsGodModeActive)
        {
            shouldDecreaseAmmo = false;
        }
        else if (infiniteAmmo)
        {
            shouldDecreaseAmmo = false;
        }

        if (shouldDecreaseAmmo)
            currentAmmo--;

        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            EnemyBase enemy = hit.collider.GetComponent<EnemyBase>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            GameObject destroyEffect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(destroyEffect, 0.3f);
        }

        AmmoPrinter();
    }


    // Updates ammo count on the UI.
    public void AmmoPrinter()
    {
        UIManager.Instance.gameplayUI.SetAmmoText(currentAmmo + "/" + magazine);
    }

    // Plays reload animation, waits, then refills ammo.
    IEnumerator Reload()
    {
        isReload = true;

        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - 0.25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        currentAmmo = magazine;
        isReload = false;

        AmmoPrinter();
    }

    // Temporarily boosts fire rate.
    public void ApplyFireRateBoost(float multiplier, float duration)
    {
        StartCoroutine(FireRateBoostRoutine(multiplier, duration));
    }

    // Coroutine for applying and then resetting fire rate boost.
    private IEnumerator FireRateBoostRoutine(float multiplier, float duration)
    {
        fireRate *= multiplier;
        yield return new WaitForSeconds(duration);
        fireRate /= multiplier;
    }

    // Temporarily enables infinite ammo.
    public void EnableInfiniteAmmo(float duration)
    {
        StartCoroutine(InfiniteAmmoRoutine(duration));
    }

    // Coroutine to toggle infinite ammo state for a duration.
    private IEnumerator InfiniteAmmoRoutine(float duration)
    {
        infiniteAmmo = true;
        yield return new WaitForSeconds(duration);
        infiniteAmmo = false;
    }

    // Plays gun fire SFX using the SoundManager.
    public void PlayShootSound()
    {
        SoundManager.Instance.PlaySound(sfxKeys);
    }
}
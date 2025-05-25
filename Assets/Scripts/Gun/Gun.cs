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

    private void Initialize()
    {
        isReload = false;
        animator.SetBool("Reloading", false);
        currentAmmo = magazine;

        _isInitialized = true;
    }

    public void Activate()
    {
        if (!_isInitialized)
            Initialize();

        AmmoPrinter();

        _isActive = true;
    }

    public void Deactivate()
    {
        _isActive = false;

        StopCoroutine(Reload());
        animator.SetBool("Reloading", false);
        
        isReload = false;
    }

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


    public void AmmoPrinter()
    {
        UIManager.Instance.gameplayUI.SetAmmoText(currentAmmo + "/" + magazine);
    }

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

    public void ApplyFireRateBoost(float multiplier, float duration)
    {
        StartCoroutine(FireRateBoostRoutine(multiplier, duration));
    }

    private IEnumerator FireRateBoostRoutine(float multiplier, float duration)
    {
        fireRate *= multiplier;
        yield return new WaitForSeconds(duration);
        fireRate /= multiplier;
    }

    public void EnableInfiniteAmmo(float duration)
    {
        StartCoroutine(InfiniteAmmoRoutine(duration));
    }

    private IEnumerator InfiniteAmmoRoutine(float duration)
    {
        infiniteAmmo = true;
        yield return new WaitForSeconds(duration);
        infiniteAmmo = false;
    }

    public void PlayShootSound()
    {
        SoundManager.Instance.PlaySound(sfxKeys);
    }
}
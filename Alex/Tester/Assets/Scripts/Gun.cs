using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject cam;
    private ParticleSystem muzzleFlash;

    public bool automatic = false;
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public int maxAmmo = 30;
    public int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    private float nextTimeToFire = 0f;
    private bool inputType;

    public Animator animator;

    void Start()
    {
        muzzleFlash = transform.Find("muzzleFlash").GetComponent<ParticleSystem>();
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isReloading)
        {
            return;
        }

        if(currentAmmo < 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if(automatic)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)) 
        {
            Debug.Log(hit.transform.name);
        }
    }

    IEnumerator Reload()
    {
        animator.SetBool("Reloading", true);
        isReloading = true;
        Debug.Log("Reloading...");
        

        yield return new WaitForSeconds(reloadTime - 0.25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }
}

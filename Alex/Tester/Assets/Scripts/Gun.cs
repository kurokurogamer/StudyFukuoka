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

    [SerializeField]
    private float recoil = 0;
    [SerializeField]
    private float recoilForce = 0.3f;

    private float nextTimeToFire = 0f;
    private bool inputType;

    public Animator animator;

    private Vector3 defaultPosition;

    void Start()
    {
        muzzleFlash = transform.Find("muzzleFlash").GetComponent<ParticleSystem>();
        currentAmmo = maxAmmo;
        defaultPosition = transform.position;
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

        Vector3 RecoilPosition = new Vector3(defaultPosition.x, defaultPosition.y, defaultPosition.z - 0.5f);

        transform.position = Vector3.Lerp(defaultPosition, RecoilPosition, recoil);
        recoil -= Time.deltaTime;
        recoil = Mathf.Clamp(recoil, 0, 1f);
    }

    void Shoot()
    {
        recoil += recoilForce;
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

using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponHandler : MonoBehaviour
{
    public Animator animator;
    public GameObject fireSound;

    RaycastHit hit;
    public GameObject firePrefab;
    public GameObject StonebulletHole;
    public GameObject MetalbulletHole;
    public GameObject WoodbulletHole;
    public GameObject EnemybulletHole;
    public GameObject Bullet;
    public Text AmmoCapecityText;

    [SerializeField]
    public EnemyAttack Enemy;
    [SerializeField]
    public Camera MainCam;
    [SerializeField]
    public Camera ADS_CAm;

    public float damage = 20;

    public int amoCapecity = 24;
    public int magazine = 7;
    public int currentAmo = 7;
    public float fireRate = 10;

    float lastFire;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            MainCam.enabled = false;
            ADS_CAm.enabled = true;
        }
        else
        {
            MainCam.enabled = true;
            ADS_CAm.enabled = false;

        }

        // Check Gun Ammo Animation
        if (Input.GetKeyDown(KeyCode.T))
        {
            animator.SetTrigger("CheckGunAmmo");
        }

        // Handel Fire Animation When Left Mouse Clicked
        if (Input.GetMouseButton(0) && currentAmo > 0 && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Animation"))
        {
            if (Time.time - lastFire > 1 / fireRate)
            {
                currentAmo--;
                var firePrefabObj = Instantiate(firePrefab, Bullet.transform.position, Bullet.transform.rotation);
                if (Physics.Raycast(Bullet.transform.position, Bullet.transform.up, out hit, 100f))
                {
                    // Place Particle
                    if (hit.transform.tag == "Metal")
                        Instantiate(MetalbulletHole, hit.point, Quaternion.LookRotation(hit.normal));
                    else if (hit.transform.tag == "Wood")
                        Instantiate(WoodbulletHole, hit.point, Quaternion.LookRotation(hit.normal));
                    else if (hit.transform.tag == "Enemy")
                    {
                        GameObject enemyHit = Instantiate(EnemybulletHole, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(enemyHit, .5f);
                    }
                    else
                        Instantiate(StonebulletHole, hit.point, Quaternion.LookRotation(hit.normal));

                    // Handel Enemey Hit
                    var enemyHealth = hit.transform?.parent.GetComponent<EnemyHealth>();
                    var child = hit.transform?.parent.GetComponent<ChildEnemyHealth>();

                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(damage);
                    }

                    if (child != null)
                    {
                        child.TakeDamage(damage);
                    }

                }
                Destroy(firePrefabObj, .5f);

                //animator.SetTrigger("Fire");
                var fireObj = Instantiate(fireSound, transform.position, transform.rotation);
                Destroy(fireObj, 1f);
                lastFire = Time.time;
            }
        }

        // Handel Reload Animation When R Pressed
        if (Input.GetKeyDown(KeyCode.R) && currentAmo >= 0 && currentAmo != magazine && amoCapecity > 0)
        {
            Reload();
            animator.SetTrigger("Reload");
        }
        AmmoCapecityText.text = currentAmo + "/" + amoCapecity;

    }

    void Reload()
    {
        int temp = magazine - currentAmo;
        if (amoCapecity >= temp)
        {
            currentAmo += temp;
            amoCapecity -= temp;
        }
        else
        {
            currentAmo += amoCapecity;
            amoCapecity = 0;
        }
    }
}

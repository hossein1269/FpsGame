using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    public Slider Slider;
    public float Health = 100;

    void Update()
    {
        Slider.value = Health;
        if(Health <= 0)
        {
            SceneManager.LoadSceneAsync(0);
        }

    }


    public void TakeDamage(float damage)
    {
        if(damage > Health)
        {
            Health = 0;
        }
        else
            Health -= damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyHands")
        {
            if (other.transform.root.tag == "Child")
            {
                TakeDamage(2f);
            }
            else
            {
                TakeDamage(10f);
            }
        }
    }
}

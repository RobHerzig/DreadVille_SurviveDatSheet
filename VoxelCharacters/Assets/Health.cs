using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{

    public int maxHealth = 100;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth;

    private bool shouldDie = false;
    public bool isDead = false;

    public delegate void DieDelegate();
    public event DieDelegate EventDie;

    public delegate void RespawnDelegate();
    public event RespawnDelegate EventRespawn;

    private Text healthText;

    public RectTransform healthBar;

    HealthViewControl healthView;

    private void Start()
    {
        //healthView = GetComponent<HealthViewControl>();
        currentHealth = maxHealth;
        if (healthBar == null)
            healthBar = GameObject.Find("HealthBarForeground").GetComponent<RectTransform>();
        //healthText = GameObject.Find("HealthText").GetComponent<Text>();
        //setHealthText(currentHealth);
    }

    private void Update()
    {

        CheckCondition();

        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TakeDamage(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(GameObject.FindGameObjectWithTag("Other_Player") != null)
                GameObject.FindGameObjectWithTag("Other_Player").GetComponent<Health>().TakeDamage(6);
        }
    }

    void CheckCondition()
    {
        if(currentHealth >= 0 && !shouldDie && !isDead)
        {
            shouldDie = true;
        }

        if(currentHealth <= 0 && shouldDie)
        {
            if(EventDie != null)
            {
                Debug.Log("TRIGGER EVENT DIE");
                EventDie();
            }
            shouldDie = false;
        }

        if(currentHealth > 0 && isDead)
        {
            if(EventRespawn != null)
            {
                EventRespawn();
            }
            isDead = false;
        }
    }

    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        currentHealth -= amount;
        //setHealthText(currentHealth);

        Debug.LogWarning("REDUCING HP BY " + amount + " ON " + gameObject.name + " / " + gameObject.tag);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //isDead = true;
            Debug.Log("Dead!");
        } else
        {
            //isDead = false;
        }
    }

    void setHealthText(int newHealth)
    {
        if (isLocalPlayer)
            healthText.text = newHealth.ToString() + "HP";
    }

    void setHealthBar(float newRatio)
    {
        if(isLocalPlayer || gameObject.tag == "Enemy")
        {
            float ratioNew = (float)currentHealth / (float)maxHealth;
            healthBar.localScale = new Vector3(ratioNew, healthBar.localScale.y, healthBar.localScale.z);

            //healthBar.localScale = new Vector3(newRatio, healthBar.localScale.y, healthBar.localScale.z);
        }
    }

    void OnChangeHealth(int health)
    {
        currentHealth = health;
        //setHealthText(health);

        float newRatio = (float)health / (float)maxHealth;
        setHealthBar(newRatio);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
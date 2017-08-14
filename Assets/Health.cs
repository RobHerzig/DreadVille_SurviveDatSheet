using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{

	float maxHealth = 100;

    [SyncVar(hook = "OnChangeHealth")]
	public float currentHealth;

    private bool shouldDie = false;
    public bool isDead = false;

    public delegate void DieDelegate();
    public event DieDelegate EventDie;

    public delegate void RespawnDelegate();
    public event RespawnDelegate EventRespawn;

    private Text healthText;

    public RectTransform healthBar;

    HealthViewControl healthView;
	Player player;
    private void Start()
    {
		player= gameObject.GetComponent<Player> ();
		maxHealth = player.MaxHealth;
        //healthView = GetComponent<HealthViewControl>();
        currentHealth = maxHealth;
        if (healthBar == null)
            healthBar = GameObject.Find("HealthBarForeground").GetComponent<RectTransform>();
		StartCoroutine (RegenerateHealthRoutine ());
    }

    private void Update()
    {
		
        CheckCondition();

        if (!isLocalPlayer)
            return;
		maxHealth = player.MaxHealth;

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
	IEnumerator RegenerateHealthRoutine(){
		while (true) {
			yield return new WaitForSeconds (1);
			if(!isDead&& currentHealth< maxHealth)
				currentHealth += player.healthRegeneration;
		}
	}
	public void TakeDamage(float amount)
    {
        if (!isServer)
            return;

		currentHealth -= amount* (1-player.armor);
        //setHealthText(currentHealth);


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
	public void TakeHeal(float amount)
	{
		if (!isServer)
			return;

		currentHealth += amount;
		//setHealthText(currentHealth);

		Debug.LogWarning("INCREASE HP BY " + amount + " ON " + gameObject.name + " / " + gameObject.tag);

		if (currentHealth >= maxHealth)
		{
			currentHealth = maxHealth;

		
		} else
		{
			
		}
	}

	void setHealthText(float newHealth)
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

	void OnChangeHealth(float health)
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
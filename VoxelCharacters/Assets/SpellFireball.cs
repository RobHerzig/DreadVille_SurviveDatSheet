using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFireball : MonoBehaviour {

    public GameObject FireBall;
    public int Power = 15;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F))
        {
            throwFireBall();
        }
	}

    public void throwFireBall()
    {
        StartCoroutine(throwCoroutine(10f, 1.5f));

    }

    IEnumerator throwCoroutine(float distance, float time)
    {
        GameObject instance = Instantiate(FireBall, transform.position + transform.up * 0.4f, Quaternion.identity) as GameObject;
        FireBall ballScript = instance.GetComponent<FireBall>();
        ballScript.initDamage(Power);

        float elapsedTime = 0;
        Vector3 startingPos = instance.transform.position;
        Vector3 targetPos = instance.transform.position + transform.forward * distance;
        instance.GetComponent<AudioSource>().Play();

        while (elapsedTime < time && !ballScript.GetTriggered())
        {
            instance.transform.position = Vector3.Lerp(startingPos, targetPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        instance.GetComponent<ParticleSystem>().enableEmission = false;
        Destroy(instance, 2f);
    }
}

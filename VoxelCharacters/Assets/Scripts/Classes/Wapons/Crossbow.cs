using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Crossbow : Wapon {

	public Crossbow (WaponsModel model)
	{
		this.model = model;
		prefab = model.crossbow;
	}
    public GameObject Bolt;
    public Transform TargetCenter, TargetLeft, TargetRight;
	// Use this for initialization
	void Start () {
        if (Bolt == null || TargetCenter == null || TargetLeft == null || TargetRight == null)
            Debug.LogError("Crossbow missing Configs");
	}
	
	// Update is called once per frame
	void Update () {
        bool normalHit = CrossPlatformInputManager.GetButtonDown("NormalHit");
        if (normalHit)
            simpleHit();

        bool skill = CrossPlatformInputManager.GetButtonDown("SpecialHit");
        if (skill)
            specialHit();
    }

    void simpleHit()
    {
        GameObject projectile = Instantiate(Bolt, transform.position, Quaternion.identity) as GameObject;
        StartCoroutine ( Shoot(projectile, TargetCenter, 0.8f));
    }

    void specialHit()
    {
        GameObject projectileCenter = Instantiate(Bolt, transform.position, Quaternion.identity) as GameObject;
        StartCoroutine(Shoot(projectileCenter, TargetCenter, 0.8f));

        GameObject projectileLeft = Instantiate(Bolt, transform.position, Quaternion.identity) as GameObject;
        StartCoroutine(Shoot(projectileLeft, TargetLeft, 0.8f));

        GameObject projectileRight = Instantiate(Bolt, transform.position, Quaternion.identity) as GameObject;
        StartCoroutine(Shoot(projectileRight, TargetRight, 0.8f));

    }

    IEnumerator Shoot(GameObject projectile, Transform targetDestination, float time)
    {
        projectile.transform.LookAt(targetDestination);
        Vector3 start = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime<time)
        {
            projectile.transform.position = Vector3.Lerp(start, targetDestination.position, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        GameObject.DestroyImmediate(projectile);
    }
}


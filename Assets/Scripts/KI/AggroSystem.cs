using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
public class AggroSystem : MonoBehaviour {

	private List<AggroTarget> aggroList;

	AggroTarget currentTarget;
	internal GameObject Target;

	// Use this for initialization
	void Start () {
		aggroList = new List<AggroTarget> ();
	}


	public void SetAggro(NetworkInstanceId id,float aggro){
		if(!ContainsInList(id))
			aggroList.Add(new AggroTarget(id,aggro));
		aggroList = aggroList.OrderBy (t => t.value).ToList ();
		aggroList.Reverse ();
		AggroTarget nextTarget = aggroList [0];
		if (currentTarget == null || nextTarget.value > currentTarget.value * 1.3f)
			currentTarget = nextTarget;
		Target = NetworkServer.FindLocalObject(currentTarget.ID);
		Debug.Log (currentTarget.ID);
	}


	bool ContainsInList(NetworkInstanceId id){
		foreach (AggroTarget target in aggroList) {
			if (target.ID == id)
				return true;
		}
			return false;
	}

	// Update is called once per frame
	void Update () {
		
	}
}


//object that contains a id and a aggro value
public class AggroTarget {
	public NetworkInstanceId ID;
	public float value;

	public AggroTarget(NetworkInstanceId id,float aggroValue){
		ID = id;
		value = aggroValue;
	}



}
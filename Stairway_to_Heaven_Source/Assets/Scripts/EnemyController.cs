using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float speed;
	GameObject player;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		if (player) {
			Vector3 dir = (player.transform.position - transform.position).normalized;
			transform.position += dir * speed * Time.deltaTime;
		}
	}
}
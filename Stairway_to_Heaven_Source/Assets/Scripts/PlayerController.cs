using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Camera camera;
	public float speed = 15f;
	public int health = 100;
	public int damage = 20;
	public GUIText HP;
	public GUIText GameOver;
	public GameObject fragments;
	Rigidbody player_rigidbody;
	Vector3 movement;
	Vector3 worldPos;
	float mouseX;
	float mouseY;
	float cam_offset;
	public GameObject enemy;
	bool spawning;
	float timer;
	public float timeToSpawn;
	public GameObject laser;
	Color original;


	void Awake(){
		player_rigidbody = GetComponent<Rigidbody> ();
		DispHP ();
		cam_offset = camera.transform.position.y - transform.position.y;
		timer = 0.0f;
		spawning = false;
		original = transform.renderer.material.GetColor ("_Color");
	}


	void FixedUpdate () {
		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");
		Move (horizontal, vertical);
		Turn ();
		if (!spawning) {
			timer += Time.deltaTime;
		}
		
		// Every time the interval is reached, Spawn is called
		if (timer >= timeToSpawn) {
			Spawn();
		}
		
		transform.renderer.material.color = original;
		
		if (Input.GetMouseButtonDown (0)){
			Shoot ();		
		}
	}

	void Move(float h, float v){
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		player_rigidbody.MovePosition (transform.position + movement);
	}

	void Turn(){
		mouseX = Input.mousePosition.x;
		mouseY = Input.mousePosition.y;
		worldPos = camera.ScreenToWorldPoint (new Vector3 (mouseX, mouseY, cam_offset));
		Vector3 lookDir = new Vector3 (worldPos.x, transform.position.y, worldPos.z);
		transform.LookAt (lookDir);
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Enemy") {
			other.gameObject.SetActive (false);
			transform.renderer.material.color = Color.red;
			health -= damage;
			DispHP ();
			if (health <= 0) {
				Destroy (gameObject);
				Instantiate(fragments, transform.position - new Vector3(.333333f, 0.0f, .333333f), transform.rotation);
				GameOver.text = "Game Over";
			}
		}
	}

	void DispHP(){
		if(health < 0){
			HP.text = "Health: 0";
		}
		else{
			HP.text = "Health: " + health.ToString();
		}
	}

	void Spawn(){
		spawning = true;
		timer = 0.0f;
		
		float random_horiz = Random.Range (-94, 94);
		float random_vert = Random.Range (-39,79 );
		
		Instantiate (enemy, new Vector3 (random_horiz, 0.25f, random_vert), Quaternion.identity);
		
		spawning = false;
	}
	
	void Shoot(){
		Instantiate (laser, transform.position, transform.rotation);
	}

}

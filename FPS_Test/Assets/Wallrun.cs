using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Wallrun : MonoBehaviour
{

	private bool WallR = false;
	private bool WallL = false;
	private RaycastHit hitR;
	private RaycastHit hitL;
	private int jumpCount = 0;
	private RigidbodyFirstPersonController cc;
	private Rigidbody rb;

	// Use this for initialization
	void Start ()
	{
		cc = GetComponent<RigidbodyFirstPersonController> ();
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (cc.Grounded) {
			jumpCount = 0;
		}
		if (Input.GetKeyDown (KeyCode.Space) && cc.Grounded && jumpCount <= 1) {
			if (Physics.Raycast (transform.position, transform.right, out hitR, 1)) {
				if (hitR.transform.tag == "wall") {
					WallR = true;
					WallL = false;
					jumpCount += 1;		
					rb.useGravity = false;
					StartCoroutine (afterRun ());
				}
			}
		}
		if (Physics.Raycast (transform.position, -transform.right, out hitL, 1)) {
			if (hitL.transform.tag == "wall") {
				WallL = true;
				WallR = false;
				jumpCount += 1;		
				rb.useGravity = false;
				StartCoroutine (afterRun ());
			}
		}
	}

	IEnumerator afterRun ()
	{
		yield return new WaitForSeconds (0.5f);
		WallL = false;
		WallR = false;
		rb.useGravity = true;

	}
}

//130, 0, 300
//-32, 32, 302.88
//-54, 85.9, 371.8
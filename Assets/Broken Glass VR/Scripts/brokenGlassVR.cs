using UnityEngine;
using System.Collections;

public class brokenGlassVR : MonoBehaviour {
	private Material _glassMat;
	private bool _cracked;
	[SerializeField]private float _distortion_before;
	private float _distortion;
	public float _repairSpeed;

	void Start () {
		_glassMat = GetComponent<MeshRenderer> ().material;
	}
	

	void Update () {
		_glassMat.SetFloat ("_BumpAmt", _distortion);
		if (_cracked) {
		
			_distortion = Mathf.MoveTowards (_distortion, 0f, _repairSpeed * Time.unscaledDeltaTime);
			if (_distortion == 0f) {
			
				_cracked = false;
			
			}
		}

        if (Input.GetKeyDown(KeyCode.G))
        {
			GlassShatter();

		}

	}

	public void GlassShatter(){
	
		_distortion = _distortion_before;
		_cracked = true;
	
	}

}

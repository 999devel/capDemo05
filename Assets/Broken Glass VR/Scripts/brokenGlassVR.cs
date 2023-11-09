using UnityEngine;
using System.Collections;

public class brokenGlassVR : MonoBehaviour {
	private Material _glassMat;
	private bool _cracked;
	[SerializeField]private float _distortion_before;
	private float _distortion;
	public float _repairSpeed;

	[HideInInspector]public bool isGetGlassObj;

	public GameObject[] questObj_Text_CanSeeIfGetGlassObj;

	void Start () {
		_glassMat = GetComponent<MeshRenderer> ().material;
	}
	

	void Update () {
		_glassMat.SetFloat ("_BumpAmt", _distortion);
		if (_cracked) {
		
			_distortion = Mathf.MoveTowards (_distortion, 0f, _repairSpeed * Time.unscaledDeltaTime);
			//if (_distortion == 0f) {
			
			//	_cracked = false;
			
			//}
		}

        if (Input.GetKeyDown(KeyCode.G))
		{
			isGetGlassObj = !isGetGlassObj;
			// 플레이어가 glass 오브젝트를 든 상태에 따라 볼 수 있는 오브젝트의 활성화/비활성화 결정

			if (!isGetGlassObj)
            {
				_distortion = 0f;
            }
            else
            {
				GlassShatter();
			}

			for (int i=0; i<questObj_Text_CanSeeIfGetGlassObj.Length; i++)		
            {
				questObj_Text_CanSeeIfGetGlassObj[i].SetActive(isGetGlassObj);
            }

			
		}
	}

	public void GlassShatter(){
	
		_distortion = _distortion_before;
		_cracked = true;
	}

}

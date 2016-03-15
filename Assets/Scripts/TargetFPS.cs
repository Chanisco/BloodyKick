using UnityEngine;
using System.Collections;

public class TargetFPS : MonoBehaviour {
	[SerializeField] public int FTPTarget;
	void Start(){
		Application.targetFrameRate = FTPTarget;
	}
}

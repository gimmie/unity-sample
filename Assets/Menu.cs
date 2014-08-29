using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	void OnGUI() {
		GUI.Box(new Rect(10,10,300,700), "Gimmie Menu");
		
		if(GUI.Button(new Rect(20,40,260,150), "Show Gimmie")) {
			GimmieBinding.ShowGimmieRewardsCatalogue();
		}

		if(GUI.Button(new Rect(20,230,260,150), "Trigger Event")) {
			GimmieBinding.TriggerGimmieEvent("do_post");
		}
	}
	
	// Use this for initialization
	void Start () {
		GimmieBinding.InitGimmie();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void GimmieNeedLogin() {
		// Get facebook id and use it login to Gimmie
		GimmieBinding.Login("facebookid");
	}
}

using UnityEngine;
using System.Collections;
using Gimmie;

public class Menu : MonoBehaviour {
	
	void OnGUI() {
		GUI.Box(new Rect(10,10,300,700), "Gimmie Menu");
		
		if(GUI.Button(new Rect(20,40,260,150), "Show Gimmie")) {
			GimmieBinding.ShowGimmieRewardsCatalogue();
		}

		if(GUI.Button(new Rect(20,230,260,150), "Trigger Event")) {
			GimmieBinding.TriggerGimmieEvent("do_post");
		}

		if(GUI.Button(new Rect(20,420,260,150), "Get Profile")) {
			GimmieWrapper.InitGimmie("32e938c2621d94dfcfac6c41ca92", "5c192912598feafc0abacba5cd96");
			GimmieWrapper.Login("test01");
			JSONObject j = GimmieWrapper.CallGimmie("https://api.gimmieworld.com/1/profile.json");
			Debug.Log(j);
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

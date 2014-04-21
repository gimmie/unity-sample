using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class GimmieBinding : MonoBehaviour {
	#if UNITY_IPHONE
	[DllImport ("__Internal")]
	private static extern void AnonymousLogin();
	[DllImport ("__Internal")]
	private static extern bool NativeIOSIsAnonymous();
	[DllImport ("__Internal")]
	private static extern void Login(string username);
	[DllImport ("__Internal")]
	private static extern void Logout();
	[DllImport ("__Internal")]
	private static extern void UpdateGimmieCountry(string country);
	[DllImport ("__Internal")]
	private static extern void ShowGimmieRewards();
	[DllImport ("__Internal")]
	private static extern void BindGimmieNotification();
	[DllImport ("__Internal")]
	private static extern void TriggerEvent(string eventname);
	#endif

	public static void initGimmie(){
		#if UNITY_IPHONE
		Debug.Log("Login to gimmie");
		BindGimmieNotification();
		AnonymousLogin();
		#endif
		
		#if UNITY_ANDROID
		AndroidJNI.AttachCurrentThread();
		AndroidJavaClass player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = player.GetStatic<AndroidJavaObject>("currentActivity");
		
		AndroidJavaClass gimmie = new AndroidJavaClass("com.gimmie.Gimmie");
		AndroidJavaObject service = gimmie.CallStatic<AndroidJavaObject>("getInstance", activity);
		service.Call("updateContext", activity);
		service.Call("login");
		#endif
	}
	
	public static void Login(string user) {
		Debug.Log("Login to gimmie");

		#if UNITY_IPHONE
		Login(user);
		#endif
		
		#if UNITY_ANDROID
		AndroidJavaClass player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = player.GetStatic<AndroidJavaObject>("currentActivity");
		
		AndroidJavaClass gimmie = new AndroidJavaClass("com.gimmie.Gimmie");
		AndroidJavaObject service = gimmie.CallStatic<AndroidJavaObject>("getInstance", activity);
		service.Call ("loginAndTransferFromGuest", user, "", "");
		#endif
	}

	public static void Logout() {
		#if UNITY_IPHONE
		Logout();
		#endif

		#if UNITY_ANDROID
		AndroidJavaClass player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = player.GetStatic<AndroidJavaObject>("currentActivity");
		
		AndroidJavaClass gimmie = new AndroidJavaClass("com.gimmie.Gimmie");
		AndroidJavaObject service = gimmie.CallStatic<AndroidJavaObject>("getInstance", activity);
		service.Call ("logout");
		#endif
	}

	public static bool IsAnonymousUser() {
		#if UNITY_IPHONE
		return NativeIOSIsAnonymous();
		#endif

		#if UNITY_ANDROID
		AndroidJavaClass player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = player.GetStatic<AndroidJavaObject>("currentActivity");
		
		AndroidJavaClass gimmie = new AndroidJavaClass("com.gimmie.Gimmie");
		AndroidJavaObject service = gimmie.CallStatic<AndroidJavaObject>("getInstance", activity);
		String user = service.Call<String> ("getUser");
		return user.StartsWith("guest:", true, null);
		#endif
	}
	
	public static void UpdateCountry(string countryCode) {
		#if UNITY_IPHONE
		UpdateGimmieCountry(countryCode);
		#endif
		
		#if UNITY_ANDROID
		AndroidJavaClass player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = player.GetStatic<AndroidJavaObject>("currentActivity");
		
		AndroidJavaClass gimmie = new AndroidJavaClass("com.gimmie.Gimmie");
		AndroidJavaObject service = gimmie.CallStatic<AndroidJavaObject>("getInstance", activity);
		service.Call ("setCountry", countryCode);
		#endif
	}
	
	public static void ShowGimmieRewardsCatalogue(){
		#if UNITY_IPHONE
		ShowGimmieRewards();
		#elif UNITY_ANDROID
		AndroidJavaClass player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = player.GetStatic<AndroidJavaObject>("currentActivity");
		
		AndroidJavaClass components = new AndroidJavaClass("com.gimmie.components.GimmieComponents");
		components.CallStatic("showRewardCatalogue", activity);
		#endif
	}
	
	public static void TriggerGimmieEvent(string eventName){
		#if UNITY_IPHONE
		TriggerEvent(eventName);
		#elif UNITY_ANDROID
		AndroidJavaClass components = new AndroidJavaClass("com.gimmie.components.GimmieComponents");
		components.CallStatic("triggerEvent", eventName);
		#endif
	}
	
	public void HandleNeedLogin(string message) {
		BroadcastMessage ("GimmieNeedLogin", null, SendMessageOptions.DontRequireReceiver);
	}
	
	public void Awake() {
		GimmieBinding.initGimmie();
	}
}

namespace Gimmie{
	using UnityEngine;
	using System;
	using System.Collections;
	using System.Runtime.InteropServices;
	using OAuth;

	public class GimmieWrapper {
		private static GimmieWrapper instance;
		private static string mKey;
		private static string mSecret;
		private static string mUser;
		private static OAuth.Manager mOAuthManger;

		public GimmieWrapper(string key, string secret){
			if(instance == null){
				mKey = key;
				mSecret = secret;
				instance = this;
			}
		}

		public static GimmieWrapper getInstance(){
			return instance==null? new GimmieWrapper(null, null) : instance;
		}

		public static void InitGimmie(string key, string secret){
			if(instance==null) instance = new GimmieWrapper(key, secret);
			mOAuthManger = new OAuth.Manager(key, secret,"","");
		}

		public static void Login(string u){
			mUser = u;
			//sets the uid as token and game secret as token_secret, for authenticated calling of api later.
			mOAuthManger.setToken(mUser, mSecret);
		}

		/// <summary>
		/// Call the specified endpoint.
		/// JSONObject returned which can be accessed like: j["response"]["user"]["awarded_points"].n
		/// </summary>
		/// <param name="endpoint">Endpoint.</param>
		public static JSONObject CallGimmie(string endpoint){
			var headers = mOAuthManger.GenerateAuthzHeader(endpoint, "GET");

			var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(endpoint);
			request.Headers["Authorization"] = headers;
			request.Accept = "application/json";
			//Debug.Log("getting " + request.RequestUri);
			System.Net.HttpWebResponse response = (System.Net.HttpWebResponse) request.GetResponse();

			System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
			string jsonString = reader.ReadToEnd();

			JSONObject jsonObject = new JSONObject(jsonString);
			//Debug.Log("got the response: " + jsonString);
			return jsonObject;
		}
	}
}

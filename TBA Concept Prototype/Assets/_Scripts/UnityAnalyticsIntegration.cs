using UnityEngine;
using System.Collections;
using UnityEngine.Cloud.Analytics;

public class UnityAnalyticsIntegration : MonoBehaviour {

	// Use this for initialization
	void Start () {
		const string projectId = "42a99555-8a00-4aaf-905f-27fc669291ed";
		UnityAnalytics.StartSDK (projectId);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

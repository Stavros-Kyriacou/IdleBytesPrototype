using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text.RegularExpressions;
using UnityEngine.Events;

public class WorldTimeAPI : MonoBehaviour
{
    #region Singleton WorldTimeAPI

    public static WorldTimeAPI Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    struct TimeData
    {
		//used to store json data from web api
        public string datetime;
		public int day_of_week;
    }
    
	const string API_URL = "https://worldtimeapi.org/api/ip";
    [HideInInspector] public bool IsTimeLoaded = false;
    public GameController gameController;
	public int dayOfWeek;
    private DateTime currentDateTime;
    public UnityEvent OnTimeLoaded;

    private void Start()
    {
        StartCoroutine(GetDateTimeFromAPI());
    }
    public DateTime GetCurrentDateTime()
    {
		//dont need to constantly get time from api request
		//store the time from api when first loading game
		//add time that game has been running to it
        return currentDateTime.AddSeconds(Time.realtimeSinceStartup);
    }
    IEnumerator GetDateTimeFromAPI()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(API_URL);
        Debug.Log("getting datetime");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
			//could not connect
            Debug.Log("Error: " + webRequest.error);
			Debug.Log("Could not get real time from API, you are not permitted to log in");
        }
        else
        {
			//store json data from api in TimeData struct
            TimeData timeData = JsonUtility.FromJson<TimeData>(webRequest.downloadHandler.text);

            currentDateTime = ParseDateTime(timeData.datetime);
			dayOfWeek = timeData.day_of_week;
            IsTimeLoaded = true;
			//load gamedata
            Debug.Log("Time loaded");
            //? only runs if OnTimeLoaded is not null
            OnTimeLoaded?.Invoke();
            gameController.SetTime();
        }
    }
    DateTime ParseDateTime(string datetime)
    {
		//convert the datetime string from json web api request to DateTime format using regex
        //0000 - 00 - 00 format
        string date = Regex.Match(datetime, @"^\d{4}-\d{2}-\d{2}").Value;

        //00:00:00 format
        string time = Regex.Match(datetime, @"\d{2}:\d{2}:\d{2}").Value;

        return DateTime.Parse(string.Format("{0} {1}", date, time));
    }
}


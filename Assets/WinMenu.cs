using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

using LitJson;
public class Score
{
    public string name;
    public float score;

    public Score(string _name, double _score)
    {
        name = _name;
        score = (float)_score;
    }
}
public class WinMenu : MonoBehaviour
{
    public List<Score> scores;
    public GameObject content;

    public GameObject Input;

    public TextMeshProUGUI Rank;
    public TextMeshProUGUI Score;


    public int stage;
    void Awake()
    {
        scores = new List<Score>();
    }

    private void OnEnable()
    {
        StartCoroutine(GetTop());
        Score.text = Utils.timeToFormat(GameManager.Main.score);

    }

    void Update()
    {

    }
    IEnumerator GetTop()
    {
        var url = "http://182.92.243.47:5000/top?stage=" + stage.ToString();
        var www = UnityWebRequest.Get(url);
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.isHttpError || www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log(www.downloadHandler.text);
            JsonData result_data = LoadResult(www.downloadHandler.text);
            //print((Score)result_data["top10"][0]);
            if ((string)result_data["status"] == "ok")
            {
                if (result_data["top10"].Count >= 10)
                    for (int i = 0; i < 10; i++)
                    {
                        double __score = (double)result_data["top10"][i]["score"];
                        Score _ = new Score((string)result_data["top10"][i]["name"], __score);
                        if (_ != null) scores.Add(_);
                        //print(_);
                    }
            }
            print(scores);
            List<TextMeshProUGUI> names = new List<TextMeshProUGUI>();
            List<TextMeshProUGUI> socres = new List<TextMeshProUGUI>();
            int index = 0;
            foreach (var score in scores)
            {
                var user = content.transform.Find("User" + (index + 1).ToString()).gameObject;
                user.transform.Find("NAME").gameObject.GetComponent<TextMeshProUGUI>().text = score.name;
                user.transform.Find("SCORE").gameObject.GetComponent<TextMeshProUGUI>().text = Utils.timeToFormat(score.score);
                index++;
            }

        }
    }

    JsonData LoadResult(string json_text)
    {
        //print("Reading data from the following JSON string:"+json_text);

        JsonData data = JsonMapper.ToObject(json_text);
        return data;
    }
    public void OnClickNextLevel()
    {
        StartCoroutine(SubmitScore());
    }

    public void GoNextLevel()
    {
        SceneManager.LoadScene(stage + 2);
    }
    IEnumerator SubmitScore()
    {
        var url = "http://182.92.243.47:5000/rank?stage=" + stage.ToString() + "&name=" + Input.GetComponent<TMP_InputField>().text + "&score=" + GameManager.Main.score;
        var www = UnityWebRequest.Get(url);
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.isHttpError || www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            print(www.downloadHandler.text);
            JsonData result_data = LoadResult(www.downloadHandler.text);
            //print((Score)result_data["top10"][0]);
            if ((string)result_data["status"] == "ok")
            {
                ;//Rank.text = (string)(int)result_data["rank"];
            }

        }
        GoNextLevel();
    }
}
public class Utils
{
    public static string timeToFormat(float time)
    {
        int hour = 0;
        int min = 0;
        int sec = 0;
        int mi = 0;
        if (time > 60 * 60) { return ((int)time / 3600).ToString() + "'" + ((int)time % 3600 / 60).ToString() + "'" + ((int)time % 60).ToString(); }
        else if (time > 60) { return ((int)time % 3600 / 60).ToString() + "'"+ ((int)time % 60).ToString() + "''" + Mathf.Round(100 * (time - (int)time)).ToString();}
        else { return ((int)time % 60).ToString() + "''" + Mathf.Round(100 * (time - (int)time)).ToString(); }
    }
}

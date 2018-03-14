using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashManage : MonoBehaviour
{

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(StartMainScene());
    }

    private IEnumerator StartMainScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Main");
    }

    // Update is called once per frame
    private void Update()
    {

    }
}

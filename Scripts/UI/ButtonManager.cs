using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class ButtonManager : MonoBehaviour
{

    public GameObject[] group;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void QuitGame()
	{
        Application.Quit();
	}
   
    public void DisableUIGroup(int changeFrom)
    {
        group[changeFrom].SetActive(false);
    }
    public void EnableUIGroup(int changeTo)
	{
        group[changeTo].SetActive(true);
    }

	public void LoadScene(int scene)
	{
        SceneManager.LoadScene(scene);
	}

    public void WipeHighScore()
    {
        if (File.Exists(Application.persistentDataPath + "/playerData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/playerData.dat");
        }

    }
}

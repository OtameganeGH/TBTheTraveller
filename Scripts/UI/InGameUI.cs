using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class InGameUI : MonoBehaviour
{
   
    public Text colllectables, lives, currentScore, recordedHighScore, stillCollect, stillLives;
    public int lifeAmount, collectAmount, howManyCollectablesForLives, hideUISpeed, startngLives, totalCollected, lastHighScore= 0;
    public float UIHideTime, initialShowTime;
    private bool UIRecentlyUpdated, showTotalScore, shipPiece1, shipPiece2, shipPiece3, shipPieceProg;
    public bool currentSceneShipPartCollected = false;
    public GameObject hiddenPositionObject, UIMover, CSHolder, StillScore;

    //Ship part ui elements
    public GameObject UIpartCollected1, UIpartCollected2, UIpartCollected3, UIpartCollectedProg, UIpartUncollected1, UIpartUncollected2, UIpartUncollected3, UIpartUncollectedProg;

    Vector3 hiddenPosition, inViewPosition;
    void Start()
    {
        LoadHighScore();
        UIRecentlyUpdated = true;
        recordedHighScore.text = lastHighScore.ToString();
       
        
        inViewPosition = new Vector3(UIMover.transform.position.x, UIMover.transform.position.y, UIMover.transform.position.z);
        hiddenPosition = new Vector3(UIMover.transform.position.x, hiddenPositionObject.transform.position.y, UIMover.transform.position.z);
        CheckforPartsCollected();
    }

    // Update is called once per frame
    void Update()
    {
        float interpolation = UIHideTime * Time.deltaTime;
        Vector3 position = UIMover.transform.position;
        lives.text = lifeAmount.ToString();
        stillLives.text = lifeAmount.ToString();
        currentScore.text = totalCollected.ToString();
        colllectables.text = collectAmount.ToString();
        stillCollect.text = collectAmount.ToString();
        if (collectAmount == howManyCollectablesForLives)
        {
            IncreaseLives();
            collectAmount = 0;
        }


        if (lifeAmount < 0)
        {
            LifeSave();
            SceneManager.LoadScene(5);
        }



        if (UIRecentlyUpdated == true)
        {
            //Shows
            Invoke("HideUI", UIHideTime);

            position.y = Mathf.Lerp(UIMover.transform.position.y, inViewPosition.y, interpolation);
            UIMover.transform.position = position;
        }
        else
        {
            //Hides
            position.y = Mathf.Lerp(UIMover.transform.position.y, hiddenPosition.y, interpolation);
            UIMover.transform.position = position;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HighScoreToggle();
        }
       

        if (showTotalScore == true)
        {
            CSHolder.SetActive(true);
            UIMover.SetActive(false);
            StillScore.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            CSHolder.SetActive(false);
            UIMover.SetActive(true);
            StillScore.SetActive(false);
        }

    }

    public void IncreaseCollectables(int increaseAmount)
    {

        CancelInvoke("HideUI");
        collectAmount += increaseAmount;
        totalCollected += increaseAmount;
        UIRecentlyUpdated = true;



    }
    public void ReduceLives()
    {
        CancelInvoke("HideUI");
        lifeAmount -= 1;

        UIRecentlyUpdated = true;
    }
    public void IncreaseLives()
    {
        CancelInvoke("HideUI");
        lifeAmount += 1;

        UIRecentlyUpdated = true;
    }

    public void HighScoreToggle()
    {
        if (showTotalScore == false)
        {
            showTotalScore = true;
           
        }
        else if (showTotalScore == true)
        {
            showTotalScore = false;
        }
    }


	public void QuitButton(int scene)
	{
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
	}

	void HideUI()
    {
        UIRecentlyUpdated = false;
    }

    public void SaveHighScore()
    {
        Debug.Log("StartedSaving");
        PlayerData playerData = new PlayerData();
        BinaryFormatter bf = new BinaryFormatter();

        
            if (File.Exists(Application.persistentDataPath + "/playerData.dat"))
            {
                FileStream file = File.Open(Application.persistentDataPath + "/playerData.dat", FileMode.Open);


                Debug.Log("Saving over last file");

                {
                    Scene currentscene = SceneManager.GetActiveScene();

                    switch (currentscene.name)
                    {
                        case "ProgTest":
                            Debug.Log("Saving ProgTest");
                        if (totalCollected > lastHighScore)
                        {
                            playerData.progTestHighscore = totalCollected;
						}
						else
						{
                            playerData.progTestHighscore = lastHighScore;
                        }

						if (currentSceneShipPartCollected == true)
						{
                            playerData.ProgTestPieceCollected = true;
						}
                        playerData.lifeAmountSaved = lifeAmount;
                        break;

                        case "Desert":
                            Debug.Log("Saving Desert");
                        if (totalCollected > lastHighScore)
                        {
                            playerData.desertHighscore = totalCollected;
                        }
                        else
                        {
                            playerData.progTestHighscore = lastHighScore;
                        }

                        if (currentSceneShipPartCollected == true)
                        {
                            playerData.Level1PieceCollected = true;
                        }
                        playerData.lifeAmountSaved = lifeAmount;
                        break;

                        case "Jungle":
                            Debug.Log("Saving Jungle");
                        if (totalCollected > lastHighScore)
                        {
                            playerData.jungleHighscore = totalCollected;
                        }
                        else
                        {
                            playerData.progTestHighscore = lastHighScore;
                        }

                        if (currentSceneShipPartCollected == true)
                        {
                            playerData.Level2PieceCollected = true;
                        }
                        playerData.lifeAmountSaved = lifeAmount;
                        break;

                        case "Snow":
                            Debug.Log("Saving Snow");
                        if (totalCollected > lastHighScore)
                        {
                            playerData.snowTestHighscore = totalCollected;
                        }
                        else
                        {
                            playerData.progTestHighscore = lastHighScore;
                        }

                        if (currentSceneShipPartCollected == true)
                        {
                            playerData.Level3PieceCollected = true;
                        }
                        playerData.lifeAmountSaved = lifeAmount;
                        break;
                    }
                    bf.Serialize(file, playerData);
                    file.Close();

                }
            }
            else
            {
                FileStream stream = File.Create(Application.persistentDataPath + "/playerData.dat");

                Debug.Log("Saving while creating new file");

                {
                    Scene currentscene = SceneManager.GetActiveScene();

                    switch (currentscene.name)
                    {
                        case "ProgTest":
                            Debug.Log("Saving ProgTest");
                        if (totalCollected > lastHighScore)
                        {
                            playerData.progTestHighscore = totalCollected;
                        }
                        else
                        {
                            playerData.progTestHighscore = lastHighScore;
                        }

                        if (currentSceneShipPartCollected == true)
                        {
                            playerData.ProgTestPieceCollected = true;
                        }
                        playerData.lifeAmountSaved = lifeAmount;
                        break;
                        case "Desert":
                            Debug.Log("Saving Desert");
                        if (totalCollected > lastHighScore)
                        {
                            playerData.desertHighscore = totalCollected;
                        }
                        else
                        {
                            playerData.progTestHighscore = lastHighScore;
                        }

                        if (currentSceneShipPartCollected == true)
                        {
                            playerData.Level1PieceCollected = true;
                        }
                        playerData.lifeAmountSaved = lifeAmount;
                        break;
                        case "Jungle":
                            Debug.Log("Saving Jungle");
                        if (totalCollected > lastHighScore)
                        {
                            playerData.jungleHighscore = totalCollected;
                        }
                        else
                        {
                            playerData.progTestHighscore = lastHighScore;
                        }

                        if (currentSceneShipPartCollected == true)
                        {
                            playerData.Level2PieceCollected = true;
                        }
                        playerData.lifeAmountSaved = lifeAmount;
                        break;
                        case "Snow":
                            Debug.Log("Saving Snow");
                        if (totalCollected > lastHighScore)
                        {
                            playerData.snowTestHighscore = totalCollected;
                        }
                        else
                        {
                            playerData.progTestHighscore = lastHighScore;
                        }

                        if (currentSceneShipPartCollected == true)
                        {
                            playerData.Level3PieceCollected = true;
                        }
                        playerData.lifeAmountSaved = lifeAmount;
                        break;
                    }
                    bf.Serialize(stream, playerData);
                    stream.Close();
                }
            }      
    }


    public void LoadHighScore()
    {
        //LOADS HIGH SCORE FROM BINARY FILE BASED ON WHICH LEVEL IS LOADED
        if (File.Exists(Application.persistentDataPath + "/playerData.dat"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerData.dat", FileMode.Open);
            if (file.Length == 0)
            {
                lifeAmount = startngLives;
                file.Close();
               
            }
            else
            {
                PlayerData data = (PlayerData)bf.Deserialize(file);
                file.Close();
                //Debug.Log("Started Loading");
                Scene currentscene = SceneManager.GetActiveScene();
                switch (currentscene.name)
                {
                    case "ProgTest":
                        Debug.Log(" Loading ProgTest");
                        						
                        lastHighScore = data.progTestHighscore;
                        if(data.ProgTestPieceCollected == true)
						{
                            shipPieceProg = true;
                        }
                        if (data.Level1PieceCollected == true)
                        {
                            shipPiece1 = true;
                        }
                        if (data.Level2PieceCollected == true)
                        {
                            shipPiece2 = true;
                        }
                        if (data.Level3PieceCollected == true)
                        {
                            shipPiece3 = true;
                        }
                        lifeAmount = data.lifeAmountSaved;
                        break;
                    case "Desert":
                        Debug.Log(" Loading Desert");
                        lastHighScore = data.desertHighscore;
                        if (data.ProgTestPieceCollected == true)
                        {
                            shipPieceProg = true;
                        }
                        if (data.Level1PieceCollected == true)
                        {
                            shipPiece1 = true;
                        }
                        if (data.Level2PieceCollected == true)
                        {
                            shipPiece2 = true;
                        }
                        if (data.Level3PieceCollected == true)
                        {
                            shipPiece3 = true;
                        }
                        lifeAmount = data.lifeAmountSaved;
                        break;
                    case "Jungle":
                        Debug.Log(" Loading Jungle");
                        lastHighScore = data.jungleHighscore;
                        if (data.ProgTestPieceCollected == true)
                        {
                            shipPieceProg = true;
                        }
                        if (data.Level1PieceCollected == true)
                        {
                            shipPiece1 = true;
                        }
                        if (data.Level2PieceCollected == true)
                        {
                            shipPiece2 = true;
                        }
                        if (data.Level3PieceCollected == true)
                        {
                            shipPiece3 = true;
                        }
                        lifeAmount = data.lifeAmountSaved;
                        break;
                    case "Snow":
                        Debug.Log(" Loading Snow");
                        lastHighScore = data.snowTestHighscore;
                        if (data.ProgTestPieceCollected == true)
                        {
                            shipPieceProg = true;
                        }
                        if (data.Level1PieceCollected == true)
                        {
                            shipPiece1 = true;
                        }
                        if (data.Level2PieceCollected == true)
                        {
                            shipPiece2 = true;
                        }
                        if (data.Level3PieceCollected == true)
                        {
                            shipPiece3 = true;
                        }
                        lifeAmount = data.lifeAmountSaved;
                        break;

                }
                file.Close();
            }

        }
    }

   
    public void WipeHighScore()
    {
        if (File.Exists(Application.persistentDataPath + "/playerData.dat"))
		{
            File.Delete(Application.persistentDataPath + "/playerData.dat");
		}

    }

    private void CheckforPartsCollected()
	{
        if(shipPiece1 == true)
		{
            UIpartCollected1.SetActive(true);
            UIpartUncollected1.SetActive(false);
		}
        if(shipPiece2 == true)
        {
            UIpartCollected2.SetActive(true);
            UIpartUncollected2.SetActive(false);
        }
       if(shipPiece3 == true)
        {
            UIpartCollected3.SetActive(true);
            UIpartUncollected3.SetActive(false);
        }
        if(shipPieceProg == true)
        {
            UIpartCollectedProg.SetActive(true);
            UIpartUncollectedProg.SetActive(false);
        }


        //Change to a for loop?
    }

    public void LifeSave()
    {
        if (File.Exists(Application.persistentDataPath + "/playerData.dat"))
        {

            PlayerData playerData = new PlayerData();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerData.dat", FileMode.Open);

            playerData.lifeAmountSaved = startngLives;

            bf.Serialize(file, playerData);
            file.Close();
        }


    }



	[System.Serializable]
    public struct PlayerData
    {

        public int progTestHighscore;
        public int desertHighscore;
        public int jungleHighscore;
        public int snowTestHighscore;
        public int lifeAmountSaved;
        public bool Level1PieceCollected;
        public bool Level2PieceCollected;
        public bool Level3PieceCollected;
        public bool ProgTestPieceCollected;

    }

}


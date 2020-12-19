using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;




public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public int score;
    private float speed;
    public float baseSpeed;
    private bool isPause = false;


    //Sounds
    //------------------------------------------------------------------
    private AudioSource Beat;

    
    

    //Floor Animations
    //------------------------------------------------------------------
    public Material floorMatt;
    public GameObject floorOBJ;    
    private float floorSpeed;   

    public GameObject[] walls;

    //UI
    //------------------------------------------------------------------
    public Image counterImage1;
    public Image counterImage2;
    public Image counterImage3;
    public Text scoreText;
    public GameObject scoreIMG;
    public GameObject Menu;
    public Button quitButton;
    public Button settingsButton;
    public Text highScoreText;
    private int highScoreNumb;
    public Image movingText;

    public Button visualSettings, soundSettings;
    public GameObject visualBox, soundBox;
    private AudioSource playExampleSound;
    public void chooseSettings(int i)
    {
        switch (i)
        {
            case 0:
                //visual
                visualBox.SetActive(true);
                soundBox.SetActive(false);
                break;
            case 1:
                visualBox.SetActive(false);
                soundBox.SetActive(true);
                break;
        }
    }

    public Sprite amazingText, niceText;
    public bool IsTextMocingEffect = false;
    

    public GameObject settingsWindow;   

    public void SettingsOnOff()
    {
        if (settingsWindow.activeSelf == false)
        {
            settingsWindow.SetActive(true);
            Menu.SetActive(false);
            quitButton.gameObject.SetActive(false);
        }
        else
        {
            settingsWindow.SetActive(false);
            Menu.SetActive(true);
            quitButton.gameObject.SetActive(true);
        }
    }

    public Image[] playerCosmeticsUI;
   

    private void currentCosmeticUI(int select)
    {
        int i = 0;
        foreach (Image item in playerCosmeticsUI)
        {
            if (select == i)
            {
                playerCosmeticsUI[select].color = Color.white;                
            }
            else
            {
                item.color = Color.grey;
            }
            i++;
        }
    }

    //Settings
    //----------------------------------------------------------------------
    //Sound Settings
    public AudioClip[] playerClips;
    public Button[] soundsUI;    
    //public AudioListener gameSound;
    public Text soundSwitcher;

    public void muteGame()
    {
        if (AudioListener.pause == false)
        {
            //gameSound.enabled = false;
            AudioListener.pause = true;
            PlayerPrefs.SetInt("volume", 0);
            soundSwitcher.text = "<color=white>Sound:</color><color=grey> ON </color><color=white>/</color><color=#00F9FF> OFF</color>"; // when sound is off
        }
        else
        {
            //gameSound.enabled = true;
            AudioListener.pause = false;
            PlayerPrefs.SetInt("volume", 1);
            soundSwitcher.text = "<color=white>Sound:</color><color=#00F9FF> ON</color><color=white> /</color><color=grey> OFF</color>"; // sound is on
        }            
    }
    private void checkIfGameMuted()
    {
        switch (PlayerPrefs.GetInt("volume", 1))
        {
            case 0:
                AudioListener.pause = true;
                soundSwitcher.text = "<color=white>Sound:</color><color=grey> ON </color><color=#00F9FF>/ OFF</color>"; // when sound is off                break;
                break;
            case 1:
                AudioListener.pause = false;
                soundSwitcher.text = "<color=white>Sound:</color><color=#00F9FF> ON</color><color=white> /</color><color=grey> OFF</color>"; // sound is on
                break;
        }
    }

    //changes the player sound and saves it
    public void changeClip(int i)
    {
        int p = 0;
        Player.GetComponent<AudioSource>().clip = playerClips[i];
        PlayerPrefs.SetInt("currentClip", i);

        foreach (Button item in soundsUI)
        {
            if (i == p)
            {
                soundsUI[i].image.color = Color.white;
            }
            else
            {
                item.image.color = Color.grey;
            }
            p++;
        }
    }

    //Play the sound once as example
    public void testSound(int i)
    {
        playExampleSound.clip = playerClips[i];
        playExampleSound.Play();
    }
    
    //------------
    public Texture[] cosmetics;
    public int currentCosmetic;
    public Material playerMatt;



    public void selectCosmetics(int select)
    {
        PlayerPrefs.SetInt("currentCosmetics",select);
        playerMatt.SetTexture("Texture2D_A37A185C", cosmetics[select]);
       // exampleCosmetic.sprite = examplePlayerUI[select];

        currentCosmeticUI(select);
    }

    
    public void selectCosmeticsColor(int color)
    {
        Color selectedColor;
        switch (color)
        {
            case 0:
                selectedColor = new Color(5f, 0f, 5f); //Pink
                playerMatt.SetColor("Color_B1C3BAC1", selectedColor);
                PlayerPrefs.SetInt("currentCosmeticsColor", 0);
               // exampleCosmetic.color = selectedColor;
                break;

            case 1:
                selectedColor = new Color(0f, 5f, 5f); // blue
                playerMatt.SetColor("Color_B1C3BAC1", selectedColor);
                PlayerPrefs.SetInt("currentCosmeticsColor", 1);
               // exampleCosmetic.color = selectedColor;
                break;

            case 2:
                selectedColor = new Color(0f, 5f, 0f); //green
                playerMatt.SetColor("Color_B1C3BAC1", selectedColor);
                PlayerPrefs.SetInt("currentCosmeticsColor", 2);
             //   exampleCosmetic.color = selectedColor;
                break;

            case 3:
                selectedColor = new Color(5f, 5f, 0f); //yellow
                playerMatt.SetColor("Color_B1C3BAC1", selectedColor);
                PlayerPrefs.SetInt("currentCosmeticsColor", 3);
              //  exampleCosmetic.color = selectedColor;
                break;

            case 4:
                selectedColor = new Color(5f, 0f, 0f); //red
                playerMatt.SetColor("Color_B1C3BAC1", selectedColor);
                PlayerPrefs.SetInt("currentCosmeticsColor", 4);
              //  exampleCosmetic.color = selectedColor;
                break;
                
        }                
    }


    

    public Volume postProccesing;
    private Bloom bloom;
    public Slider bloomSliderUI;
    public void bloomSlider(float i)
    {
        bloom.intensity.value = i;
        PlayerPrefs.SetFloat("defaultBloom", i);
    }



    // Start is called before the first frame update
    void Start()
    {
        
        Beat = GetComponent<AudioSource>();
        playExampleSound = soundBox.GetComponent<AudioSource>();
        postProccesing.profile.TryGet(out bloom);
        bloomSliderUI.value = PlayerPrefs.GetFloat("defaultBloom", 5);

        changeClip(PlayerPrefs.GetInt("currentClip", 0));

        checkIfGameMuted();

        highScoreNumb = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = highScoreNumb.ToString();
        Menu.SetActive(true);
        scoreIMG.SetActive(false);
        quitButton.gameObject.SetActive(true);
        counterImage1.gameObject.SetActive(false);
        counterImage2.gameObject.SetActive(false);
        counterImage3.gameObject.SetActive(false);
        scoreText.text = score.ToString();
        Time.timeScale = 0;
        selectCosmetics(PlayerPrefs.GetInt("currentCosmetics", 1));
        selectCosmeticsColor(PlayerPrefs.GetInt("currentCosmeticsColor", 0));
        floorSpeed = -10.5f;
        floorMatt.SetVector("Vector2_85B49CA4", new Vector2(0, floorSpeed));
        score = 0;
        speed = baseSpeed;
        foreach (GameObject wall in walls)
        {
            wall.GetComponent<WallScript>().speed = speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTextMocingEffect)
        {
            Vector2 i = new Vector2(-1000, movingText.rectTransform.anchoredPosition.y);
            Vector2 p = new Vector2(900f, movingText.rectTransform.anchoredPosition.y);
            Debug.Log(p);
            movingText.rectTransform.anchoredPosition = Vector2.MoveTowards(movingText.rectTransform.anchoredPosition, i, 500f * Time.deltaTime);

            if (movingText.rectTransform.anchoredPosition == i)
            {
                movingText.rectTransform.anchoredPosition = p;                
                IsTextMocingEffect = false;                
            }
                
        }
       
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 12 && !isPause)
        {
            
            score += 10;
            scoreText.text = score.ToString();
            if (score % 100 == 0)
            {
                if (speed <= 100)
                {
                    speed += 10;
                    foreach (GameObject wall in walls)
                    {
                        wall.GetComponent<WallScript>().speed = speed;
                    }
                    floorMatt.SetVector("Vector2_85B49CA4", new Vector2(0, floorMatt.GetVector("Vector2_85B49CA4").y - 3.5f));
                }
                movingText.sprite = niceText;

                if (score % 500 == 0)
                {
                    movingText.sprite = amazingText;                    
                }

                IsTextMocingEffect = true;
                floorOBJ.GetComponent<Animator>().Play("FloorGlow");
                
            }
            
            Debug.Log(score);
            Debug.Log("speed is" + speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 12 && !isPause)
        Beat.Play();
    }

    IEnumerator PreGameCountdown()
    {
        Player.GetComponent<PlayerScript>().gameStarted = false;
        Time.timeScale = 0;
        score = 0;
        scoreText.text = score.ToString();
        foreach (GameObject wall in walls)
        {
            wall.transform.position = wall.GetComponent<WallScript>().gameStartPos;
        }
        

        yield return new WaitForSecondsRealtime(0.5f);
        //3
        counterImage3.gameObject.SetActive(true);
        
        yield return new WaitForSecondsRealtime(1f);
        //2
        counterImage3.gameObject.SetActive(false);
        counterImage2.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        //1        
        counterImage2.gameObject.SetActive(false);
        counterImage1.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        scoreIMG.SetActive(true);
        counterImage1.gameObject.SetActive(false);
        Time.timeScale = 1;
        Player.GetComponent<PlayerScript>().gameStarted = true;
        yield return new WaitForSecondsRealtime(0.5f);
        isPause = false;


    }

    public void startGame()
    {
        quitButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        Menu.SetActive(false);        
        StartCoroutine(PreGameCountdown());
    }

    public void gameOver()
    {
        

        isPause = true;
        Time.timeScale = 0;
        quitButton.gameObject.SetActive(true);
        Menu.SetActive(true);
        settingsButton.gameObject.SetActive(true);

        if (score > highScoreNumb)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreNumb = PlayerPrefs.GetInt("HighScore", score);
            highScoreText.text = highScoreNumb.ToString();
        }
        speed = baseSpeed;
        floorMatt.SetVector("Vector2_85B49CA4", new Vector2(0, floorSpeed));
        foreach (GameObject wall in walls)
        {
            wall.GetComponent<WallScript>().speed = speed;
            wall.GetComponent<WallScript>().randomWallPosition();
        }        


    }


    public void quitGame()
    {
        Application.Quit();
    }

}

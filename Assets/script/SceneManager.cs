using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{

    //home
    public Button playBtn;
    public Button settingBtn;
    public Button endGameBtn;

    //setting
    public Button graphicBtn;
    public Button audioBtn;
    public Button languageBtn;
    public Button backToHomeBtn;

    //grahic


    //selectStage
    public Button playerDetaBtn;
    public Button backToHomeBtnOnSelectStage;
    public Button backToSelectStageBtn;

    public GameObject gameScene;
    public GameObject homeScene;
    public GameObject settingScene;

    public GameObject graphicScene;
    public GameObject audioScene;
    public GameObject languageScene;

    public GameObject selectStageScene;
    public GameObject playerDetaScene;

    // Start is called before the first frame update
    void Start()
    {
        playBtn.onClick.AddListener(PlayBtn);
        settingBtn.onClick.AddListener(OpenSettingBtn);
        endGameBtn.onClick.AddListener(EndGameBtn);

        graphicBtn.onClick.AddListener(OpenGraphicSetting);
        audioBtn.onClick.AddListener(OpenAudioSetting);
        languageBtn.onClick.AddListener(OpenLanguageSetting);
        backToHomeBtn.onClick.AddListener(CloseSettingBtn);

        playerDetaBtn.onClick.AddListener(OpenPlayerDeta);
        backToHomeBtnOnSelectStage.onClick.AddListener(BackToHome);
        backToSelectStageBtn.onClick.AddListener(BackToSelectStage);
    }

    public void PlayBtn()
    {
        selectStageScene.SetActive(true);
        homeScene.SetActive(false);
    }

    public void OpenSettingBtn()
    {
        settingScene.SetActive(true);
        homeScene.SetActive(false);
    }

    public void EndGameBtn()
    {

    }

    public void CloseSettingBtn()
    {
        homeScene.SetActive(true);
        settingScene.SetActive(false);
    }



    //Setting
    public void OpenGraphicSetting()
    {
        graphicScene.SetActive(true);
        settingScene.SetActive(false);
    }
    public void OpenAudioSetting()
    {
        audioScene.SetActive(true);
        settingScene.SetActive(false);
    }



    public void OpenLanguageSetting()
    {
        languageScene.SetActive(true);
        settingScene.SetActive(false);
    }

    public void BackToSetting()
    {
        settingScene.SetActive(true);
        languageScene.SetActive(false);
        graphicScene.SetActive(false);
        audioScene.SetActive(false);
        selectStageScene.SetActive(false);
    }

    public void BackToHome()
    {
        homeScene.SetActive(true);
        selectStageScene.SetActive(false);
    }

    public void OpenPlayerDeta()
    {
        playerDetaScene.SetActive(true);
        selectStageScene.SetActive(false);
    }

    public void BackToSelectStage()
    {
        playerDetaScene.SetActive(false);
        selectStageScene.SetActive(true);
    }
}

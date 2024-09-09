using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerPower : MonoBehaviour
{

    [Header("Pfefs削除")]
    public bool isDdestroyPrefs;
    [Header("実数値")]
    public int coin;
    public int ballPower;
    public int paddleSize;
    public float criticalChance;
    public int initialBallCount;
    public int ballSize;

    [Header("Lv")]
    public int ballPowerLv;
    public int paddleSizeLv;
    public int criticalChanceLv;
    public int initialBallCountLv;
    public int ballSizeLv;

    [Header("必要なコイン")]
    public int defNeedCoinOfBallPower;
    public int defNeedCoinOfPaddleSize;
    public int defNeedCoinOfCriticalChance;
    public int defNeedCoinOfInitialBallCount;
    public int defNeedCoinOfBallSize;

    [Header("ボタン")]
    public Button ballPowerBtn;
    public Button paddleSizeBtn;
    public Button criticalChanceBtn;
    public Button initialBallCountBtn;
    public Button ballSizeBtn;

    [Header("ボタンのテキスト")]
    public Text ballPowerBtnText;
    public Text paddleSizeBtnText;
    public Text criticalChanceBtnText;
    public Text initialBallCountBtnText;
    public Text ballSizeBtnText;


    [Header("Text")]
    public Text coinTxt;
    public Text ballPowerTxt;
    public Text paddleSizeTxt;
    public Text criticalChanceTxt;
    public Text initialBallCountTxt;
    public Text ballSizeTxt;

    public enum PLAYER_DETA
    {
        Coin,
        BallPower, 
        PaddleSize, 
        CriticalChance,
        InitialBallCount,
        BallSize
    }
    // Start is called before the first frame update
    void Start()
    {

        //削除用
        if (isDdestroyPrefs) DeleteAllPlayerPrefs();


        coin = LoadIntPrefs(PLAYER_DETA.Coin);
        ballPowerLv = LoadIntPrefs(PLAYER_DETA.BallPower);
        paddleSizeLv = LoadIntPrefs(PLAYER_DETA.PaddleSize);
        criticalChanceLv = LoadIntPrefs(PLAYER_DETA.CriticalChance);
        initialBallCountLv = LoadIntPrefs(PLAYER_DETA.InitialBallCount);
        ballSizeLv = LoadIntPrefs(PLAYER_DETA.BallSize);

        coinTxt.text = "所持金：" + coin.ToString() + "g";


        ballPowerTxt.text = "Lv" + ballPowerLv.ToString();
        paddleSizeTxt.text = "Lv" + paddleSizeLv.ToString();
        criticalChanceTxt.text = "Lv" + criticalChanceLv.ToString();
        initialBallCountTxt.text = "Lv" + initialBallCountLv.ToString();
        ballSizeTxt.text = "Lv" + ballSizeLv.ToString();

        ballPowerBtnText.text = defNeedCoinOfBallPower * ballPowerLv + "g";
        paddleSizeBtnText.text = defNeedCoinOfPaddleSize * paddleSizeLv + "g";
        criticalChanceBtnText.text = defNeedCoinOfCriticalChance * criticalChanceLv + "g";
        initialBallCountBtnText.text = defNeedCoinOfInitialBallCount * initialBallCountLv + "g";
        ballSizeBtnText.text = defNeedCoinOfBallSize * ballSizeLv + "g";

        if (coin < defNeedCoinOfBallPower * ballPowerLv) ballPowerBtn.enabled = false;
        if(coin < defNeedCoinOfPaddleSize * paddleSizeLv) paddleSizeBtn.enabled = false;
        if(coin < defNeedCoinOfCriticalChance * criticalChanceLv) criticalChanceBtn.enabled = false;
        if(coin < defNeedCoinOfInitialBallCount * initialBallCountLv) initialBallCountBtn.enabled = false;
        if(coin < defNeedCoinOfBallSize * ballSizeLv) ballSizeBtn.enabled = false;

        ballPowerBtn.onClick.AddListener(() => LvUpBtn(PLAYER_DETA.BallPower));
        paddleSizeBtn.onClick.AddListener(() => LvUpBtn(PLAYER_DETA.PaddleSize));
        criticalChanceBtn.onClick.AddListener(() => LvUpBtn(PLAYER_DETA.CriticalChance));
        initialBallCountBtn.onClick.AddListener(() => LvUpBtn(PLAYER_DETA.InitialBallCount));
        ballSizeBtn.onClick.AddListener(() => LvUpBtn(PLAYER_DETA.BallSize));

    }

    public void SetIntPrefs(PLAYER_DETA deta,int num)
    {
        PlayerPrefs.SetInt(deta.ToString(), num);
        PlayerPrefs.Save();
    }

    public int LoadIntPrefs(PLAYER_DETA deta)
    {
        int num = 0;
        if (deta != PLAYER_DETA.Coin) num = 1;

        return PlayerPrefs.GetInt(deta.ToString(), num);
    }

    public void LvUpBtn(PLAYER_DETA deta)
    {

        //所持金チェック
        int defNum = 0;
        int lvNum = 0;
        Button button = null;
        Text text = null;
        Text btnText = null;
        switch (deta)
        {
            case PLAYER_DETA.BallPower:
                defNum = defNeedCoinOfBallPower;
                lvNum = ++ballPowerLv;
                button = ballPowerBtn;
                text = ballPowerTxt;
                btnText = ballPowerBtnText;
                break;
            case PLAYER_DETA.PaddleSize:
                defNum = defNeedCoinOfPaddleSize;
                lvNum = ++paddleSizeLv;
                button = paddleSizeBtn;
                text = paddleSizeTxt;
                btnText = paddleSizeBtnText;
                break;
            case PLAYER_DETA.CriticalChance:
                defNum = defNeedCoinOfCriticalChance;
                lvNum = ++criticalChanceLv;
                button = criticalChanceBtn;
                text = criticalChanceTxt;
                btnText = criticalChanceBtnText;
                break;
            case PLAYER_DETA.InitialBallCount:
                defNum = defNeedCoinOfInitialBallCount;
                lvNum = ++initialBallCountLv;
                button = initialBallCountBtn;
                text = initialBallCountTxt;
                btnText = initialBallCountBtnText;
                break;
            case PLAYER_DETA.BallSize:
                defNum = defNeedCoinOfBallSize;
                lvNum = ++ballSizeLv;
                button = ballSizeBtn;
                text = ballSizeTxt;
                btnText = ballSizeBtnText;
                break;
        }
        coin = coin - defNum * (lvNum - 1);
        SetIntPrefs(deta, lvNum);
        text.text = "Lv" + lvNum.ToString();
        Debug.Log("ballPowerTxt.text" + lvNum);
        btnText.text = (defNum * lvNum).ToString() + "g";
        coinTxt.text = "所持金" + coin + "g";

        if (coin < defNeedCoinOfBallPower * ballPowerLv) ballPowerBtn.enabled = false;
        if (coin < defNeedCoinOfPaddleSize * paddleSizeLv) paddleSizeBtn.enabled = false;
        if (coin < defNeedCoinOfCriticalChance * criticalChanceLv) criticalChanceBtn.enabled = false;
        if (coin < defNeedCoinOfInitialBallCount * initialBallCountLv) initialBallCountBtn.enabled = false;
        if (coin < defNeedCoinOfBallSize * ballSizeLv) ballSizeBtn.enabled = false;
    }

    void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All PlayerPrefs data has been deleted.");
    }
}

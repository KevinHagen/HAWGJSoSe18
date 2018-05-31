using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {

    public GameObject winScreen;
    [Tooltip("order: yellow, red, green, blue")]
    public Image[] botImages;
    public Sprite[] botSprites;
    public float timeBetweenWinners,timeToLosers;
    public Animator chestAnimator;
    public ParticleSystem particles;

    public bool testGameOver;
    public List<Colors> _colorList;
    public PlayerController[] _players;
    
    private Animator[] _playerAnimators;
    private bool gameOver;

    private void Update()
    {
        if (gameOver)
        {
            if (Input.GetButtonDown("Green1"))
            {
                ReturnToMainMenu();
            }
        }
        if(testGameOver)
        {
            GameOver(_colorList,_players);
            testGameOver = false;
        }
    }

    public void GameOver(List<Colors> colorList,PlayerController[] players)
    {
        _players = players;
        _colorList = colorList;
        _playerAnimators = new Animator[_players.Length];

        gameOver = true;
        winScreen.SetActive(true);
        for (int i = 0; i < players.Length; i++)
        {
            _playerAnimators[i] = botImages[i].gameObject.GetComponent<Animator>();
            botImages[players[i].startPosition].sprite = botSprites[(int)players[i].Color];
        }

        StartCoroutine("PlayWinAnimations");
    }

    private IEnumerator PlayWinAnimations()
    {
        chestAnimator.SetTrigger("startAnimation");
        yield return new WaitForSeconds(timeBetweenWinners);

        for (int i=0;i<_players.Length;i++)
        {
            if(_colorList.Contains(_players[i].Color))
            {
                _playerAnimators[_players[i].startPosition].SetTrigger("win");
                yield return new WaitForSeconds(timeBetweenWinners);
            }
            
        }

        chestAnimator.SetTrigger("open");
        particles.Play();

        yield return new WaitForSeconds(timeToLosers);

        for (int i = 0; i < _players.Length; i++)
        {
            if (!_colorList.Contains(_players[i].Color))
            {
                _playerAnimators[_players[i].startPosition].SetTrigger("lose");
            }
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

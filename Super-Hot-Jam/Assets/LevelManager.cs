using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject exit_door;
    public BoxCollider2D level_transition;
    public string name_of_next_level;
    public Animator transition_anim;
    public float transition_time = 1f;

    List<GameObject> bad_guys = new List<GameObject>();
    GameObject player;

    // Start is called before the first frame update
    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(CheckReadyToMoveOn());
    }

    public void LoadLevel(string name)
    {
        StartCoroutine(LoadScene(name));
       //LevelManager.LoadScene(name, LoadSceneMode.Single);
    }

    IEnumerator CheckReadyToMoveOn()
    {
        yield return new WaitUntil(CheckPlayerAtEnd);

        LoadLevel(name_of_next_level);
    }

    bool CheckPlayerAtEnd()
    {
        if (level_transition.bounds.Contains(player.transform.position))
        {
            return true;
        }
        return false;
    }

    IEnumerator LoadScene(string scene_name)
    {
        transition_anim.SetTrigger("StartFade");

        yield return new WaitForSeconds(transition_time);

        SceneManager.LoadScene(scene_name);
    }


    public void AddToEnemyList(GameObject enemy)
    {
        bad_guys.Add(enemy);
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        if(bad_guys.Contains(enemy))
        {
            bad_guys.Remove(enemy);
        }
        
        if(bad_guys.Count <= 0)
        {
            ExitCondition();
        }
    }

    private void ExitCondition()
    {
        exit_door.SetActive(false);
    }


    public void GameOver()
    {
        LoadLevel("MenuScene");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionDetection : MonoBehaviour
{

    private PlayerStats _playerStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerStats.CurrentHealth <= 0) {
            Debug.Log("Current hp: " + _playerStats.CurrentHealth.ToString());
            Debug.Log ("You died noob");

            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene((currentScene%currentScene)+1);


            /*Scene resetScene = SceneManager.GetSceneByName("Level 1");
            SceneManager.LoadScene(resetScene.name);*/
            _playerStats.Reset();
            //Return player to hub.
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("taking damkage");
            _playerStats.CurrentHealth--;
            //RuntimeManager.PlayOneShot(Enemyhit);
            //_cameraShake.StartShake();

        }
    }
}

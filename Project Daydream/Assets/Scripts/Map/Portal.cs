using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    /* ---------------- 인스펙터 --------------- */
    [Header("Scene Name")]
    [SerializeField] private string normalMap;
    [SerializeField] private string coinMap;
    [SerializeField] private string healingMap;
    [SerializeField] private string storeMap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (this.name == "NormalPortal")
            {
                Debug.Log("포탈 접촉, 맵 이동");
                //SceneManager.LoadScene(normalMap);
            }
            else if (this.name == "BonusPortal")
            {
                int randomNumber = Random.Range(1, 100);

                if (randomNumber <= 33)
                {
                    Debug.Log("포탈 접촉, 코인맵 이동");
                    //SceneManager.LoadScene(coinMap);
                }
                else if (randomNumber <= 66)
                {
                    Debug.Log("포탈 접촉, 회복맵 이동");
                    //SceneManager.LoadScene(healingMap);
                }
                else
                {
                    Debug.Log("포탈 접촉, 상점맵 이동");
                    //SceneManager.LoadScene(storeMap);
                }
            }
            else { }
        }
    }
}
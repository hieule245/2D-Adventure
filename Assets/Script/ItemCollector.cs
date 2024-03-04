using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    private int oranges = 0;
    [SerializeField] private Text orangeText;
    [SerializeField] private AudioSource collectSoundEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Orange"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            oranges++;
            orangeText.text = "Oranges: " + oranges;
        }
    }
}

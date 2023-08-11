using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int rotateSpeed;
    MeshRenderer mrs;
    private void Start()
    {
        mrs = GetComponent<MeshRenderer>();
        rotateSpeed = Random.Range(49, 100);
    }
    private void Update()
    {
        if (GameManager.gameManagerSingleton.GetGameState() != GameManager.GameState.Running)
            return;

       // if (PlayerController.isDead) return;
        transform.Rotate(0, rotateSpeed*Time.deltaTime, 0, Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.name == "ValorWings")
            {
                GameManager.gameManagerSingleton.UpdateWings(1);
                AudioPlayer.audioPlayerSingle.PlayAudioOnce(GameManager.gameManagerSingleton.audioData.WingsCollected,transform,AudioSettings.audioSettings.SoundVolume);
                VFXController._vFXControllerSingle.DoVfxEffect(GameManager.gameManagerSingleton.vfxData.TreasureCollection, 
                    new Vector3(transform.position.x, transform.position.y + 1, transform.position.z));
            }

            else
            {
                GameManager.gameManagerSingleton.UpdateCoins(1);
                AudioPlayer.audioPlayerSingle.PlayAudioOnce(GameManager.gameManagerSingleton.audioData.CoinCollected, transform, AudioSettings.audioSettings.SoundVolume);
                VFXController._vFXControllerSingle.DoVfxEffect(GameManager.gameManagerSingleton.vfxData.CoinCollection, this.transform.position);
            }

            mrs.enabled = false;
        }
    }

    private void OnEnable()
    {
        if (mrs != null)
        {
            mrs.enabled = true;
        }
    }

}

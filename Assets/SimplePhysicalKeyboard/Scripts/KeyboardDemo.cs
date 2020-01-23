using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BotaLab {
public class KeyboardDemo : MonoBehaviour {
  [SerializeField]
  private KeySpecial m_keySp;

  private AudioSource m_audioSource;

  [SerializeField]
  private AudioClip m_beepSound;

  // Start is called before the first frame update
  void Start() {
    m_audioSource = GetComponent<AudioSource>();
    if(m_keySp!=null){
      m_keySp.SetCallBack(PlayOnce);
    }
  }

  void PlayOnce(){
    if((m_audioSource!=null) && (m_beepSound!=null)){
      m_audioSource.PlayOneShot(m_beepSound,1f);
    }
  }
}
}

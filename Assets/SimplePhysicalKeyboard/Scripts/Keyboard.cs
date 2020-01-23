using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BotaLab {
public class Keyboard : MonoBehaviour {
  [SerializeField]
  protected Text m_text = null;

  private string m_inputedStr = "";

  [SerializeField]
  protected char m_cursor = '_';

  [SerializeField]
  protected Color m_cursorColor = Color.gray;

  // KeyStroke
  [SerializeField]
  protected float m_stroke = 0.001f;

  private AudioSource m_audioSource;

  [SerializeField]
  private AudioClip m_typingSound;

  [SerializeField]
  private bool m_CapsLock = false;

  private bool m_isShift;

  public float stroke {
    get {
      return m_stroke;
    }
  }

  public bool isShift {
    get {
      return m_isShift;
    }
  }

  public string content{
    get{
      return m_inputedStr;
    }
  }

  public void PlayTypingSound() {
    if ((m_audioSource != null) && (m_audioSource != null)) {
      m_audioSource.PlayOneShot(m_typingSound, 1f);
    }
  }

  public void UpdateScreen() {
    if (m_text != null) {
      m_text.text = m_inputedStr
                    + string.Format("<color='#{0}'>{1}</color>",
                                    ColorUtility.ToHtmlStringRGB(m_cursorColor), m_cursor);
    }
  }

  public void Receive(char c) {
    if (m_CapsLock ^ m_isShift) {
      c = char.ToUpper(c);
    } else {
      c = char.ToLower(c);
    }
    m_inputedStr += c;
    UpdateScreen();
    PlayTypingSound();
  }

  public void BackSpace(int len = 1) {
    if (m_inputedStr.Length > 0) {
      m_inputedStr = m_inputedStr.Substring(0, m_inputedStr.Length - len);
    }
    UpdateScreen();
    PlayTypingSound();
  }

  public void Newline() {
    m_inputedStr += "\n";
    UpdateScreen();
    PlayTypingSound();
  }

  public void SetShift(bool b) {
    m_isShift = b;
    UpdateScreen();
    PlayTypingSound();
  }

  public void SwitchCapsLock() {
    m_CapsLock = !m_CapsLock;
    UpdateScreen();
    PlayTypingSound();
  }

  void Awake() {
    m_audioSource = GetComponent<AudioSource>();
    UpdateScreen();
    // Debug.LogFormat("{0}",ColorUtility.ToHtmlStringRGB(m_cursorColor));
  }
}
}
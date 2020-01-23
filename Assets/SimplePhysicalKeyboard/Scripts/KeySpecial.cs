using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace BotaLab {

public class KeySpecial : MonoBehaviour {

  [SerializeField]
  protected Keyboard m_keyboard;

  protected enum FUNC {
    SPACE,
    BACKSPACE,
    NEWLINE,
    RETURN,
    SHIFT,
    TAB,
    CAPSLOCK,
    SUPER,
  }

  [SerializeField]
  protected FUNC m_function;

  [SerializeField]
  protected Text m_keyTopText;

  [SerializeField]
  protected Color m_defaultColor = Color.black;

  [SerializeField]
  protected Color m_pressedColor = Color.cyan;

  [SerializeField]
  protected BoxCollider m_collider;

  protected UnityAction m_callback;

  // KeyStroke
  // [SerializeField]
  protected float m_stroke = 0.001f;

  private int m_numPressingFingers = 0; // the number of objects pressing this key
  private bool m_isPressing = false;

  private Transform m_tf;

  public void SetCallBack(UnityAction callback) {
    m_callback = callback;
  }

  void Awake() {
    m_tf = transform;
    // Get keyboard automatically
    if (m_keyboard == null) {
      var g = GameObject.Find("Keyboard");
      if (g != null) {
        m_keyboard = g.GetComponent<Keyboard>();
      }
    }

    if (m_keyboard != null) {
      m_stroke = m_keyboard.stroke;
    }

    if (m_keyTopText == null) {
      var g = m_tf.Find("Keycap");
      if (g != null) {
        m_keyTopText = g.GetComponent<Text>();
      }
    }

    if (m_collider == null) {
      m_collider = GetComponent<BoxCollider>();
    }
  }

  void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag == "Stylus") {
      m_numPressingFingers += 1;
      if (m_numPressingFingers > 0) {
        // prev-state is unpressed.
        if (m_isPressing == false) {
          // m_keyboard.Receive(m_char);
          //
          switch (m_function) {
          case FUNC.SPACE:
            // your function
            break;
          case FUNC.BACKSPACE:
            m_keyboard.BackSpace();
            break;
          case FUNC.NEWLINE:
            m_keyboard.Newline();
            break;
          case FUNC.RETURN:
            // your function
            break;
          case FUNC.SHIFT:
            m_keyboard.SetShift(true);
            break;
          case FUNC.TAB:
            // your function
            break;
          case FUNC.CAPSLOCK:
            m_keyboard.SwitchCapsLock();
            break;
          case FUNC.SUPER:
            // your function
            break;
          }
          if (m_callback != null) {
            m_callback();
          }
          m_collider.size += 2f * m_stroke * Vector3.up;
          m_tf.position -= m_stroke * m_tf.up;
          if (m_keyTopText != null) {
            m_keyTopText.color = m_pressedColor;
          }
        }
        m_isPressing = true;
      }
    }
  }

  void OnTriggerExit(Collider other) {
    if (other.gameObject.tag == "Stylus") {
      m_numPressingFingers -= 1;
      if (m_numPressingFingers == 0) {
        switch (m_function) {
        case FUNC.SHIFT:
          m_keyboard.SetShift(false);
          break;
        }
        m_isPressing = false;
        m_collider.size -= 2f * m_stroke * Vector3.up;
        m_tf.position += m_stroke * m_tf.up;
        if (m_keyTopText != null) {
          m_keyTopText.color = m_defaultColor;
        }
      }
    }
  }
}

}

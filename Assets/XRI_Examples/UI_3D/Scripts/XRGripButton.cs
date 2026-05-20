using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace UnityEngine.XR.Content.Interaction
{
    public class XRGripButton : XRBaseInteractable
    {
        [SerializeField]
        Transform m_Button = null;

        [SerializeField]
        float m_PressDistance = 0.1f;

        [SerializeField]
        bool m_ToggleButton = false;

        [SerializeField]
        UnityEvent m_OnPress;

        [SerializeField]
        UnityEvent m_OnRelease;

        [SerializeField]
        [Tooltip("Audio que se reproduce al presionar el botón")]
        AudioSource m_PressAudio = null; // ← Nuevo

        bool m_Hovered = false;
        bool m_Selected = false;
        bool m_Toggled = false;

        public Transform button { get => m_Button; set => m_Button = value; }
        public float pressDistance { get => m_PressDistance; set => m_PressDistance = value; }
        public UnityEvent onPress => m_OnPress;
        public UnityEvent onRelease => m_OnRelease;

        void Start() { SetButtonHeight(0.0f); }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (m_ToggleButton)
                selectEntered.AddListener(StartTogglePress);
            else
            {
                selectEntered.AddListener(StartPress);
                selectExited.AddListener(EndPress);
                hoverEntered.AddListener(StartHover);
                hoverExited.AddListener(EndHover);
            }
        }

        protected override void OnDisable()
        {
            if (m_ToggleButton)
                selectEntered.RemoveListener(StartTogglePress);
            else
            {
                selectEntered.RemoveListener(StartPress);
                selectExited.RemoveListener(EndPress);
                hoverEntered.RemoveListener(StartHover);
                hoverExited.RemoveListener(EndHover);
                base.OnDisable();
            }
        }

        void StartTogglePress(SelectEnterEventArgs args)
        {
            m_Toggled = !m_Toggled;
            if (m_Toggled)
            {
                SetButtonHeight(-m_PressDistance);
                m_OnPress.Invoke();
                if (m_PressAudio != null) m_PressAudio.Play(); // ← Nuevo
            }
            else
            {
                SetButtonHeight(0.0f);
                m_OnRelease.Invoke();
            }
        }

        void StartPress(SelectEnterEventArgs args)
        {
            SetButtonHeight(-m_PressDistance);
            m_OnPress.Invoke();
            if (m_PressAudio != null) m_PressAudio.Play(); // ← Nuevo
            m_Selected = true;
        }

        void EndPress(SelectExitEventArgs args)
        {
            if (m_Hovered) m_OnRelease.Invoke();
            SetButtonHeight(0.0f);
            m_Selected = false;
        }

        void StartHover(HoverEnterEventArgs args)
        {
            m_Hovered = true;
            if (m_Selected) SetButtonHeight(-m_PressDistance);
        }

        void EndHover(HoverExitEventArgs args)
        {
            m_Hovered = false;
            SetButtonHeight(0.0f);
        }

        void SetButtonHeight(float height)
        {
            if (m_Button == null) return;
            Vector3 newPosition = m_Button.localPosition;
            newPosition.y = height;
            m_Button.localPosition = newPosition;
        }

        void OnDrawGizmosSelected()
        {
            var pressStartPoint = transform.position;
            var pressDownDirection = -transform.up;
            if (m_Button != null)
            {
                pressStartPoint = m_Button.position;
                pressDownDirection = -m_Button.up;
            }
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pressStartPoint, pressStartPoint + (pressDownDirection * m_PressDistance));
        }

        void OnValidate() { SetButtonHeight(0.0f); }
    }
}
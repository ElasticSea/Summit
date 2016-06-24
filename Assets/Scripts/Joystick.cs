using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace UnityEngine.UI
{
    [AddComponentMenu("UI/Joystick", 36), RequireComponent(typeof (RectTransform))]
    public class Joystick : UIBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField, Tooltip("The child graphic that will be moved around")] private RectTransform _joystickGraphic;

        [SerializeField] private Vector2 _axis;

        [SerializeField, Tooltip("How fast the joystick will go back to the center")] private float _spring = 25;

        [SerializeField, Tooltip("How close to the center that the axis will be output as 0")] private float _deadZone =
            .1f;

        [Tooltip("Customize the output that is sent in OnValueChange")] public AnimationCurve outputCurve =
            new AnimationCurve(new Keyframe(0, 0, 1, 1), new Keyframe(1, 1, 1, 1));

        public JoystickMoveEvent OnValueChange;

        private RectTransform _rectTransform;

        private bool _isDragging;

        [HideInInspector] private bool dontCallEvent;

        public RectTransform JoystickGraphic
        {
            get { return _joystickGraphic; }
            set
            {
                _joystickGraphic = value;
                UpdateJoystickGraphic();
            }
        }

        public float Spring
        {
            get { return _spring; }
            set { _spring = value; }
        }

        public float DeadZone
        {
            get { return _deadZone; }
            set { _deadZone = value; }
        }

        public Vector2 JoystickAxis
        {
            get
            {
                var outputPoint = _axis.magnitude > _deadZone ? _axis : Vector2.zero;
                var magnitude = outputPoint.magnitude;

                outputPoint *= outputCurve.Evaluate(magnitude);

                return outputPoint;
            }
            set { SetAxis(value); }
        }

        public RectTransform rectTransform
        {
            get
            {
                if (!_rectTransform) _rectTransform = transform as RectTransform;

                return _rectTransform;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!IsActive())
                return;

            EventSystem.current.SetSelectedGameObject(gameObject, eventData);

            Vector2 newAxis = transform.InverseTransformPoint(eventData.position);

            newAxis.x /= rectTransform.sizeDelta.x*.5f;
            newAxis.y /= rectTransform.sizeDelta.y*.5f;

            SetAxis(newAxis);

            _isDragging = true;
            dontCallEvent = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position,
                eventData.pressEventCamera, out _axis);

            _axis.x /= rectTransform.sizeDelta.x*.5f;
            _axis.y /= rectTransform.sizeDelta.y*.5f;

            SetAxis(_axis);

            dontCallEvent = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
        }

        private void OnDeselect()
        {
            _isDragging = false;
        }

        private void Update()
        {
            if (_isDragging)
                if (!dontCallEvent)
                    if (OnValueChange != null) OnValueChange.Invoke(JoystickAxis);
        }

        private void LateUpdate()
        {
            if (!_isDragging)
                if (_axis != Vector2.zero)
                {
                    var newAxis = _axis - _axis*Time.unscaledDeltaTime*_spring;

                    if (newAxis.sqrMagnitude <= .0001f)
                        newAxis = Vector2.zero;

                    SetAxis(newAxis);
                }

            dontCallEvent = false;
        }

        protected override void OnValidate()
        {
            UpdateJoystickGraphic();
        }

        public void SetAxis(Vector2 axis)
        {
            _axis = Vector2.ClampMagnitude(axis, 1);

            var outputPoint = _axis.magnitude > _deadZone ? _axis : Vector2.zero;
            var magnitude = outputPoint.magnitude;

            outputPoint *= outputCurve.Evaluate(magnitude);

            if (!dontCallEvent)
                if (OnValueChange != null)
                    OnValueChange.Invoke(outputPoint);

            UpdateJoystickGraphic();
        }

        private void UpdateJoystickGraphic()
        {
            if (_joystickGraphic)
                _joystickGraphic.localPosition = _axis*Mathf.Max(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y)*
                                                 .5f;
        }

        [Serializable]
        public class JoystickMoveEvent : UnityEvent<Vector2>
        {
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            Vector2 newAxis = transform.InverseTransformPoint(eventData.position);

            newAxis.x /= rectTransform.sizeDelta.x * .5f;
            newAxis.y /= rectTransform.sizeDelta.y * .5f;

            SetAxis(newAxis);
            _isDragging = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDragging = false;
        }
    }
}

#if UNITY_EDITOR
internal static class JoystickGameObjectCreator
{
    [MenuItem("GameObject/UI/Joystick", false, 2000)]
    private static void Create()
    {
        var go = new GameObject("Joystick", typeof (Joystick));

        var canvas = Selection.activeGameObject ? Selection.activeGameObject.GetComponent<Canvas>() : null;

        Selection.activeGameObject = go;

        if (!canvas)
            canvas = Object.FindObjectOfType<Canvas>();

        if (!canvas)
        {
            canvas =
                new GameObject("Canvas", typeof (Canvas), typeof (RectTransform), typeof (GraphicRaycaster))
                    .GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }

        if (canvas)
            go.transform.SetParent(canvas.transform, false);

        var background = new GameObject("Background", typeof (Image));
        var graphic = new GameObject("Graphic", typeof (Image));

        background.transform.SetParent(go.transform, false);
        graphic.transform.SetParent(go.transform, false);

        background.GetComponent<Image>().color = new Color(1, 1, 1, .86f);

        var backgroundTransform = graphic.transform as RectTransform;
        var graphicTransform = graphic.transform as RectTransform;

        graphicTransform.sizeDelta = backgroundTransform.sizeDelta*.5f;

        var joystick = go.GetComponent<Joystick>();
        joystick.JoystickGraphic = graphicTransform;
    }
}
#endif
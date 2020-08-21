using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    /// <summary>
    /// Rigidbody сферы
    /// </summary>
    private Rigidbody rigidbody;
    /// <summary>
    /// Скорость перемещения
    /// </summary>
    [SerializeField] private float ballSpeed = 1f;
    /// <summary>
    /// Стартовое положение (для мыши)
    /// </summary>
    private Vector3 startPos;
    private void Start()
    {
        //Присваиваем rigidbody
        rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
#if UNITY_ANDROID || UNITY_IOS
        //Если есть касание
        if(Input.touchCount > 0)
        {
            //Сохраняем в переменную
            var touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                //Если касание в движении
                case TouchPhase.Moved:
                    //Снимаем ограничение на вращение
                    rigidbody.constraints = RigidbodyConstraints.None;
                    //Вычисляем смещение
                    offset =  new Vector3(touch.deltaPosition.x, transform.position.y, touch.deltaPosition.y);
                    //Перемещаем в зависимости от направления движения тача
                    rigidbody.AddForce(offset * (ballSpeed / 900));
                    break;
                //Если касание закончилось
                case TouchPhase.Ended:
                    //Замораживаем вращение
                    rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                    break;
            }
        }
#endif
    }
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
    //Когда пользователь зажал кнопку мыши
    private void OnMouseDown()
    {
        //Сохраняем стратовое положение
        startPos = Input.mousePosition;
    }
    //Когда пользователь двигает мышью
    private void OnMouseDrag()
    {
        //Снимаем ограничение на вращение
        rigidbody.constraints = RigidbodyConstraints.None;
        //Вычисляем смещение
        var offset = new Vector3(Input.mousePosition.x - startPos.x, transform.position.y, Input.mousePosition.y - startPos.y);
        //Двигаем сферу
        rigidbody.AddForce(offset * (ballSpeed / 900));
    }
    //Когда пользователь отпустил кнопку мыши
    private void OnMouseUp()
    {
        //Замораживаем вращение
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }
#endif
}
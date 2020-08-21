using System.Collections.Generic;
using UnityEngine;

public class FigureBehavior : MonoBehaviour
{
    /// <summary>
    /// Список всех частей фигуры
    /// </summary>
    private List<GameObject> parts = new List<GameObject>();
    /// <summary>
    /// Общий коллайдер фигуры
    /// </summary>
    private BoxCollider collider;
    private void Start()
    {
        //Присваиваем значение коллайдеру
        collider = GetComponent<BoxCollider>();
        //Для каждого дочернего объекта фигуры
        for(int i = 0; i < transform.childCount; i++)
        {
            //Добавляем в список
            parts.Add(transform.GetChild(i).gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Если сфера столкнулась с фигурой
        if(collision.gameObject.tag == "Player")
        {
            //Уничтожаем общий коллайдер
            Destroy(collider);
            //Для каждой части фигуры
            foreach(var part in parts)
            {
                //Делаем часть динамичной
                part.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
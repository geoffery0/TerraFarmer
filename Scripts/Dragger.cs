using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    private Vector3 _dragOffSet;
    private Camera _cam;

    [SerializeField] private float _speed = 10;

    void Awake(){
        _cam =  Camera.main;
    }

    void OnMouseDown() {
        _dragOffSet = transform.position- getMousePos();
    }
    void OnMouseDrag() {
        transform.position = Vector3.MoveTowards(transform.position,getMousePos() + _dragOffSet,_speed * Time.deltaTime);
    }

    Vector3 getMousePos(){
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}

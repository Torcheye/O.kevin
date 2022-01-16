using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncubationCharacter : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        transform.position = new Vector2(Screen.width * UISettings.ObjectsSwipeMoveScalerToUI, transform.position.y);
    }
    
    
}

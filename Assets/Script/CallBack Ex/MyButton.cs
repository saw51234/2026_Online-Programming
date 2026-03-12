using System;
using UnityEngine;

public class MyButton : MonoBehaviour
{
    public Action OnPressed;

    private bool canPress = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!canPress) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("버튼을 눌렀다.");
            canPress = false;
            OnPressed.Invoke();
        }
    }
}

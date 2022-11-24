using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera.Follow = NewPlayer.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

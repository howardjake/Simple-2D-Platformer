using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SceneCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtual;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineVirtual.Follow = NewPlayer.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

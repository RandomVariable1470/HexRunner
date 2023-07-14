using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReference : MonoBehaviour
{
    [Header("Base")]
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineImpulseSource impulseSource;

    [Header("Color")]
    public GameObject RedColor;
    [HideInInspector] public bool red;
    public GameObject OrangeColor;
    [HideInInspector] public bool orange;
    public GameObject BlueColor;
    [HideInInspector] public bool blue;
    public GameObject GreenColor;
    [HideInInspector] public bool green;

    [Header("GroundMat")]
    public Material[] groundMat;
    public MeshRenderer[] platform;
    public MeshRenderer platformEnd;
}
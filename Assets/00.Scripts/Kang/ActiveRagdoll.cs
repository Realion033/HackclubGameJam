using System.Collections.Generic;
using UnityEngine;

public class ActiveRagdoll : MonoBehaviour
{
    public float torqueForce = 100f;     // AddTorque에 사용될 힘 (프로퍼티로 설정 가능)
    public float force = 100f;     // AddTorque에 사용될 힘 (프로퍼티로 설정 가능)
    public float speedLim = 10f;     // AddTorque에 사용될 힘 (프로퍼티로 설정 가능)

    public List<Rigidbody> ragdollBodies = new List<Rigidbody>(); // CharacterJoint가 있는 Rigidbody 리스트
    public List<Transform> referenceBones = new List<Transform>(); // 기준 Transform의 자식들
    public List<Rigidbody> hip;
    public List<Transform> referenceHip;
    public bool active = true;
    private void Start()
    {

    }

    private void Update()
    {
        if (active)
        {
            for (int i = 0; i < referenceBones.Count; i++)
            {
                Transform referenceBone = referenceBones[i];
                Rigidbody targetRigidbody = ragdollBodies[i];


                Quaternion currentRotation = targetRigidbody.rotation;

                // 목표 회전 상태
                Quaternion targetRotation = referenceBone.rotation;

                // Quaternion 간의 차이 계산
                Quaternion deltaRotation = targetRotation * Quaternion.Inverse(currentRotation);

                // Quaternion에서 회전축과 각도 추출
                deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);

                // 회전축과 각도 기반으로 토크 계산
                if (angle > 180f) angle -= 360f; // -180도 ~ 180도로 정규화
                Vector3 torque = axis * angle * Mathf.Deg2Rad * torqueForce;

                // 토크 적용
                targetRigidbody.AddTorque(torque, ForceMode.Force);
            }
            for (int i = 0; i < referenceHip.Count; i++)
            {
                hip[i].AddForce((referenceHip[i].position - hip[i].position) * force);
            }
        }
    }
    private void LateUpdate()
    {
        for (int i = 0; i < ragdollBodies.Count; i++)
        {
            if(ragdollBodies[i].linearVelocity.magnitude > speedLim)
            {
                ragdollBodies[i].linearVelocity = ragdollBodies[i].linearVelocity.normalized * speedLim;
            }
        }
        for (int i = 0; i < hip.Count; i++)
        {
            if (hip[i].linearVelocity.magnitude > speedLim)
            {
                hip[i].linearVelocity = hip[i].linearVelocity.normalized * speedLim;
            }
        }
    }
}
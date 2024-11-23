using System.Collections.Generic;
using UnityEngine;

public class ActiveRagdoll : MonoBehaviour
{
    public float torqueForce = 100f;     // AddTorque�� ���� �� (������Ƽ�� ���� ����)
    public float force = 100f;     // AddTorque�� ���� �� (������Ƽ�� ���� ����)
    public float speedLim = 10f;     // AddTorque�� ���� �� (������Ƽ�� ���� ����)

    public List<Rigidbody> ragdollBodies = new List<Rigidbody>(); // CharacterJoint�� �ִ� Rigidbody ����Ʈ
    public List<Transform> referenceBones = new List<Transform>(); // ���� Transform�� �ڽĵ�
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

                // ��ǥ ȸ�� ����
                Quaternion targetRotation = referenceBone.rotation;

                // Quaternion ���� ���� ���
                Quaternion deltaRotation = targetRotation * Quaternion.Inverse(currentRotation);

                // Quaternion���� ȸ����� ���� ����
                deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);

                // ȸ����� ���� ������� ��ũ ���
                if (angle > 180f) angle -= 360f; // -180�� ~ 180���� ����ȭ
                Vector3 torque = axis * angle * Mathf.Deg2Rad * torqueForce;

                // ��ũ ����
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
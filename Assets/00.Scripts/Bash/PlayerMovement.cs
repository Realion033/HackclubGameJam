using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace bash
{
    public class PlayerMovement : MonoBehaviour
    {

        public Rigidbody rigidbody;

        public Transform cameraRoot;

        public bool isSliding;

        [SerializeField]
        float _maxSpeed = 10, _accelation = 25, _jumpPower = 5, _damp = 3, _gravity = -11, _plHeight = 2, _plRaius = 0.26f; //PlayerState�� �� ������
        [SerializeField]
        float _mouseSpeed = 5f;//PlayerSettingMing ���� ���ߵ�
        [SerializeField]
        float _runSpeedMulti = 2f;
        private bool _isRunning;


        public bool _isCanJump, _isGround;

        [SerializeField]
        LayerMask _whatIsGround;

        Vector2 _mouseTmp;
        Vector3 _movDir;

        RaycastHit _groundCheck;

        Collider[] _groundCheckCols = new Collider[1];
        void Start()
        {
            RPlayerMana.Instance.jumpAction += Jump;
        }

        // Update is called once per frame
        private void Update()
        {
            RotateCamera();
        }
        void FixedUpdate()
        {
            _isCanJump = false;
            _isGround = false;

            Vector3 input = BashUtils.V2toV3(RPlayerMana.Instance.playerInput.movement);
            //_movDir = BashUtils.V3X0Z(cameraRoot.TransformVector(input)).normalized;
            input = (Quaternion.Euler(0, _mouseTmp.x, 0) * input);

            //���� ��� ���� ������ �����ϱ� ���� ��� ����(Overlap)�� ������ ���� ����(SphereCast)�� ��������.
            if (Physics.OverlapSphereNonAlloc(transform.position - Vector3.up * _plHeight / 2, _plRaius + 0.1f, _groundCheckCols, _whatIsGround) > 0) //������ üũ(�γ��� ����)
            {
                _isCanJump = true;

                if (Physics.SphereCast(transform.position, _plRaius, -transform.up, out _groundCheck, _plHeight / 2, _whatIsGround))//������ üũ(������ ����)
                {
                    _isGround = true;

                    //OnGorund(
                    //����O ����O
                    MoveOnGorund(ref input);
                }
                else
                {
                    //OnAirAndGround?
                    //����x ����O

                }

            }
            else
            {
                //OnAir
                //����X ����X

            }

            //    _movDir = input * (Mathf.Lerp(1, 0, (Vector3.Project(input, rigidbody.velocity) + rigidbody.velocity).magnitude / _maxSpeed)
            //+ Vector3.Project(input, -rigidbody.velocity.normalized).magnitude);
            _movDir = input * Mathf.Lerp(1, 0, (Vector3.Project(input, rigidbody.linearVelocity) + rigidbody.linearVelocity).magnitude / (_maxSpeed* (RPlayerMana.Instance.playerInput.isSliding ? 1: _runSpeedMulti)));


            //_movDir += Vector3.Project(input, Quaternion.Euler(0, 90, 0) * rigidbody.velocity.normalized);
            //_movDir += Vector3.Project(input, Quaternion.Euler(0, 270, 0) * rigidbody.velocity.normalized);

            //rigidbody.linearVelocity += _movDir;
            rigidbody.AddForce(_movDir,ForceMode.Impulse);

        }

        void Jump()
        {
            if (_isCanJump)
                rigidbody.AddForce(transform.up * _jumpPower, ForceMode.Impulse);
        }

        void MoveOnGorund(ref Vector3 input)
        {
            Vector3 horizontalSpeed = BashUtils.V3X0Z(rigidbody.linearVelocity);
            rigidbody.linearVelocity = Vector3.up * rigidbody.linearVelocity.y
               + Vector3.Lerp(horizontalSpeed, Vector3.zero, _damp * Time.fixedDeltaTime);
            //����

            input = Vector3.ProjectOnPlane(input, _groundCheck.normal);
            //�������� ��� ���� �� �ְ��ϴ� �ڵ�;
        }

        void MoveOnAir()
        {

        }

        void RotateCamera()
        {
            //_mouseTmp += RPlayerMana.Instance.playerInput.mouseMov*_mouseSpeed;
            _mouseTmp.x += Input.GetAxisRaw("Mouse X") * _mouseSpeed * Time.deltaTime;
            _mouseTmp.y -= Input.GetAxisRaw("Mouse Y") * _mouseSpeed * Time.deltaTime;
            _mouseTmp.y = Mathf.Clamp(_mouseTmp.y, -89, 89);

            cameraRoot.rotation = Quaternion.Euler(_mouseTmp.y, _mouseTmp.x, 0);
        }

    }

}

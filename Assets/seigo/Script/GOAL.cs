using UnityEngine;

public class GOAL : MonoBehaviour
{
    public GameManager gameManager;  // �Q�[���}�l�[�W���[�i���U���g��\����������j

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // �v���C���[���S�[���ɓ��B������
        {
            gameManager.ShowResult();  // ���U���g��ʂ�\������
        }
    }
}

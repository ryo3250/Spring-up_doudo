using UnityEngine;
using UnityEngine.SceneManagement;  // �V�[���J�ڂɕK�v

public class GameManager : MonoBehaviour
{
    public GameObject resultPanel;  // ���U���g��ʂ�UI�i�p�l���Ȃǁj
    public Text resultText;         // ���ʂ�\������e�L�X�g

    public void ShowResult()
    {
        // ���U���g��ʂ�\��
        resultPanel.SetActive(true);
        resultText.text = "Congratulations! You've finished the race!";  // ���ʃe�L�X�g

        // �K�v�ɉ����ă��U���g�Ɋ�Â��ǉ��̏����i�^�C���\����X�R�A�Ȃǁj���ł��܂�
    }

    // �{�^���ȂǂŃ��U���g��ɍăX�^�[�g����ꍇ
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // ���݂̃V�[�����ēǂݍ���
    }
}

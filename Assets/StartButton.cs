using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{

    // �o�Ӥ�k�|�b���s�Q�I���ɳQ�I�s
    public void OnButtonClicked()
    {
        // �ϥ� SceneManager ��������
        keywordscene.r = false;
        StartCoroutine(LoadNextSceneAfterDelay(0.01f)); // Using StartCoroutine for delay

    } 

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}


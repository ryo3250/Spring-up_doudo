using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManeger : MonoBehaviour
{
    /// <summary>
    /// 指定したシーン名に切り替える
    /// </summary>
    /// <param name="sceneName"></param>
    // Update is called once per frame
    public void ChangeScene(string sceneName) 
    {
        //入力チェック
        if (string.IsNullOrEmpty(sceneName)) 
        {
            Debug.LogError("シーン名が指定されていません。");//シーンがないかんじだと処理を中断する
            return;
        }

        //シーンがBuild Settingsに登録されているか確認
        if (!IsSceneInBuild(sceneName))
        {
            Debug.LogError($"シーン'{sceneName}'がBuild Settingsに登録されていません");
            return;
        }

        //シーン切り替え
        SceneManager.LoadScene(sceneName);
    }
    /// <summary>
    /// 指定シーンがBuild settings に登録されているか確認
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private bool IsSceneInBuild(string sceneName) 
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;//Build Settingsに登録されているシーンの数を取得する
        for (int i = 0; i < sceneCount; i++)//登録されている数だけループ
        { 
            string path = SceneUtility.GetScenePathByBuildIndex(i);//Build Settings のi番目のシーンのファイルパスを取得
            string name = System.IO.Path.GetFileNameWithoutExtension(path);//ファイルパスから拡張子(.unity)を除いたシーン名だけを取り出す

            if(name == sceneName)//取り出したシーン名と引数のSceneNameが一致すれば、trueを返す
                return true;
        }
        return false;//ループでどのシーン名とも一致してない場合、falseを返す
    }
    
}

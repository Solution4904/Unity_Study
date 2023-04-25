using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {
    #region Variable
    private Auth auth;

    private readonly string ID_FIELD = "ID FIELD";
    private readonly string PASSWORD_FIELD = "PASSWORD FIELD";
    private readonly string RESISTER_BUTTON = "RESISTER BUTTON";
    private readonly string LOGIN_BUTTON = "LOGIN BUTTON";
    private readonly string RESULT_TEXT = "RESULT TEXT";

    private Dictionary<string, TMP_InputField> inputFieldDic = new Dictionary<string, TMP_InputField>();
    private Dictionary<string, Button> buttonDic = new Dictionary<string, Button>();

    private TextMeshProUGUI resultText = null;
    #endregion


    #region Life Cycle
    private void Awake() {
        auth = new Auth();

        CachingComponents();

        AddInputFieldToDic(ID_FIELD, GameObject.Find(ID_FIELD));
        AddInputFieldToDic(PASSWORD_FIELD, GameObject.Find(PASSWORD_FIELD));

        AddButtonToDic(RESISTER_BUTTON, GameObject.Find(RESISTER_BUTTON));
        AddButtonToDic(LOGIN_BUTTON, GameObject.Find(LOGIN_BUTTON));

        SetButtonsEvent();
    }
    #endregion

    #region Essential Function
    private void CachingComponents() {
        resultText = null ?? GameObject.Find(RESULT_TEXT).GetComponent<TextMeshProUGUI>();
    }
    #endregion


    #region Definition Function
    /// <summary>
    /// ��ư ��� ���� ����� ǥ��
    /// </summary>
    /// <param name="result"></param>
    private void ShowResult(string result) {
        Debug.Log($"ShowResult => {result}");

        resultText.text = result;
    }

    /// <summary>
    /// ��ư ��ųʸ��� ����ִ� ��ư���� �̺�Ʈ�� ����
    /// </summary>
    private void SetButtonsEvent() {
        foreach (var item in buttonDic) item.Value.onClick.RemoveAllListeners();

        buttonDic[RESISTER_BUTTON].onClick.AddListener(
            () => {
                var id = inputFieldDic[ID_FIELD].text;
                var pw = inputFieldDic[PASSWORD_FIELD].text;

                auth.Resister(id, pw,
                    (result) => {
                        ShowResult(result);
                    });
            });

        buttonDic[LOGIN_BUTTON].onClick.AddListener(
            () => {
                var id = inputFieldDic[ID_FIELD].text;
                var pw = inputFieldDic[PASSWORD_FIELD].text;

                auth.Login(id, pw,
                    (result) => {
                        ShowResult(result);
                    });
            });
    }

    /// <summary>
    /// TextMesh Pro InputField ������Ʈ���� ��ųʸ��� �߰�
    /// </summary>
    /// <param name="key">��Ͻ�ų Key</param>
    /// <param name="inputfield">��Ͻ�ų InputField ������Ʈ</param>
    private void AddInputFieldToDic(string key, GameObject inputfield) {
        if (inputFieldDic.ContainsKey(key))
            Debug.Log($"Duplication Key => {key}");
        else
            inputFieldDic.Add(key, inputfield.GetComponent<TMP_InputField>());
    }

    /// <summary>
    /// Button ������Ʈ���� ��ųʸ��� �߰�
    /// </summary>
    /// <param name="key">��Ͻ�ų Key</param>
    /// <param name="button">��Ͻ�ų Button ������Ʈ</param>
    private void AddButtonToDic(string key, GameObject button) {
        if (buttonDic.ContainsKey(key))
            Debug.Log($"Duplication Key => {key}");
        else
            buttonDic.Add(key, button.GetComponent<Button>());
    }
    #endregion
}
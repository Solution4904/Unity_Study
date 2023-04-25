using Firebase.Auth;
using System.Text;

public class Auth {
    #region Variable
    private FirebaseAuth auth;
    public delegate void Callback<T>(T result);

    private StringBuilder result = new StringBuilder();
    #endregion

    #region Life Cycle
    public Auth() {
        auth = FirebaseAuth.DefaultInstance;
    }
    #endregion

    #region Definition Function
    /// <summary>
    /// ���� ��� �Լ�.
    /// �ݹ����� �޴� �Լ��� UI ������ ��쿩�� async�� ����
    /// </summary>
    /// <param name="id">���̵�</param>
    /// <param name="pw">��й�ȣ</param>
    /// <param name="cb">�ݹ��Լ�</param>
    public async void Resister(string id, string pw, Callback<string> cb) {
        result.Clear();

        await auth.CreateUserWithEmailAndPasswordAsync(id, pw).ContinueWith(
           task => {
               if (!task.IsCanceled && !task.IsFaulted)
                   result.Append("registration success");
               else
                   result.Append("registration failed");
           });

        cb.Invoke(result.ToString());
    }

    /// <summary>
    /// �α��� ��� �Լ�.
    /// �ݹ����� �޴� �Լ��� UI ������ ��쿩�� async�� ����
    /// </summary>
    /// <param name="id">���̵�</param>
    /// <param name="pw">��й�ȣ</param>
    /// <param name="cb">�ݹ��Լ�</param>
    public async void Login(string id, string pw, Callback<string> cb) {
        result.Clear();

        await auth.SignInWithEmailAndPasswordAsync(id, pw).ContinueWith(
            task => {
                if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
                    result.Append("login success");
                else
                    result.Append("login failed");
            });

        cb.Invoke(result.ToString());
    }
    #endregion
}
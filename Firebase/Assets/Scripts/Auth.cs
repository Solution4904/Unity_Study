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
    /// 가입 통신 함수.
    /// 콜백으로 받는 함수는 UI 제어인 경우여서 async로 선언
    /// </summary>
    /// <param name="id">아이디</param>
    /// <param name="pw">비밀번호</param>
    /// <param name="cb">콜백함수</param>
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
    /// 로그인 통신 함수.
    /// 콜백으로 받는 함수는 UI 제어인 경우여서 async로 선언
    /// </summary>
    /// <param name="id">아이디</param>
    /// <param name="pw">비밀번호</param>
    /// <param name="cb">콜백함수</param>
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

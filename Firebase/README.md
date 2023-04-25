# Firebase Auth Practice
* Firebase SDK를 사용해 유니티 엔진 내에서 회원가입과 로그인하는 실습 수행.
<br><br>
-----
## 1.1 가입
<img src="https://user-images.githubusercontent.com/47467306/234209766-b2e7a77d-137c-4138-8747-7acf2378bbfc.jpg"></img><br>
가입 하려는 이메일 아이디와 비밀번호를 입력 후 'Resister' 버튼 클릭으로 가입.<br><br>

<img src="https://user-images.githubusercontent.com/47467306/234209815-cab206ae-73ff-4406-900a-36a800896b3c.jpg"></img><br>
Firebase console에서 확인해보면 정상적으로 작동되었음을 확인.

## 1.2 로그인
가입에 사용되었던 아이디와 비밀번호를 정확히 입력 후 'Login' 버튼 클릭 시 로그인 시도.<br><br>

<img src="https://user-images.githubusercontent.com/47467306/234209800-a8cc4dd8-1a7b-48b1-9f76-a24b0a648535.jpg"></img><br>
<b>로그인 성공</b><br>
입력된 값과 일치하는 계정 정보가 있을 경우 로그인 성공.<br><br>

<img src="https://user-images.githubusercontent.com/47467306/234209585-baa826d7-a325-4d24-a2ca-35debdd9f121.jpg"></img><br>
<b>로그인 실패</b><br>
만약 입력된 값이 존재하지 않거나 다를 경우 로그인 실패.

## 2 기록
* Auth.class에 MonoBehaviour는 필요 없다고 판단, Firebase와 통신하는 함수만 존재하는 class로 작성.<br>
* UI에 대한 권한이 없는 Auth Class에서 UI에 접근할 수 있도록 UIManager에서 통신 요청 시 통신 결과 값이 리턴되었을 경우 UI에 결과 값이 갱신되어 표시될 수 있도록 콜백 함수 사용.
* 단, Firebase SDK에 통신 구문이 별도의 Task를 사용하는 탓에 UI에 접근할 수 없는 문제가 발생했고 이를 해결하기 위해 Auth.class에 통신 함수들을 async로 선언하고 await을 통해 콜백 함수를 실행함으로서 해결.

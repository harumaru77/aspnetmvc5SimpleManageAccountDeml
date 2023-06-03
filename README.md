# aspnetmvc5SimpleManageAccountDeml

ASP.NET MVC5 기반으로 간단하게 Login/Logout 구현 샘플입니다.

주요 사항
1. Web.config : 아래의 설정을 추가한다.
```
<authentication mode="Forms">
  <forms defaultUrl="/Home/Profile" loginUrl="/Home/Index" slidingExpiration="true" timeout="2880"></forms>
</authentication>
```

2. Account 클래스를 생성한다.
```
public class UserInfo
{
    public int UserId { get; set; }
    [Required(ErrorMessage = "필수입력사항입니다.")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "필수입력사항입니다.")]
    public string UserPassword { get; set; }
    [Required(ErrorMessage = "필수입력사항입니다.")]
    [Compare("UserPassword", ErrorMessage = "비밀번호가 일치하지 않습니다.")]
    public string ConfirmUserPassword { get; set; }
}
```

3. 로그인 정보(UserName, UserPassword)가 일치하다면 인증된 사용자의 폼인증 쿠키를 만든다.
```
FormsAuthentication.SetAuthCookie(userInfo.UserName, false);
```

4. 사용자의 폼인증 쿠키를 삭제하여 로그아웃 처리한다.
```
FormsAuthentication.SignOut();
```

5. 사용자가 로그인 되어 있는지 확인한다.
```
if(User.Identity.IsAuthenticated)
  // 로그인되었을 때(인증된 사용자)의 처리
else
  // 로그인되지 않았을 때(인증되지 않은 사용자)의 처리
```

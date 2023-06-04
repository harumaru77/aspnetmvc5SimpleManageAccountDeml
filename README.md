# aspnetmvc5SimpleManageAccountDeml

## ASP.NET MVC5 기반으로 간단하게 Login/Logout 구현 샘플

주요 사항
1. Web.config : 아래의 설정을 추가한다.
```
<system.web>
  <authentication mode="Forms">
    <forms defaultUrl="/Home/Profile" loginUrl="/Home/Index" slidingExpiration="true" timeout="2880"></forms>
  </authentication>
</system.web>
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

## ASP.NET MVC5 기반 Role에 따른 권한 제어 구현 
주요사항
1. System.Web.Security.RoleProvier를 상속받은 Custom RoleProvider 클래스를 생성한다.
2. 구현해야 하는 여러 함수가 있으나 'public override string[] GetRolesForUser(string username)' 함수만 구현해도 된다.
  - username으로 username에 할당되어 있는 rolename string 배열을 리터한다.
3. Web.config : 아래의 설정을 추가한다.
```
<system.web>
  <roleManager defaultProvider="myRolePrv" enabled="true">
    <providers>
      <clear/>
      <add name="myRolePrv" type="WebApplication4.Models.MyRoleProvider"/>
    </providers>
  </roleManager>
</system.web>
```
4. Controller에 Role 권한 설정은 아래와 같이 한다.
```
/// Profile Action에는 Admin, Customer Role 권한이 있으면 접근이 가능
/// 즉 Admin, Customer Role 권한이 있으면 접근이 가능
[Authorize(Roles = "Admin, Customer")]
public ActionResult Profile()
{
    return View();
}

/// Contact Action에는 Admin Role 권한이 있으면 접근이 가능
/// 즉 Admin Role 권한자만 접근이 가능
[Authorize(Roles = "Admin")]
public ActionResult Contact()
{
    return View();
}

```
5. View 화면에서는 아래와 같이 한다.
```
<div class="navbar-collapse collapse">
    <ul class="nav navbar-nav">
        <li>@Html.ActionLink("홈", "Index", "Home")</li>
        @if (User.Identity.IsAuthenticated)
        {
            <li>@Html.ActionLink("정보", "About", "Home")</li>
            if (User.IsInRole("Admin"))
            {
                <li>@Html.ActionLink("연락처", "Contact", "Home")</li>
            }
        }
    </ul>
</div>
```
- 정보와 연락처 링크는 로그인한 사용자에게만 보여지만 연락처  로그인한 사용자의 ROle에 Admin 권한이 있어야만 보여진다.

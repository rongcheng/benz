<%
'模拟用户验证，密码均未加密，
'返回值: 1 代表成功 0 代表验证失败
'例子中以a开头的用户名为正确的用户
dim ret
ret="0"

dim userName
dim password

userName = request("username")
password = request("password")

if left(username,1)="a" then
    ret="1"
end if
response.write ret
%>
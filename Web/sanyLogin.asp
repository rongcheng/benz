<%
'ģ���û���֤�������δ���ܣ�
'����ֵ: 1 ����ɹ� 0 ������֤ʧ��
'��������a��ͷ���û���Ϊ��ȷ���û�
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
# smtplib 用于邮件的发信动作
import smtplib#类似于js import
from email.mime.text import MIMEText#使用这种语法格式的 import 语句，只会导入模块中指定的成员，而不是全部成员。同时，当程序中使用该成员时，无需附加任何前缀，直接使用成员名（或别名）即可。
# email 用于构建邮件内容
from email.header import Header

# 发信方的信息：发信邮箱，QQ 邮箱授权码
from_addr = '459942586@qq.com'
password = 'nxpqlxnezbcbbida'

# 收信方邮箱
to_addr = 'yuleen_@outlook.com'

# 发信服务器
smtp_server = 'smtp.qq.com'



def sendMail(sendMsg):
    # 邮箱正文内容，第一个参数为内容，第二个参数为格式(plain 为纯文本)，第三个参数为编码
    msg = MIMEText(sendMsg, 'plain', 'utf-8')
    # 邮件头信息
    msg['From'] = Header(from_addr)
    msg['To'] = Header(to_addr)
    msg['Subject'] = Header('豆瓣猫组开车监控')
    # 开启发信服务，这里使用的是加密传输
    server=smtplib.SMTP_SSL(smtp_server)
    server.connect(smtp_server, 465)
    # 登录发信邮箱
    server.login(from_addr, password)
    # 发送邮件
    server.sendmail(from_addr, to_addr, msg.as_string())
    # 关闭服务器
    server.quit()
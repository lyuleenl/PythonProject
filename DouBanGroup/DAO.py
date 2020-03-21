#SQL命令
SQL_INSERT = "INSERT INTO DOUBANGROUP(Time, Title,Url) VALUES (%s, %s,%s);"
SQL_VERIFY='SELECT * FROM DOUBANGROUP WHERE Title=%s AND Url=%s'

#MySQL连接与命令处理

# 导入pymysql模块
import pymysql

cursor=None
conn=None
#连接MySQL
def mySQLConn():
    # 连接database
    global conn
    conn = pymysql.connect(host='127.0.0.1', user='root',password='root',database='pythonlocaldata',charset='utf8')
    # 得到一个可以执行SQL语句的光标对象
    global cursor
    cursor = conn.cursor()
#插入新数据
def insertData(time,title,url):
    global cursor
    cursor.execute(SQL_INSERT, [time, title,url])
    global conn
    conn.commit()
#核验数据是否已存在 存在返回True 不存在返回False
def verifyData(title,url):
    global cursor
    cursor.execute(SQL_VERIFY,[title,url])
    results = cursor.fetchall()
    if results ==():
        return False
    else:
        return True
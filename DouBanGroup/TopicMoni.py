#豆瓣小组话题监控
#urllib提供了一系列用于操作URL的功能。
from urllib import request
#BeautifulSoup最主要的功能是从网页抓取数据，Beautiful Soup自动将输入文档转换为Unicode编码，输出文档转换为utf-8编码。
from bs4 import BeautifulSoup
#正则表达式
import re
#时间函数
import time
#自定义 发送邮件模块
import SendMail
#自定义 连接数据库模块
import DAO
#主入口  实际上不是...未完善功能
import Main
#消息缓存
msg=[]
#本地存储缓存
getWebData={}
#是否使用MySQL存储
MySQLSWH=Main.isMySQL
#开始抓取
craw=False


#匹配关键字 若匹配到返回True 否则返回False
def compilText(pattern,compilStr):
    return pattern.search(compilStr) is not None

#解析url地址 返回utf-8解码信息
def analyUrl(url):
    header={'User-Agent': 'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0'}
    #发送访问请求  此处header作用为模拟浏览器访问  部分网页反爬虫会检测访问源信息
    _tmpRes=request.Request(url=url,headers=header)
    _req=request.urlopen(_tmpRes)
    #网页utf-8解码
    html=_req.read().decode('utf-8')
    return html

#核心逻辑 分析网页信息并匹配关键字
def analyAndCompile(url,firstKey,secondKey):
    global msg
    global craw
    # htmlUrl = analyUrl(url)
    #提取Html数据
    #二级匹配关键字
    pattern=re.compile(secondKey)
    #循环次数
    flag=0
    #while 循环  10s一次  重复匹配信息
    while (1):
        craw=True
        flag += 1
        try:
         curtime=time.strftime("%Y-%m-%d %H:%M:%S", time.localtime())
         print('第' + str(flag) + '次 ' +curtime )
         msg.append('第' + str(flag) + '次 ' +curtime+'\n')
         #每次重新访问
         htmlUrl = analyUrl(url)
         html = BeautifulSoup(htmlUrl, 'html.parser')
         #信息中包含'a'Tag和title内容与一级匹配关键字相同的提取
         for link in html.find_all('a',title=re.compile(firstKey)):
            #获取title内容
           link_title=link.get('title')
            #获取网址
           info_link = link.get('href')
            #compilText判断是否与二级关键字匹配    关键字匹配且之前没出现过即为所需信息
           if compilText(pattern,link_title) and not isMySQL(MySQLSWH,'check',link_title,info_link):#DAO.verifyData(link_title,info_link):
              print('      ·出现新车：'+link_title+'\n      地址：'+info_link)
              msg.append('      ·出现新车：'+link_title+'\n      地址：'+info_link+'\n')
              #发送邮件
              SendMail.sendMail('出现新车：'+link_title+'\n地址：'+info_link)
              # DAO.insertData(curtime,link_title,info_link)
              isMySQL(MySQLSWH,'add',curtime,link_title,info_link)
           elif isMySQL(MySQLSWH,'check',link_title,info_link): #DAO.verifyData(link_title,info_link):
               msg.append('      ·已经出现过的车' + link_title+'\n')
               print('      .已经出现过的车')
           else:
               msg.append('      ·无新数据'+link_title+'\n')
               print('      ·无新数据'+link_title)
         craw = False
         time.sleep(10)
         #异常获取
        except Exception as e:
            import traceback
            print(traceback.format_exc())
            print('异常')
            time.sleep(10)

#判断数据是否存入MySQL 或者为缓存
def isMySQL(on_off,add_check,*tmp):
    if on_off=='off':
        return noMySQL(add_check,*tmp)
    if add_check=='check':
       return  DAO.verifyData(tmp[0], tmp[1])
    if add_check=='add':
       return DAO.insertData(tmp[0], tmp[1],tmp[2])


#缓存处理法
def noMySQL(add_check,*tmp):
    global getWebData
    if add_check=='add':
        getWebData[tmp[2]]=tmp[1]
        return True
    elif add_check=='check':
        return tmp[1] in getWebData

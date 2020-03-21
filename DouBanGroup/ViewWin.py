#视图模块
#python 进行窗口视窗设计的模块
import tkinter as tk
#自定义模块 UI事件处理
import ViewFun
#多线程调用处理
import threading
#自定义模块  抓取逻辑
import TopicMoni
#时间模块
import time


#消息传递
msgText=TopicMoni.msg



#实例化 窗口对象
window=tk.Tk()
#窗口名
window.title('网页动态监听')
#设置窗口大小
window.geometry('600x400')
#设定窗口元素标签
tag1=tk.Label(window, text='你好，请按照规则填写筛选关键字', font=('Arial', 12), width=30, height=2)
#放置标签
tag1.pack()

#接收关键字  已填写默认内容
urlText=tk.StringVar(value='https://www.douban.com/group/656297/')
firstText=tk.StringVar(value='【开车】')
secondText=tk.StringVar(value='猫砂')
#Entry是tkinter类中提供的的一个单行文本输入域

#输入框
# e1 = tk.Entry(window, show='*', font=('Arial', 14))   # 显示成密文形式
urlKey= tk.Entry(window, textvariable=urlText,show=None, font=('Arial', 14))
firstKey = tk.Entry(window, textvariable=firstText,show=None, font=('Arial', 14))  # 显示成明文形式
secondKey= tk.Entry(window,textvariable=secondText,show=None, font=('Arial', 14))
urlKey.pack()
firstKey.pack()
secondKey.pack()
#输入框旁边标识
tk.Label(window, text='监听网址: ').place(x=60,y=43)
tk.Label(window, text='一级筛选字符: ').place(x=50,y=70)
tk.Label(window, text='二级筛选字符: ').place(x=50,y=95)

#Button组件
b = tk.Button(window, text="Start", command=lambda:[thread_it(ViewFun.Start,urlText.get(),firstText.get(),secondText.get()),thread_it(showMsg)])
b.pack()




#创建滚动条
s1=tk.Scrollbar()
s1.pack(side='right',fill='y') # side是滚动条放置的位置，上下左右。fill是将滚动条沿着y轴填充

#创建文本显示框
t1 = tk.Text(window,bg='black',width=60, height=15)
t1.config(fg='white')
t1.pack()
s1.config(command=t1.yview)
t1.insert('end', '网页结果在此处显示\n')

#将函数打包进线程
def thread_it(func, *args):
    # 创建
    t = threading.Thread(target=func, args=args)
    # 守护 !!!
    t.setDaemon(True)
    # 启动
    t.start()
    # 阻塞--卡死界面！

#消息开始显示  消息刷新
def showMsg():
    while (1):
        msg=TopicMoni.msg
        t1.config(state='normal')  # 设置为可编辑
        # t1.delete('1.0','end')  #清空文本框
        if len(msg)>0:
            t1.insert('end', msg[0])
            t1.see('end')  # 设置显示最末尾的数据
            t1.update()
            msg.pop(0)
        elif TopicMoni.craw:
            time.sleep(0.2)
        else:
            time.sleep(2)
        t1.config(state='disabled')  # 设置为无法编辑

#窗口循环显示
window.mainloop()
# 注意，loop因为是循环的意思，window.mainloop就会让window不断的刷新，如果没有mainloop,就是一个静态的window,传入进去的值就不会有循环，mainloop就相当于一个很大的while循环，有个while，每点击一次就会更新一次，所以我们必须要有循环
# 所有的窗口文件都必须有类似的mainloop函数，mainloop是窗口文件的关键的关键。

import DAO
import TopicMoni
import Main
#Button  Start 回调函数
def Start(urlTx,firstTx,secondTx):
    if Main.isMySQL=='on':
        DAO.mySQLConn()
    # html = TopicMoni.analyUrl(urlTx)
    # TopicMoni.analyAndCompile(html, firstTx, secondTx)
    TopicMoni.analyAndCompile(urlTx, firstTx, secondTx)
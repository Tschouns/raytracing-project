//  self

    #include "tcp-server.h"


//  app

    #include "tcp-socket.h"


//  qt

    #include <QTcpServer>


//  c++

    using namespace std;



TcpServer :: TcpServer (Connections * connptr, Canvas * cnvptr)
    : Server (connptr, cnvptr)
    , SrvPtr (new QTcpServer)
{
    connect (SrvPtr, & QTcpServer::newConnection,
        this, & TcpServer::SltNewConnection,
        Qt::QueuedConnection);

}



TcpServer :: ~ TcpServer ()
{
    delete SrvPtr;
}



Socket * TcpServer :: MakeConnection()
{
    QTcpSocket * qsockptr = SrvPtr->nextPendingConnection();
    Socket * sockptr = new TcpSocket (ConnPtr, qsockptr, CnvPtr);
    return sockptr;
}



void TcpServer :: listen (int newport)
{
    if (newport != port())
    {
        setPort (newport);
        if (SrvPtr->isListening())
            SrvPtr->close();
        SrvPtr->listen (QHostAddress::Any, newport);
    }
}



string TcpServer :: infoPort() const
{
    return "tcp: " + Server::infoPort();
}

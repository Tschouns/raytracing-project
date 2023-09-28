//  self

    #include "web-server.h"


//  qpp

    #include "web-socket.h"


//  qt

    #include <QWebSocketServer>


//  c++

    using namespace std;



WebServer :: WebServer (Connections * connptr, Canvas * cnvptr)
    : Server (connptr, cnvptr)
    , SrvPtr (new QWebSocketServer ("canvas", QWebSocketServer::NonSecureMode))
{
    connect (SrvPtr, & QWebSocketServer::newConnection,
        this, & WebServer::SltNewConnection,
        Qt::QueuedConnection);
}



WebServer :: ~ WebServer ()
{
    delete SrvPtr;
}



Socket * WebServer :: MakeConnection ()
{
    QWebSocket * qsockptr = SrvPtr->nextPendingConnection();
    Socket * sockptr = new WebSocket (ConnPtr, qsockptr, CnvPtr);
    return sockptr;
}



void WebServer :: listen (int newport)
{
    if (newport != port())
    {
        setPort (newport);
        if (SrvPtr->isListening())
            SrvPtr->close();
        SrvPtr->listen (QHostAddress::Any, newport);
    }
}



string WebServer :: infoPort () const
{
    return "web: " + Server::infoPort();
}

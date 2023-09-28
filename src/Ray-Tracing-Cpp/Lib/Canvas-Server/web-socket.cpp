//  self

    #include "web-socket.h"


//  qt

    #include <QWebSocket>


//  c++

    using namespace std;



WebSocket :: WebSocket (Connections * servptr, QWebSocket * sockptr, Canvas * cnvptr)
    : Socket (servptr, cnvptr)
    , SockPtr (sockptr)
{
    connect (SockPtr, & QWebSocket::textMessageReceived,
            this, & WebSocket::SltTextMessageReceived,
        Qt::QueuedConnection);

    connect (SockPtr, & QWebSocket::disconnected,
            this, & WebSocket::SltClose,
        Qt::QueuedConnection);
}



WebSocket :: ~ WebSocket ()
{
    delete SockPtr;
}



void WebSocket :: SltTextMessageReceived (const QString & text)
{
    string cmd = text.toStdString() + '\0';
    ProcessBuffer (cmd.data());
}



void WebSocket :: SltClose ()
{
    SockPtr->close();
    Close();
}

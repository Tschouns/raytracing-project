#ifndef WEB_SOCKET_H_INCLUDED
#define WEB_SOCKET_H_INCLUDED



//  app

    #include "socket.h"


//  qt

    class QWebSocket;



class WebSocket
    : public Socket
{
        Q_OBJECT


        QWebSocket * SockPtr;


    public:

        WebSocket (Connections * servptr, QWebSocket * sockptr, Canvas * cnvptr);
        ~ WebSocket ();

        WebSocket (const WebSocket &) = delete;
        WebSocket & operator = (const WebSocket &) = delete;


    private slots:

        void SltTextMessageReceived (const QString & text);
        void SltClose ();
};



#endif

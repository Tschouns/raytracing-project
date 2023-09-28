#ifndef TCP_SOCKET_H_INCLUDED
#define TCP_SOCKET_H_INCLUDED



//  app

    #include "socket.h"


//  qt

    class QTcpSocket;



class TcpSocket
    : public Socket
{
        Q_OBJECT


        QTcpSocket * SockPtr;


    public:

        TcpSocket (Connections * servptr, QTcpSocket * sockptr, Canvas * cnvptr);
        ~ TcpSocket ();

        TcpSocket (const TcpSocket &) = delete;
        TcpSocket & operator = (const TcpSocket &) = delete;


    private slots:

        void SltRead ();
        void SltClose ();
};



#endif

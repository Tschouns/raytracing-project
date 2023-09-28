#ifndef CONNECTIONS_H_INCLUDED
#define CONNECTIONS_H_INCLUDED



//  app

    class Canvas;
    class TcpServer;
    class WebServer;
    class Socket;


//  qt

    #include <QObject>


//  c++

    #include <set>



class Connections
    : public QObject
{
        Q_OBJECT


        typedef std::set <Socket *> SocketPtrSet;

        TcpServer * TcpSrvPtr;
        WebServer * WebSrvPtr;
        SocketPtrSet SockPtrs;


        static std::string LocalIpAddress ();


    public:

        Connections (Canvas * cnvptr);
        ~ Connections ();

        Connections (const Connections &) = delete;
        Connections & operator = (const Connections &) = delete;

        int tcpPort () const;
        int webPort () const;
        void listen (int tcpPort, int webPort);

        std::string info () const;


    signals:

        void sigRaise ();
        void sigInfo ();


    private slots:

        void SltNewConnection (Socket * sockptr);


    public slots:

        void sltCloseSockets ();
        void sltCloseSocket (Socket * sockptr);
};



#endif

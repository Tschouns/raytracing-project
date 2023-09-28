#ifndef TCP_SERVER_H_INCLUDED
#define TCP_SERVER_H_INCLUDED



//  app

    #include "server.h"


//  qt

    class QTcpServer;



class TcpServer
    : public Server
{
        Q_OBJECT


        QTcpServer * SrvPtr;


        virtual Socket * MakeConnection () override;


    public:

        TcpServer (Connections * connptr, Canvas * cnvptr);
        virtual ~ TcpServer ();

        TcpServer (const TcpServer &) = delete;
        TcpServer & operator = (const TcpServer &) = delete;

        void listen (int newport);
        virtual std::string infoPort () const override;
};



#endif

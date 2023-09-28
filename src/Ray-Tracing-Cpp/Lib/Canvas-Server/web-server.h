#ifndef WEB_SERVER_H_INCLUDED
#define WEB_SERVER_H_INCLUDED



//  app

    class QWebSocketServer;

    #include "server.h"



class WebServer
    : public Server
{
        QWebSocketServer * SrvPtr;


        virtual Socket * MakeConnection () override;


    public:

        WebServer (Connections * connptr, Canvas * cnvptr);
        ~ WebServer ();

        WebServer (const WebServer &) = delete;
        WebServer & operator = (const WebServer &) = delete;

        void listen (int port);
        virtual std::string infoPort () const override;
};



#endif

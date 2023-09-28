#ifndef SERVER_H_INCLUDED
#define SERVER_H_INCLUDED



//  app

    class Canvas;
    class Connections;
    class Socket;


//  qt

    #include <QObject>



class Server
    : public QObject
{
        Q_OBJECT


        int Port;
        int OpenConnections;


        virtual Socket * MakeConnection () = 0;


    protected:

        Connections * ConnPtr;
        Canvas * CnvPtr;


    public:

        Server (Connections * srvptr, Canvas * cnvptr);
        virtual ~ Server ();

        int port () const;
        void setPort (int port);
        virtual std::string infoPort () const;
        int openConnections () const;


    signals:

        void sigNewConnection (Socket * sockptr);


    protected slots:

        void SltNewConnection ();


    private slots:

        void SltCloseConnection ();
};



#endif

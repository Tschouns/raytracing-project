#ifndef CANVAS_H_INCLUDED
#define CANVAS_H_INCLUDED



//  qt

    class QEventLoop;
    class QTcpSocket;

    #include <QObject>


//  c++

    #include <string>



class CanvasClient
    : public QObject
{
        Q_OBJECT


        std::string Addr;
        int Port;
        QTcpSocket * SockPtr;
        int Attempt;
        int WaitTimeMs;


        void Send (const std::string & cmd);

        void Size (int w, int h);
        void Fill (double r, double g, double b);
        void Pixel (int x, int y, double r, double g, double b);

        void Main ();


    public:

        CanvasClient (const std::string & addr, int port);
        ~ CanvasClient ();

        CanvasClient (const CanvasClient &) = delete;
        CanvasClient & operator = (const CanvasClient &) = delete;


    signals:

        void sigFinished ();


    protected slots:

        virtual void SltRun ();

        void SltConnectToHost ();
        void SltDisconnectFromHost ();
        void SltConnected ();
        void SltDisconnected ();
        void SltError ();
};



#define main CanvasClient::Main



#endif

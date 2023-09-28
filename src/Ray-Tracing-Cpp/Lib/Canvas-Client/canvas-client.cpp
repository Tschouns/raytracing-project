//  self

    #include "canvas-client.h"


//  qt

    #include <QEventLoop>
    #include <QTcpSocket>
    #include <QTimer>


//  c++

    #include <iostream>
    #include <sstream>

    using namespace std;



CanvasClient :: CanvasClient (const string & addr, int port)
    : Addr (addr)
    , Port (port)
    , SockPtr (new QTcpSocket)
    , Attempt (0)
    , WaitTimeMs (0)
{
    connect (SockPtr, & QTcpSocket::connected,
        this, & CanvasClient::SltConnected,
        Qt::QueuedConnection);

    connect (SockPtr, & QTcpSocket::disconnected,
        this, & CanvasClient::SltDisconnected,
        Qt::QueuedConnection);

    connect (SockPtr, & QTcpSocket::errorOccurred,
            this, & CanvasClient::SltError,
        Qt::QueuedConnection);

    QTimer::singleShot (0, this, & CanvasClient::SltConnectToHost);
}



CanvasClient :: ~ CanvasClient ()
{
    delete SockPtr;
}



void CanvasClient :: Send (const string & cmd)
{
    string str = cmd + "\r\n";
    SockPtr->write (str.data(), str.length());
    SockPtr->waitForBytesWritten (-1);
}



void CanvasClient :: Size (int x, int y)
{
    ostringstream ostr;
    ostr << "size " << x << " " << y;
    Send (ostr.str());
}



void CanvasClient :: Fill (double r, double g, double b)
{
    ostringstream ostr;
    ostr << "fill " << r << " " << g << " " << b;
    Send (ostr.str());
}



void CanvasClient :: Pixel (int x, int y, double r, double g, double b)
{
    ostringstream ostr;
    ostr << "pixel " << x << " " << y << " " << r << " " << g << " " << b;
    Send (ostr.str());
}



void CanvasClient :: SltRun ()
{
    Main();
    QTimer::singleShot (0, this, & CanvasClient::SltDisconnectFromHost);
}



void CanvasClient :: SltConnectToHost ()
{
    cerr
        << "connecting to server " << Addr
        << ", port " << Port
        << ", attempt #" << ++Attempt
        << " ..." << endl;
    SockPtr->connectToHost ("127.0.0.1", 9012, QIODevice::WriteOnly);
}



void CanvasClient :: SltDisconnectFromHost ()
{
    cerr << "disconnecting ..." << endl;
    SockPtr->disconnectFromHost();
}



void CanvasClient :: SltConnected ()
{
    cerr << "connected" << endl;
    QTimer::singleShot (WaitTimeMs, this, & CanvasClient::SltRun);
}



void CanvasClient :: SltDisconnected ()
{
    cerr << "disconnected" << endl;
    SockPtr->disconnectFromHost();
    emit sigFinished();
}



void CanvasClient :: SltError ()
{
    cerr << "error: " << SockPtr->errorString().toStdString() << endl;
    SockPtr->abort();
    WaitTimeMs = 100;
    QTimer::singleShot (0, this, & CanvasClient::SltConnectToHost);
}

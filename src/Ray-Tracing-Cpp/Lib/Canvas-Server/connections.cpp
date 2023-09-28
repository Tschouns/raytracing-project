//  self

    #include "connections.h"


//  app

    #include "config.h"
    #include "socket.h"
    #include "tcp-server.h"
    #include "web-server.h"


//  qt

    #include <QNetworkInterface>
    #include <QTimer>


//  c++

    #include <sstream>

    using namespace std;



Connections :: Connections (Canvas * cnvptr)
    : TcpSrvPtr (new TcpServer (this, cnvptr))
    , WebSrvPtr (new WebServer (this, cnvptr))
{
    connect (TcpSrvPtr, & TcpServer::sigNewConnection,
            this, & Connections::SltNewConnection,
        Qt::QueuedConnection);

    connect (WebSrvPtr, & WebServer::sigNewConnection,
        this, & Connections::SltNewConnection,
        Qt::QueuedConnection);

    QTimer::singleShot (0, this, [this] () { emit sigInfo(); });
}



int Connections :: tcpPort () const
{
    return TcpSrvPtr->port();
}



int Connections :: webPort () const
{
    return WebSrvPtr->port();
}



void Connections :: listen (int tcpPort, int webPort)
{
    if (tcpPort == webPort)
        ++webPort;
    TcpSrvPtr->listen (tcpPort);
    WebSrvPtr->listen (webPort);
    emit sigInfo();
}



string Connections :: info() const
{
    static const string sep = "  |  ";
    ostringstream ostr;

    int tcp= TcpSrvPtr->openConnections();
    int web= WebSrvPtr->openConnections();
    int all = tcp + web;

    ostr
        << LocalIpAddress() << sep
        << TcpSrvPtr->infoPort() << sep
        << WebSrvPtr->infoPort() << sep
        << tcp << " + " << web << " = " << all << " connections";

    return ostr.str();
}



Connections :: ~ Connections ()
{
    Config::get()->setTcpPort (TcpSrvPtr->port());
    Config::get()->setWebPort (WebSrvPtr->port());
    sltCloseSockets();
    delete TcpSrvPtr;
    delete WebSrvPtr;
}



string Connections :: LocalIpAddress ()
{
    QHostAddress local = QHostAddress (QHostAddress::LocalHost);
    QHostAddress addr = local;
    for (const QHostAddress & a: QNetworkInterface::allAddresses())
        if (a != local && a.protocol() == QAbstractSocket::IPv4Protocol)
            addr = a;
    QString qs = addr.toString();
    string s = qs.toStdString();
    return s;
}



void Connections :: SltNewConnection (Socket * sockptr)
{
    SockPtrs.insert (sockptr);
    emit sigRaise();
    emit sigInfo();
}



void Connections :: sltCloseSockets ()
{
    for (Socket * sockptr: SockPtrs)
        delete sockptr;
    SockPtrs.clear();
    emit sigInfo();
}



void Connections :: sltCloseSocket (Socket * sockptr)
{
    SockPtrs.erase (sockptr);
    delete sockptr;
    emit sigInfo();
}

//  self

    #include "server.h"


//  app

    #include "socket.h"


//  c++

    using namespace std;



Server :: Server (Connections * srvptr, Canvas * cnvptr)
    : Port (0)
    , OpenConnections (0)
    , ConnPtr (srvptr)
    , CnvPtr (cnvptr)
{
}



Server :: ~ Server ()
{
}



int Server :: port () const
{
    return Port;
}



void Server :: setPort (int port)
{
    Port = port;
}



string Server :: infoPort () const
{
    return to_string (Port);
}



int Server :: openConnections () const
{
    return OpenConnections;
}



void Server :: SltNewConnection ()
{
    ++OpenConnections;
    Socket * sockptr = MakeConnection();

    connect (sockptr, & Socket::sigClose0,
        this, & Server::SltCloseConnection,
        Qt::QueuedConnection);

    emit sigNewConnection (sockptr);
}



void Server :: SltCloseConnection ()
{
    --OpenConnections;
}

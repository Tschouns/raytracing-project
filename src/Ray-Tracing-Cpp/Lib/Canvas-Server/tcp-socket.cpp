//  self

    #include "tcp-socket.h"


//  qt

    #include <QTcpSocket>



TcpSocket :: TcpSocket (Connections * servptr, QTcpSocket * sockptr, Canvas * cnvptr)
    : Socket (servptr, cnvptr)
    , SockPtr (sockptr)
{
    sockptr->open (QIODevice::ReadWrite);

    connect (SockPtr, & QTcpSocket::readyRead,
            this, & TcpSocket::SltRead,
        Qt::QueuedConnection);

    connect (SockPtr, & QTcpSocket::disconnected,
            this, & TcpSocket::SltClose,
            Qt::QueuedConnection);
}



TcpSocket :: ~ TcpSocket ()
{
    delete SockPtr;
}



void TcpSocket :: SltRead ()
{
    long bufsize = SockPtr->bytesAvailable();
    char * buffer = new char [bufsize + 1];
    SockPtr->read (buffer, bufsize);
    buffer [bufsize] = 0;
    ProcessBuffer (buffer);
    delete [] buffer;
}



void TcpSocket :: SltClose ()
{
    SockPtr->close();
    Close();
}

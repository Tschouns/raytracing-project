//  app

    #include "canvas-client.h"
    #undef main

//  qt

    #include <QCoreApplication>



int main (int argc, char * argv [])
{
    QCoreApplication app (argc, argv);
    CanvasClient cli ("127.0.0.1", 9012);

    QObject::connect (& cli, & CanvasClient::sigFinished,
        & app, & QCoreApplication::quit);

    return app.exec();
}

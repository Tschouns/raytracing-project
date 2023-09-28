//  self

    #include "port-dialog.h"


//  qt designer

    #include "ui_port-dialog.h"



PortDialog :: PortDialog (QWidget * parent)
    : QDialog (parent)
    , UiPtr (new Ui::PortDialog)
{
    UiPtr->setupUi (this);
}



PortDialog :: ~ PortDialog ()
{
    delete UiPtr;
}



int PortDialog :: tcpPort () const
{
    return UiPtr->EdtTcp->text().toInt();
}



void PortDialog :: setTcpPort (int port)
{
    UiPtr->EdtTcp->setText (QString ("%1") .arg (port));
}



int PortDialog :: webPort () const
{
    return UiPtr->EdtWeb->text().toInt();
}



void PortDialog :: setWebPort (int port)
{
    UiPtr->EdtWeb->setText (QString ("%1") .arg (port));
}

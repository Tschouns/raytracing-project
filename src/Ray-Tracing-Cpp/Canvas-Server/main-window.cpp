//  self

    #include "main-window.h"


//  app

    #include "config.h"
    #include "connections.h"
    #include "port-dialog.h"


//  qt creator

    #include "ui_main-window.h"


//  qt

    #include <QFileDialog>


//  c++

    #include <iostream>
    #include <sstream>

    using namespace std;



MainWindow :: MainWindow (QWidget *parent)
    : QMainWindow (parent)
    , UiPtr (new Ui::MainWindow)
    , LblInfoPtr (new QLabel (nullptr))
    , FileDlgPtr (new QFileDialog (nullptr, "Save Image", "", "*.tif"))
    , PortDlgPtr (new PortDialog)
{
    UiPtr->setupUi (this);
    UiPtr->Sts->addPermanentWidget (LblInfoPtr);

    FileDlgPtr->setAcceptMode (QFileDialog::AcceptSave);
    FileDlgPtr->setFileMode (QFileDialog::AnyFile);
    FileDlgPtr->selectFile ("ray-tracing.tif");

    connect (UiPtr->ActFileSave, & QAction::triggered,
        this, & MainWindow::SltSave,
        Qt::QueuedConnection);

    connect (UiPtr->ActConnPorts, & QAction::triggered,
        this, & MainWindow::SltPorts,
        Qt::QueuedConnection);

    connect (FileDlgPtr, & QFileDialog::fileSelected,
        UiPtr->Cnv, & Canvas::sltSave,
        Qt::QueuedConnection);

    connect (UiPtr->Cnv, & Canvas::sigInfo,
        this, & MainWindow::SltInfo,
        Qt::QueuedConnection);

    ConnPtr = new Connections (UiPtr->Cnv);
    ConfigPtr cptr = Config::get();
    ConnPtr->listen (cptr->tcpPort(), cptr->webPort());

    connect (UiPtr->ActConnClose, & QAction::triggered,
        ConnPtr, & Connections::sltCloseSockets,
        Qt::QueuedConnection);

    connect (ConnPtr, & Connections::sigRaise,
            this, & MainWindow::SltRaise,
        Qt::QueuedConnection);

    connect (ConnPtr, & Connections::sigInfo,
        this, & MainWindow::SltInfo,
        Qt::QueuedConnection);
}



MainWindow :: ~ MainWindow ()
{
    delete PortDlgPtr;
    delete FileDlgPtr;
    delete ConnPtr;
    delete UiPtr;
}



void MainWindow :: SltRaise ()
{
    Qt::WindowFlags flags = windowFlags();
    setWindowFlags (flags | Qt::WindowStaysOnTopHint);
    show();
    setWindowFlags (flags);
    show();
}



void MainWindow :: SltSave ()
{
    QDir dir = Config::get()->imgPath();
    FileDlgPtr->setDirectory (dir.exists() ? dir : QDir ("."));

    FileDlgPtr->exec();

    dir = FileDlgPtr->directory();
    QString dirname = dir.canonicalPath();
    Config::get()->setImgPath (dirname);
}



void MainWindow :: SltPorts ()
{
    int otp = ConnPtr->tcpPort();
    int owp = ConnPtr->webPort();

    PortDlgPtr->setTcpPort (otp);
    PortDlgPtr->setWebPort (owp);
    if (PortDlgPtr->exec() == QDialog::Accepted)
    {
        int ntp = PortDlgPtr->tcpPort();
        int nwp = PortDlgPtr->webPort();
        ConnPtr->listen (ntp, nwp);
        ConfigPtr cp = Config::get();
        cp->setTcpPort (ntp);
        cp->setWebPort (nwp);
    }
}



void MainWindow :: SltInfo ()
{
    ostringstream ostr;

    ostr << UiPtr->Cnv->info();
    ostr << "  |  ";
    ostr << ConnPtr->info();

    string str = ostr.str();
    QString qstr = QString::fromStdString (str);
    LblInfoPtr->setText (qstr);
}

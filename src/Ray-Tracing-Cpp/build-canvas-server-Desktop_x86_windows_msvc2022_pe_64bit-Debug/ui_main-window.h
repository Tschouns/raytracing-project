/********************************************************************************
** Form generated from reading UI file 'main-window.ui'
**
** Created by: Qt User Interface Compiler version 6.5.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_MAIN_2D_WINDOW_H
#define UI_MAIN_2D_WINDOW_H

#include <QtCore/QVariant>
#include <QtGui/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QGridLayout>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenu>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QWidget>
#include "canvas.h"

QT_BEGIN_NAMESPACE

class Ui_MainWindow
{
public:
    QAction *ActConnClose;
    QAction *ActFileSave;
    QAction *ActQuit;
    QAction *ActConnPorts;
    QWidget *Cnt;
    QGridLayout *gridLayout;
    Canvas *Cnv;
    QMenuBar *Mnu;
    QMenu *MnuFile;
    QMenu *MnuClose;
    QStatusBar *Sts;

    void setupUi(QMainWindow *MainWindow)
    {
        if (MainWindow->objectName().isEmpty())
            MainWindow->setObjectName("MainWindow");
        MainWindow->resize(800, 600);
        QFont font;
        font.setPointSize(10);
        MainWindow->setFont(font);
        ActConnClose = new QAction(MainWindow);
        ActConnClose->setObjectName("ActConnClose");
        ActFileSave = new QAction(MainWindow);
        ActFileSave->setObjectName("ActFileSave");
        ActQuit = new QAction(MainWindow);
        ActQuit->setObjectName("ActQuit");
        ActConnPorts = new QAction(MainWindow);
        ActConnPorts->setObjectName("ActConnPorts");
        Cnt = new QWidget(MainWindow);
        Cnt->setObjectName("Cnt");
        gridLayout = new QGridLayout(Cnt);
        gridLayout->setObjectName("gridLayout");
        Cnv = new Canvas(Cnt);
        Cnv->setObjectName("Cnv");
        QSizePolicy sizePolicy(QSizePolicy::Ignored, QSizePolicy::Ignored);
        sizePolicy.setHorizontalStretch(0);
        sizePolicy.setVerticalStretch(0);
        sizePolicy.setHeightForWidth(Cnv->sizePolicy().hasHeightForWidth());
        Cnv->setSizePolicy(sizePolicy);
        Cnv->setMinimumSize(QSize(16, 16));
        QFont font1;
        font1.setFamilies({QString::fromUtf8("Consolas")});
        font1.setPointSize(12);
        Cnv->setFont(font1);
        Cnv->setAlignment(Qt::AlignCenter);

        gridLayout->addWidget(Cnv, 0, 0, 1, 2);

        MainWindow->setCentralWidget(Cnt);
        Mnu = new QMenuBar(MainWindow);
        Mnu->setObjectName("Mnu");
        Mnu->setGeometry(QRect(0, 0, 800, 23));
        MnuFile = new QMenu(Mnu);
        MnuFile->setObjectName("MnuFile");
        MnuClose = new QMenu(Mnu);
        MnuClose->setObjectName("MnuClose");
        MainWindow->setMenuBar(Mnu);
        Sts = new QStatusBar(MainWindow);
        Sts->setObjectName("Sts");
        MainWindow->setStatusBar(Sts);

        Mnu->addAction(MnuFile->menuAction());
        Mnu->addAction(MnuClose->menuAction());
        MnuFile->addAction(ActFileSave);
        MnuFile->addSeparator();
        MnuFile->addAction(ActQuit);
        MnuClose->addAction(ActConnPorts);
        MnuClose->addSeparator();
        MnuClose->addAction(ActConnClose);

        retranslateUi(MainWindow);
        QObject::connect(ActQuit, &QAction::triggered, MainWindow, qOverload<>(&QMainWindow::close));

        QMetaObject::connectSlotsByName(MainWindow);
    } // setupUi

    void retranslateUi(QMainWindow *MainWindow)
    {
        MainWindow->setWindowTitle(QCoreApplication::translate("MainWindow", "Canvas Server", nullptr));
        ActConnClose->setText(QCoreApplication::translate("MainWindow", "&Close All", nullptr));
#if QT_CONFIG(tooltip)
        ActConnClose->setToolTip(QCoreApplication::translate("MainWindow", "Close All Connections", nullptr));
#endif // QT_CONFIG(tooltip)
        ActFileSave->setText(QCoreApplication::translate("MainWindow", "&Save Image", nullptr));
        ActQuit->setText(QCoreApplication::translate("MainWindow", "&Quit", nullptr));
#if QT_CONFIG(shortcut)
        ActQuit->setShortcut(QCoreApplication::translate("MainWindow", "Esc", nullptr));
#endif // QT_CONFIG(shortcut)
        ActConnPorts->setText(QCoreApplication::translate("MainWindow", "Change &Ports", nullptr));
#if QT_CONFIG(tooltip)
        ActConnPorts->setToolTip(QCoreApplication::translate("MainWindow", "Change Ports", nullptr));
#endif // QT_CONFIG(tooltip)
        MnuFile->setTitle(QCoreApplication::translate("MainWindow", "&File", nullptr));
        MnuClose->setTitle(QCoreApplication::translate("MainWindow", "&Connections", nullptr));
    } // retranslateUi

};

namespace Ui {
    class MainWindow: public Ui_MainWindow {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MAIN_2D_WINDOW_H

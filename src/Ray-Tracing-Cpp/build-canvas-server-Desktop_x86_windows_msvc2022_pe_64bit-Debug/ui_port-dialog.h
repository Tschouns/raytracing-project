/********************************************************************************
** Form generated from reading UI file 'port-dialog.ui'
**
** Created by: Qt User Interface Compiler version 6.5.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_PORT_2D_DIALOG_H
#define UI_PORT_2D_DIALOG_H

#include <QtCore/QVariant>
#include <QtWidgets/QAbstractButton>
#include <QtWidgets/QApplication>
#include <QtWidgets/QDialog>
#include <QtWidgets/QDialogButtonBox>
#include <QtWidgets/QGridLayout>
#include <QtWidgets/QLabel>
#include <QtWidgets/QLineEdit>
#include <QtWidgets/QSpacerItem>

QT_BEGIN_NAMESPACE

class Ui_PortDialog
{
public:
    QGridLayout *gridLayout;
    QLabel *LblTcp;
    QLineEdit *EdtTcp;
    QLabel *LblWeb;
    QLineEdit *EdtWeb;
    QDialogButtonBox *BtnBox;
    QSpacerItem *Vsp1;
    QSpacerItem *Vsp2;
    QSpacerItem *Hsp1;
    QSpacerItem *Hsp2;

    void setupUi(QDialog *PortDialog)
    {
        if (PortDialog->objectName().isEmpty())
            PortDialog->setObjectName("PortDialog");
        PortDialog->resize(366, 168);
        gridLayout = new QGridLayout(PortDialog);
        gridLayout->setObjectName("gridLayout");
        LblTcp = new QLabel(PortDialog);
        LblTcp->setObjectName("LblTcp");

        gridLayout->addWidget(LblTcp, 1, 1, 1, 1);

        EdtTcp = new QLineEdit(PortDialog);
        EdtTcp->setObjectName("EdtTcp");
        EdtTcp->setAlignment(Qt::AlignCenter);

        gridLayout->addWidget(EdtTcp, 1, 2, 1, 1);

        LblWeb = new QLabel(PortDialog);
        LblWeb->setObjectName("LblWeb");

        gridLayout->addWidget(LblWeb, 2, 1, 1, 1);

        EdtWeb = new QLineEdit(PortDialog);
        EdtWeb->setObjectName("EdtWeb");
        EdtWeb->setAlignment(Qt::AlignCenter);

        gridLayout->addWidget(EdtWeb, 2, 2, 1, 1);

        BtnBox = new QDialogButtonBox(PortDialog);
        BtnBox->setObjectName("BtnBox");
        QSizePolicy sizePolicy(QSizePolicy::Preferred, QSizePolicy::Fixed);
        sizePolicy.setHorizontalStretch(0);
        sizePolicy.setVerticalStretch(0);
        sizePolicy.setHeightForWidth(BtnBox->sizePolicy().hasHeightForWidth());
        BtnBox->setSizePolicy(sizePolicy);
        BtnBox->setOrientation(Qt::Horizontal);
        BtnBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        BtnBox->setCenterButtons(true);

        gridLayout->addWidget(BtnBox, 4, 0, 1, 4);

        Vsp1 = new QSpacerItem(348, 60, QSizePolicy::Minimum, QSizePolicy::Expanding);

        gridLayout->addItem(Vsp1, 0, 0, 1, 4);

        Vsp2 = new QSpacerItem(348, 60, QSizePolicy::Minimum, QSizePolicy::Expanding);

        gridLayout->addItem(Vsp2, 3, 0, 1, 4);

        Hsp1 = new QSpacerItem(82, 51, QSizePolicy::Expanding, QSizePolicy::Minimum);

        gridLayout->addItem(Hsp1, 1, 0, 2, 1);

        Hsp2 = new QSpacerItem(81, 51, QSizePolicy::Expanding, QSizePolicy::Minimum);

        gridLayout->addItem(Hsp2, 1, 3, 2, 1);


        retranslateUi(PortDialog);
        QObject::connect(BtnBox, &QDialogButtonBox::accepted, PortDialog, qOverload<>(&QDialog::accept));
        QObject::connect(BtnBox, &QDialogButtonBox::rejected, PortDialog, qOverload<>(&QDialog::reject));

        QMetaObject::connectSlotsByName(PortDialog);
    } // setupUi

    void retranslateUi(QDialog *PortDialog)
    {
        PortDialog->setWindowTitle(QCoreApplication::translate("PortDialog", "Change Ports", nullptr));
        LblTcp->setText(QCoreApplication::translate("PortDialog", "Tcp Sockets", nullptr));
        EdtTcp->setInputMask(QCoreApplication::translate("PortDialog", "00000", nullptr));
        LblWeb->setText(QCoreApplication::translate("PortDialog", "Web Sockets", nullptr));
        EdtWeb->setInputMask(QCoreApplication::translate("PortDialog", "00000", nullptr));
    } // retranslateUi

};

namespace Ui {
    class PortDialog: public Ui_PortDialog {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_PORT_2D_DIALOG_H

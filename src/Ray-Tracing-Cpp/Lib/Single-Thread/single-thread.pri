INCLUDEPATH += \
    $$PWD

HEADERS += \
    $$PWD/single-thread.h

SOURCES += \
    $$PWD/my-main.cpp

CONFIG += c++17
QT = core gui widgets
DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000

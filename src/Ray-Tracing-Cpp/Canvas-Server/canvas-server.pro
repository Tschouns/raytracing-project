include (../Lib/lib-canvas-server.pri)

FORMS += \
    main-window.ui \
    port-dialog.ui

HEADERS += \
    config.h \
    main-window.h \
    port-dialog.h

SOURCES += \
    config.cpp \
    main-window.cpp \
    main.cpp \
    port-dialog.cpp

CONFIG += c++17
DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000
QT += core gui widgets network websockets

#win32 {
#    DESTDIR = C:/Util/Canvas-Server
#    DEPLOY = $$dirname(QMAKE_QMAKE)/windeployqt
#    EXE = $$DESTDIR/$${TARGET}.exe
#    QMAKE_POST_LINK = $$DEPLOY $$EXE
#}



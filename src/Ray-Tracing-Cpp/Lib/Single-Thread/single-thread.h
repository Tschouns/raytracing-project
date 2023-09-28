#ifndef SINGLE_THREAD_H_INCLUDED
#define SINGLE_THREAD_H_INCLUDED



//  app

    #include "main.h"
    #include "thread.h"



struct SingleThread
    : public Thread
{
    virtual void run () override;
};



#endif

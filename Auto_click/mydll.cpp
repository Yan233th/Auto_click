#define BUILD_MYDLL

#include "pch.h"
#include "mydll.h"

#include <iostream>
#include <windows.h>
#include <ctime>

#define t_1s 1000

bool CheckNum (char text[])
{
    if (text[0] == '.') return false;
    int length = strlen (text), pointSum = 0;
    for (int i = 0; i < length; i++)
    {
        if (text[i] >= '0' && text[i] <= '9') continue;
        if (text[i] == '.')
        {
            pointSum++;
            continue;
        }
        return false;
    }
    if (pointSum > 1) return false;
    return true;
}

bool Click (double keep, double gap, double stay)
{
    Sleep (stay * t_1s);
    if (gap < 0.01) gap = 0.01;
    double limit = time (NULL) + keep;
    while (time (NULL) <= limit)
    {
        mouse_event (MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        Sleep (10);
        mouse_event (MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        Sleep (gap * t_1s);
    }
    return true;
}
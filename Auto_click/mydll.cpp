#define BUILD_MYDLL

#include "pch.h"
#include "mydll.h"

#include <iostream>
#include <windows.h>
#include <ctime>

#define t_1s 995

bool CheckNum (char text[], int mode)
{
    //mode: [1]整数 [2]小数
    switch (mode)
    {
        case 1:
        {
            int length = strlen (text);
            for (int i = 0; i < length; i++)
            {
                if (text[i] >= '0' && text[i] <= '9') continue;
                return false;
            }
            break;
        }
        case 2:
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
            break;
        }
    }
    return true;
}

bool Click_Time (double time_, double gap, double stay, int mode)
{
    Sleep (stay * t_1s);
    if (gap < 0.01) gap = 0.01;
    double limit = time (NULL) + time_;
    switch (mode)
    {
        case 1:
        {
            while (time (NULL) <= limit)
            {
                mouse_event (MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                Sleep (10);
                mouse_event (MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                Sleep (gap * t_1s);
            }
            break;
        }
        case 2:
        {
            while (time (NULL) <= limit)
            {
                mouse_event (MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                Sleep (10);
                mouse_event (MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                Sleep (gap * t_1s);
            }
            break;
        }
    }
    return true;
}

bool Click_Times (int times, double gap, double stay, int mode)
{
    Sleep (stay * t_1s);
    if (gap < 0.01) gap = 0.01;
    switch (mode)
    {
        case 1:
        {
            while (times-- > 0)
            {
                mouse_event (MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                Sleep (10);
                mouse_event (MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                Sleep (gap * t_1s);
            }
            break;
        }
        case 2:
        {
            while (times-- > 0)
            {
                mouse_event (MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                Sleep (10);
                mouse_event (MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                Sleep (gap * t_1s);
            }
            break;
        }
    }
    return true;
}

bool Hold_Time (double time_, double stay, int mode)
{
    Sleep (stay * t_1s);
    if (time_ < 0.01) time_ = 0.01;
    switch (mode)
    {
        case 1:
        {
            mouse_event (MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            Sleep (time_ * t_1s);
            mouse_event (MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            break;
        }
        case 2:
        {
            mouse_event (MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            Sleep (time_ * t_1s);
            mouse_event (MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            break;
        }
    }
    return true;
}
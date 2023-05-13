#pragma once

#ifdef BUILD_MYDLL
#define API_SYMBOL __declspec (dllexport)
#else
#define API_SYMBOL __declspec (dllimport)
#endif

extern "C" API_SYMBOL bool CheckNum (char text[], int mode);
extern "C" API_SYMBOL bool Click_Time (double time_, double gap, double stay, int mode, void (*CallBack) (int status));
extern "C" API_SYMBOL bool Click_Times (int times, double gap, double stay, int mode, void (*CallBack) (int status));
extern "C" API_SYMBOL bool Hold_Time (double time_, double stay, int mode, void (*CallBack) (int status));
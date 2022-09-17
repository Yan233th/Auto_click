#pragma once

#ifdef BUILD_MYDLL
#define API_SYMBOL __declspec (dllexport)
#else
#define API_SYMBOL __declspec (dllimport)
#endif

extern "C" API_SYMBOL bool CheckNum (char text[]);
extern "C" API_SYMBOL bool Click (double keep, double gap, double stay);
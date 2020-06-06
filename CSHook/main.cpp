
#include "stdio.h"
#include "Windows.h"
#include <iostream>
#include <chrono>
#include <thread>

HHOOK hMouseHook;
INPUT ip;
MOUSEHOOKSTRUCT *pMouseStruct;
short oldx,delta;
bool direction;

short CountMouDelta()
{
    if(pMouseStruct != NULL)
    return oldx - pMouseStruct->pt.x;
    return 0;
}

bool DirectionChanged() {
    delta = CountMouDelta();
    bool olddir = direction;
    if (pMouseStruct->pt.x < 960)
    {
        direction = true;
        
    }
    if (pMouseStruct->pt.x > 960)
    {
        direction = false;
    }
    
    return (olddir != direction);
}

LRESULT CALLBACK LowLevelMouseProc(int nCode, WPARAM wParam, LPARAM lParam)
{
    pMouseStruct = (MOUSEHOOKSTRUCT*)lParam;

    DirectionChanged();
    std::cout << pMouseStruct->pt.x << " " << pMouseStruct->pt.y << " " << direction << " " << delta << std::endl;
      oldx = pMouseStruct->pt.x;
    

   
    return CallNextHookEx(hMouseHook, nCode, wParam, lParam);
}



DWORD WINAPI MyMouseLogger(LPVOID lpParm)
{
    hMouseHook = SetWindowsHookEx(WH_MOUSE_LL, LowLevelMouseProc, NULL, 0);
   
    MSG message;
    while (GetMessage(&message, NULL, 0, 0) > 0)
    {
        TranslateMessage(&message);
        DispatchMessage(&message);
    }

    UnhookWindowsHookEx(hMouseHook);
    return 0;
}



int main(int argc, char** argv)
{

  
    ip.type = INPUT_KEYBOARD;
    ip.ki.time = 0;
    ip.ki.wVk = 0; 
    ip.ki.dwExtraInfo = 0;
   

    HANDLE hThread = NULL;
    hThread = CreateThread(NULL, 0, MyMouseLogger, NULL, 0, NULL);
  
    while (true)
    {
       
            if (!direction)
            {
                ip.ki.dwFlags = KEYEVENTF_SCANCODE;
                ip.ki.wScan = 0x20;  
                SendInput(1, &ip, sizeof(INPUT));
            } 
            
            if (direction)
            {
                ip.ki.dwFlags = KEYEVENTF_SCANCODE;
                ip.ki.wScan = 0x1E;
                SendInput(1, &ip, sizeof(INPUT));
                //  ip.ki.dwFlags = KEYEVENTF_SCANCODE | KEYEVENTF_KEYUP;
                //  SendInput(1, &ip, sizeof(INPUT));
            }
       
        std::this_thread::sleep_for(std::chrono::nanoseconds(1000000));
        ip.ki.dwFlags = KEYEVENTF_SCANCODE | KEYEVENTF_KEYUP;
        SendInput(1, &ip, sizeof(INPUT));
       
    }

    if (hThread != NULL)
        return WaitForSingleObject(hThread, INFINITE);
    else
        return 1;
}
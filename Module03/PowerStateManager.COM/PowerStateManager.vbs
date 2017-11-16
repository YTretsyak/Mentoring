Dim powerStateManager
Set powerStateManager = CreateObject("PowerStateManager.COM.PowerStateManagerCOM")

Dim result
result = "Last sleep time: " & powerStateManager.GetLastSleepTime() & vbCrLf
result = result & "Last wake time: " & powerStateManager.GetLastWakeTime() & vbCrLf

MsgBox result
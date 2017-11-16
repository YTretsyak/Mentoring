Dim manager
Set manager = CreateObject("SuspendManager.COM.SuspendManager")

Dim result
result = manager.Sleep()

MsgBox result
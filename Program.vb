Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Threading

Module Program
    ' Declare external functions to interact with the system cursor position
    <DllImport("user32.dll", SetLastError:=True)>
    Public Function GetCursorPos(ByRef lpPoint As Point) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Function SetCursorPos(x As Integer, y As Integer) As Boolean
    End Function



    Sub Main()
        ' Declare variables to hold the cursor position and time of last movement
        Dim currentPos As New Point()
        Dim lastPos As New Point()
        Dim lastMoveTime As DateTime = DateTime.Now

        ' Interval in seconds to check inactivity
        Dim inactivityThreshold As Integer = 60

        ' Main loop
        While True
            ' Get the current position of the cursor
            If GetCursorPos(currentPos) Then
                '-##############################################
                ' Check if the cursor position has changed
                '-##############################################
                If currentPos.X <> lastPos.X OrElse currentPos.Y <> lastPos.Y Then
                    '-##############################################
                    ' Update the last known position and reset the last move time
                    '-##############################################
                    lastPos.X = currentPos.X
                    lastPos.Y = currentPos.Y
                    lastMoveTime = DateTime.Now
                End If
                '-##############################################
                ' Check if the inactivity threshold has been exceeded
                '-##############################################
                If (DateTime.Now - lastMoveTime).TotalSeconds >= inactivityThreshold Then
                    '-##############################################
                    ' Move the cursor by 10 pixels horizontally
                    '-##############################################
                    Dim originalPos As New Point(currentPos.X, currentPos.Y)
                    SetCursorPos(originalPos.X + 10, originalPos.Y)
                    '-##############################################
                    ' Simulate a small pause
                    '-##############################################
                    Thread.Sleep(1000)
                    '-##############################################
                    ' Move the cursor back to the original position
                    '-##############################################
                    SetCursorPos(originalPos.X, originalPos.Y)
                    'Console.WriteLine("Move cursor position.")
                    '-##############################################
                    '-  Reset Timer
                    '-##############################################
                    lastPos.X = currentPos.X
                    lastPos.Y = currentPos.Y
                    lastMoveTime = DateTime.Now

                End If
            Else
                Console.WriteLine("Failed to get the cursor position.")
            End If

            ' Wait briefly before checking again
            Thread.Sleep(500)
        End While
    End Sub



End Module

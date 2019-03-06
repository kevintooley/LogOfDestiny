Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Threading

Public Class TimerForm

    Dim hourTensValue As Double, hourOnesValue As Double, minTensValue As Double, minOnesValue As Double, secValue As Double

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        If secValue > 0 Then

            secValue = secValue - 1

        ElseIf hourTensValue >= 0 And hourOnesValue >= 0 And minTensValue > 0 And minOnesValue > 0 And secValue = 0 Then

            minOnesValue = minOnesValue - 1
            secValue = 59

        ElseIf hourTensValue >= 0 And hourOnesValue >= 0 And minTensValue > 0 And minOnesValue = 0 And secValue = 0 Then

            minTensValue = minTensValue - 1
            minOnesValue = 9
            secValue = 59

        ElseIf hourTensValue >= 0 And hourOnesValue >= 0 And minTensValue = 0 And minOnesValue > 0 And secValue = 0 Then

            minOnesValue = minOnesValue - 1
            secValue = 59

        ElseIf hourTensValue >= 0 And hourOnesValue > 0 And minTensValue = 0 And minOnesValue = 0 And secValue = 0 Then

            hourOnesValue = hourOnesValue - 1
            minTensValue = 5
            minOnesValue = 9
            secValue = 59

        ElseIf hourTensValue > 0 And hourOnesValue = 0 And minTensValue = 0 And minOnesValue = 0 And secValue = 0 Then

            hourTensValue = hourTensValue - 1
            hourOnesValue = 9
            minTensValue = 5
            minOnesValue = 9
            secValue = 59

        Else : Timer1.Stop()

            'If Timer1.Enabled = True Then 'Added

            '    MsgBox("FINEX")

            'End If 'Added

        End If

        Label1.Text = hourTensValue & hourOnesValue
        Label2.Text = minTensValue & minOnesValue
        Label4.Text = secValue

    End Sub

    Private Sub startButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startButton.Click

        Label1.Text = hourTensUpDown.Text & hourOnesUpDown.Text
        hourTensValue = hourTensUpDown.Text
        hourOnesValue = hourOnesUpDown.Text

        Label2.Text = minTensUpDown.Text & minOnesUpDown.Text
        minTensValue = minTensUpDown.Text
        minOnesValue = minOnesUpDown.Text

        Timer1.Start()

    End Sub

    Public Sub startCountdownTimer(ByVal hourTens As Double, ByVal hourOnes As Double, ByVal minTens As Double, ByVal minOnes As Double)

        hourTensValue = hourTens

        hourOnesValue = hourOnes

        minTensValue = minTens

        minOnesValue = minOnes

        'Me.ShowDialog()

        Timer1.Start()

    End Sub

    Public Sub autoTimerStart(ByVal sender As BackgroundWorker)

        Me.Show()

    End Sub

    Private Sub TimerForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

End Class

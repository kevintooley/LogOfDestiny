Imports Microsoft.Office.Core
Imports Microsoft.Office.Interop
Imports System.IO
Imports System.ComponentModel
Imports System.Threading
Imports Microsoft.VisualBasic.Logging
'Imports Microsoft.VisualBasic

Public Class frmLogEntry

    Public exerciseStart As Boolean = False 'Start/Running = true; Break = false
    'Public configType
    Public oXL As Excel.Application
    Public oWB As Excel.Workbook
    Public ws As Excel.Worksheet
    Public manualTimeEntry, OrtsDetect
    Dim hourTens As Double, hourOnes As Double, minTens As Double, minOnes As Double
    Dim autostartTimer As Boolean

    Public elementXmlDataArray(1, 4) ' As String
    Public elementArray() As CElement

    Private Sub cmdEnter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEnter.Click

        'If cgConfig.Checked = False And ddgConfig.Checked = False Then

        '    MsgBox("Please choose a Configuration:  CG or DDG")

        '    Exit Sub

        'End If

        'Try

        '    FormFunctions.submitFormData()

        '    ''clear the data
        '    'ws.Range("A3:AM200").Sort(Key1:=ws.Range("A3"), Order1:=Excel.XlSortOrder.xlAscending, Header:=Excel.XlYesNoGuess.xlGuess, _
        '    '    OrderCustom:=1, MatchCase:=False, Orientation:=Excel.XlSortOrientation.xlSortColumns, _
        '    '    DataOption1:=Excel.XlSortDataOption.xlSortNormal)

        '    'txtLogEntry.Text = ""

        'Catch ex As Exception

        '    MsgBox("You forgot to setup the EXCEL form")

        'End Try

        FormFunctions.submitFormData()

    End Sub

    Private Sub ExcelStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExcelStart.Click

        Try

            'Setup Form based on configuration file
            xmlUtilities.readXmlConfig()

            ' Start Excel and get Application object.
            oXL = CreateObject("Excel.Application")

            oXL.Visible = True

            ' Get a new workbook.
            Dim myDataDirectory = Application.UserAppDataPath

            If configBL9A.Checked = True Or configBL9C.Checked = True Or configBL9D.Checked Then
                ''COMPILE PATHS
                oWB = oXL.Workbooks.Open(myDataDirectory & "\LogOfDestiny_AMOD_Blank.xlsx")
                'oWB = oXL.Workbooks.Open("C:\Program Files\LogOfDestiny\LogOfDestiny_AMOD_blank.xlsx")  ''USE FOR MEIT
                'oWB = oXL.Workbooks.Open("C:\Documents and Settings\ktooley\My Documents\Visual Studio 2010\Projects\LogOfDestiny\LogOfDestiny\LogOfDestiny_AMOD_blank.xlsx")

            ElseIf configF105.Checked = True Then

                oWB = oXL.Workbooks.Open(myDataDirectory & "\LogOfDestiny_F105_Blank.xlsx")
                'oWB = oXL.Workbooks.Open("C:\Program Files\LogOfDestiny\LogOfDestiny_F105_blank.xlsx")  ''USE FOR MEIT
                'oWB = oXL.Workbooks.Open("C:\Documents and Settings\ktooley\My Documents\Visual Studio 2010\Projects\LogOfDestiny\LogOfDestiny\LogOfDestiny_F105_blank.xlsx")

            ElseIf configAWD.Checked = True Then

                oWB = oXL.Workbooks.Open(myDataDirectory & "\LogOfDestiny_AWD_Blank.xlsx")
                'oWB = oXL.Workbooks.Open("C:\Program Files\LogOfDestiny\LogOfDestiny_AWD_blank.xlsx")  ''USE FOR MEIT
                'oWB = oXL.Workbooks.Open("C:\Documents and Settings\ktooley\My Documents\Visual Studio 2010\Projects\LogOfDestiny\LogOfDestiny\LogOfDestiny_AWD_blank.xlsx")

            End If

            ws = oWB.Worksheets("LogData")

        Catch ex As Exception

            MsgBox("A program error has occurred in ExcelStart_Click(): " & ex.Message)

        End Try

    End Sub

    Private Sub ExerciseStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExerciseStatus.Click

        Try

            If exerciseStart = False Then

                exerciseStart = True

                txtLogEntry.Text = "Exercise Start"

                ExerciseStatus.Text = "Break"

                If autostartTimer = True Then

                    TimerForm.startCountdownTimer(hourTens, hourOnes, minTens, minOnes)

                    'BackgroundWorker1.RunWorkerAsync()

                    autostartTimer = False

                End If

            ElseIf exerciseStart = True Then

                exerciseStart = False

                txtLogEntry.Text = "Break"

                ExerciseStatus.Text = "Exercise Resume"

            End If

            FormFunctions.submitFormData()

        Catch ex As Exception

            MsgBox("A program error has occurred in ExerciseStatus_Click(): " & ex.Message)

        End Try

    End Sub

    Private Sub cgConfig_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub ddgConfig_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub elementMetricsButtonClick(ByVal itsShortName As String)

        For Each element As CElement In elementArray
            If element.shortName = itsShortName Then
                element.calculateMetrics()
                Exit For
            End If
        Next

    End Sub

    Private Sub adsMetrics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles adsMetrics.Click

        MsgBox("Click OK to begin calculating.  A message will appear when all calculations are complete.")

        If configF105.Checked = True Then

            elementMetricsButtonClick("SADS")

        Else : elementMetricsButtonClick("ADS")

        End If

        'elementMetricsButtonClick("ADS")

        MsgBox("Calculations complete.")

        ExceptionLogTest("EventLog")

    End Sub


    Private Sub cndMetrics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cndMetrics.Click

        MsgBox("Click OK to begin calculating.  A message will appear when all calculations are complete.")

        If configF105.Checked = True Then

            elementMetricsButtonClick("SCND")

        Else : elementMetricsButtonClick("CND")

        End If

        'elementMetricsButtonClick("CND")

        MsgBox("Calculations complete.")

    End Sub

    Private Sub spyMetrics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles spyMetrics.Click

        MsgBox("Click OK to begin calculating.  A message will appear when all calculations are complete.")

        elementMetricsButtonClick("SPY")

        MsgBox("Calculations complete.")

    End Sub

    Private Sub wcsMetrics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles wcsMetrics.Click

        MsgBox("Click OK to begin calculating.  A message will appear when all calculations are complete.")

        elementMetricsButtonClick("WCS")

        MsgBox("Calculations complete.")

    End Sub

    Private Sub sigproMetrics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sigproMetrics.Click

        MsgBox("Click OK to begin calculating.  A message will appear when all calculations are complete.")

        elementMetricsButtonClick("SIGPRO")

        MsgBox("Calculations complete.")

    End Sub

    Private Sub fclMetrics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fclMetrics.Click

        MsgBox("Click OK to begin calculating.  A message will appear when all calculations are complete.")

        elementMetricsButtonClick("FCL")

        MsgBox("Calculations complete.")

    End Sub

    Private Sub ortsMetrics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ortsMetrics.Click

        MsgBox("Click OK to begin calculating.  A message will appear when all calculations are complete.")

        elementMetricsButtonClick("ORTS")

        MsgBox("Calculations complete.")

    End Sub

    Private Sub actsMetrics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles actsMetrics.Click

        MsgBox("Click OK to begin calculating.  A message will appear when all calculations are complete.")

        elementMetricsButtonClick("ACTS")

        MsgBox("Calculations complete.")

    End Sub

    Private Sub simMetrics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles simMetrics.Click

        MsgBox("Click OK to begin calculating.  A message will appear when all calculations are complete.")

        elementMetricsButtonClick("SIM")

        MsgBox("Calculations complete.")

    End Sub

    Private Sub mpMetrics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mpMetrics.Click

        MsgBox("Click OK to begin calculating.  A message will appear when all calculations are complete.")

        elementMetricsButtonClick("MP")

        MsgBox("Calculations complete.")

    End Sub

    Private Sub bmdMetrics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bmdMetrics.Click

        MsgBox("Click OK to begin calculating.  A message will appear when all calculations are complete.")

        elementMetricsButtonClick("BMD")

        MsgBox("Calculations complete.")

    End Sub

    Private Sub awsMetrics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles awsMetrics.Click

        MsgBox("Click OK to begin calculating.  A message will appear when all calculations are complete.")

        If configF105.Checked = True Then

            elementMetricsButtonClick("SADS")

        Else : elementMetricsButtonClick("ADS")

        End If

        'elementMetricsButtonClick("ADS")

        If configF105.Checked = True Then

            elementMetricsButtonClick("SCND")

        Else : elementMetricsButtonClick("CND")

        End If

        'elementMetricsButtonClick("CND")

        elementMetricsButtonClick("SPY")

        elementMetricsButtonClick("WCS")

        elementMetricsButtonClick("ORTS")

        elementMetricsButtonClick("ACTS")

        elementMetricsButtonClick("SIM")

        elementMetricsButtonClick("MP")

        elementMetricsButtonClick("SIGPRO")

        elementMetricsButtonClick("FCL")

        elementMetricsButtonClick("BMD")

        elementMetricsButtonClick("AWS")

        elementMetricsButtonClick("ADSC")

        MsgBox("Calculations complete.")

    End Sub

    Private Sub acsMetrics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles acsMetrics.Click

        MsgBox("Click OK to begin calculating.  A message will appear when all calculations are complete.")

        If configF105.Checked = True Then

            For Each element As CElement In elementArray
                'If element.shortName = itsShortName Then
                element.calculateMetrics()
                'Exit For
                'End If
            Next

            'End If
        Else

            elementMetricsButtonClick("SM2")

            elementMetricsButtonClick("SM3")

            elementMetricsButtonClick("SM6")

            elementMetricsButtonClick("ESSM")

            elementMetricsButtonClick("GUN")

            elementMetricsButtonClick("PWS")

            elementMetricsButtonClick("HARPOON")

            elementMetricsButtonClick("VLS")

            elementMetricsButtonClick("VLA")

            elementMetricsButtonClick("OTST")

            elementMetricsButtonClick("UWS")

            elementMetricsButtonClick("AC")

            elementMetricsButtonClick("SPS67")

            elementMetricsButtonClick("SPQ9B")

            elementMetricsButtonClick("LAMPS")

            elementMetricsButtonClick("CEP")

            elementMetricsButtonClick("LINK")

            elementMetricsButtonClick("IFF")

            elementMetricsButtonClick("EWS")

            elementMetricsButtonClick("NAV")

            elementMetricsButtonClick("ILL1")

            elementMetricsButtonClick("ILL2")

            elementMetricsButtonClick("ILL3")

            elementMetricsButtonClick("ILL4")

        End If

        MsgBox("Calculations complete.")

    End Sub

    Private Sub OpenExcelFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenExcelFile.Click

        Try

            xmlUtilities.readXmlConfig()

        Catch Ex As Exception

            MessageBox.Show("Exception detected while setting up the form: " & Ex.Message)

        End Try

        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = "C:\"
        openFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            Try

                myStream = openFileDialog1.OpenFile()

                If (myStream IsNot Nothing) Then

                    ' Insert code to read the stream here.

                    ' Start Excel and get Application object.
                    oXL = CreateObject("Excel.Application")

                    oXL.Visible = True

                    ' Get workbook.
                    'oWB = oXL.Workbooks.Open(openFileDialog1.FileName, [ReadOnly]:=True)
                    oWB = oXL.Workbooks.Open(openFileDialog1.FileName)

                    ws = oWB.Worksheets("LogData")

                End If

            Catch Ex As Exception

                MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)

            Finally

                ' Check this again, since we need to make sure we didn't throw an exception on open.
                If (myStream IsNot Nothing) Then

                    myStream.Close()

                End If

            End Try

        End If

    End Sub

    Private Sub OrtsDetectYes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrtsDetectYes.Click

        Try

            For Each findElement As CElement In elementArray

                If findElement.shortName = "ORTS Detect" Then

                    findElement.setStatus("ORTS Detect", "Y")

                    If Me.txtLogEntry.Text = "" Then
                        Me.txtLogEntry.Text = "ORTS detected the fault"
                    Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + " ORTS detected the fault"
                    End If

                    'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

                    Exit For

                End If

            Next

        Catch ex As Exception

            MsgBox("A program error has occurred in OrtsDetectYes: " & ex.Message)

        End Try

    End Sub

    Private Sub OrtsDetectNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrtsDetectNo.Click

        Try

            For Each findElement As CElement In elementArray

                If findElement.shortName = "ORTS Detect" Then

                    findElement.setStatus("ORTS Detect", "N")

                    If Me.txtLogEntry.Text = "" Then
                        Me.txtLogEntry.Text = "ORTS did not detect the fault"
                    Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + " ORTS did not detect the fault"
                    End If

                    'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

                    Exit For

                End If

            Next

        Catch ex As Exception

            MsgBox("A program error has occurred in OrtsDetectNo: " & ex.Message)

        End Try

    End Sub

    Private Sub frmLogEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Text = "Log Of Destiny v" & My.Application.Info.Version.ToString

    End Sub

    Private Sub hourTensUpDown_SelectedItemChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles hourTensUpDown.SelectedItemChanged

        hourTens = hourTensUpDown.Text

    End Sub

    Private Sub hourTensUpDown_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles hourTensUpDown.KeyPress

        Dim allowedChars As String = "0123456789"

        If allowedChars.IndexOf(e.KeyChar) = -1 Then

            e.Handled = True ' Invalid Character

        End If

        If e.KeyChar = Chr(8) Then

            e.Handled = False 'allow Backspace

        End If

    End Sub

    Private Sub hourOnesUpDown_SelectedItemChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles hourOnesUpDown.SelectedItemChanged

        hourOnes = hourOnesUpDown.Text

    End Sub

    Private Sub hourOnesUpDown_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles hourOnesUpDown.KeyPress

        Dim allowedChars As String = "0123456789"

        If allowedChars.IndexOf(e.KeyChar) = -1 Then

            e.Handled = True ' Invalid Character

        End If

        If e.KeyChar = Chr(8) Then

            e.Handled = False 'allow Backspace

        End If

    End Sub

    Private Sub minTensUpDown_SelectedItemChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles minTensUpDown.SelectedItemChanged

        minTens = minTensUpDown.Text

    End Sub

    Private Sub minTensUpDown_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles minTensUpDown.KeyPress

        Dim allowedChars As String = "0123456789"

        If allowedChars.IndexOf(e.KeyChar) = -1 Then

            e.Handled = True ' Invalid Character

        End If

        If e.KeyChar = Chr(8) Then

            e.Handled = False 'allow Backspace

        End If

    End Sub

    Private Sub minOnesUpDown_SelectedItemChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles minOnesUpDown.SelectedItemChanged

        minOnes = minOnesUpDown.Text

    End Sub

    Private Sub minOnesUpDown_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles minOnesUpDown.KeyPress

        Dim allowedChars As String = "0123456789"

        If allowedChars.IndexOf(e.KeyChar) = -1 Then

            e.Handled = True ' Invalid Character

        End If

        If e.KeyChar = Chr(8) Then

            e.Handled = False 'allow Backspace

        End If

    End Sub

    Private Sub timerStartCheckbox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerStartCheckbox.CheckedChanged

        autostartTimer = True

    End Sub

    Private Sub timerStartButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerStartButton.Click

        Try

            'TimerForm.Show()

            ' Load the assembly into the current appdomain:
            Dim newAssembly As System.Reflection.Assembly = System.Reflection.Assembly.LoadFrom("C:\Documents and Settings\ktooley\My Documents\Visual Studio 2010\Publish\GenericTimer_v1.0.0.1\Application Files\GenericTimer_1_0_0_1\GenericTimer.exe")

            ' Instantiate RemoteObject:
            newAssembly.CreateInstance("GenericTimer.RemoteObject")

            'Dim o As GenericTimer.ReboteObject = New GenericTimer.TimerForm()






        Catch ex As Exception

            MsgBox("A program error has occurred in timerStartButton_Click(): " & ex.Message)

        End Try

    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        'Dim testTimer As New TimerForm

        '' Do not access the form's BackgroundWorker reference directly.
        '' Instead, use the reference provided by the sender parameter.
        'Dim bw As BackgroundWorker = CType(sender, BackgroundWorker)

        ' Extract the argument.
        'Dim arg = Fix(e.Argument)


        ' Start the time-consuming operation.
        'Thread.Sleep(arg)

        '' If the operation was canceled by the user, 
        '' set the DoWorkEventArgs.Cancel property to true.
        'If bw.CancellationPending Then
        '    e.Cancel = True
        'End If

        'Thread.Sleep(10000)
        'MsgBox("Backgroundworker COMPLETE")

        'Dim bw As New BackgroundWorker
        'AdsElement.calculateMetrics()

        'CndElement.calculateMetrics()

        'SpyElement.calculateMetrics()

        'WcsElement.calculateMetrics()

        'OrtsElement.calculateMetrics()

        'ActsElement.calculateMetrics()

        'SimElement.calculateMetrics()

        'MpElement.calculateMetrics()

        'SigproElement.calculateMetrics()

        'FclElement.calculateMetrics()

        'BmdElement.calculateMetrics()

        TimerForm.ShowDialog()

        'TimerForm.startCountdownTimer(hourTens, hourOnes, minTens, minOnes)

    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged



    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        'If e.Cancelled Then
        '    ' The user canceled the operation.
        '    MessageBox.Show("Operation was canceled")
        'ElseIf (e.Error IsNot Nothing) Then
        '    ' There was an error during the operation.
        '    Dim msg As String = String.Format("An error occurred: {0}", e.Error.Message)
        '    MessageBox.Show(msg)
        '    'Else
        '    '    ' The operation completed normally.
        '    '    Dim msg As String = String.Format("Result = {0}", e.Result)
        '    '    MessageBox.Show(msg)
        'End If

        'Dim testTimer As New TimerForm(e.Result)
        'testTimer.Show()

        MsgBox("TEST")

    End Sub

    Private Sub manualTimeEntryBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles manualTimeEntryBox.KeyPress

        Dim allowedChars As String = "0123456789:"

        If allowedChars.IndexOf(e.KeyChar) = -1 Then

            e.Handled = True ' Invalid Character

        End If

        If e.KeyChar = Chr(8) Then

            e.Handled = False 'allow Backspace

        End If

    End Sub

    Public Sub ExceptionLogTest(ByVal fileName As String)

        'Try

        '    ' Code that might generate an exception goes here.
        '    ' For example:
        '    Dim x As Object
        '    MsgBox(x.ToString)

        'Catch ex As Exception

        '    My.Application.Log.WriteException(ex, TraceEventType.Error, "Exception in ExceptionLogTest " _
        '                                      & "with argument " & fileName & ".")


        '    'Dim writer As New StreamWriter("C:\Program Files\LogOfDestiny\Logs\MyLog.log", True, System.Text.Encoding.ASCII)
        '    'writer.WriteLine(ex.Message)
        '    'writer.Close()

        'End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        xmlUtilities.readXmlConfig()

    End Sub
    '//UP BUTTONS
    'Private Sub Rb1Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rb1Up.Click

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb1Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb1Up_Click(): " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb2Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb2Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb2Up_Click(): " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb3Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb3Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb3Up_Click(): " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb4Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb4Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb4Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb5Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb5Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb5Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb6Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb6Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb6Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb7Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb7Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb7Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb8Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb8Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb8Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb9Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb9Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb9Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb10Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb10Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb10Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb11Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb11Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb11Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb12Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb12Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb12Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb13Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb13Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb13Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb14Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb14Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb14Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb15Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb15Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb15Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb16Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb16Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb16Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb17Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb17Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb17Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb18Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb18Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb18Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb19Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb19Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb19Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb20Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb20Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb20Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb21Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb21Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb21Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb22Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb22Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb22Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb23Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb23Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb23Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb24Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb24Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb24Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb25Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb25Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb25Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb26Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb26Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb26Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb27Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb27Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb27Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb28Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb28Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb28Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb29Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb29Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb29Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb30Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb30Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb30Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb31Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb31Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb31Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb32Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb32Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb32Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb33Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb33Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb33Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb34Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb34Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb34Up: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb35Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.upButton = Rb35Up.Name Then

    '                findElement.setStatus(findElement.shortName, "U")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is UP"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is UP"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb35Up: " & ex.Message)

    '    End Try

    'End Sub
    ''//END UP BUTTONS

    ''//DG BUTTONS

    'Private Sub Rb1Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rb1Dg.Click

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb1Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb1Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb2Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb2Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb2Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb3Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb3Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb3Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb4Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb4Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb4Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb5Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb5Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb5Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb6Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb6Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb6Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb7Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb7Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb7Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb8Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb8Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb8Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb9Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb9Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb9Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb10Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb10Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb10Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb11Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb11Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb11Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb12Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb12Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb12Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb13Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb13Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb13Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb14Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb14Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb14Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb15Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb15Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb15Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb16Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb16Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb16Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb17Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb17Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb17Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb18Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb18Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb18Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb19Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb19Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb19Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb20Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb20Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb20Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb21Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb21Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb21Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb22Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb22Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb22Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb23Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb23Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb23Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb24Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb24Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb24Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb25Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb25Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb25Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb26Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb26Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb26Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb27Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb27Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb27Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb28Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb28Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb28Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb29Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb29Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb29Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb30Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb30Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb30Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb31Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb31Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb31Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb32Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb32Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb32Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb33Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb33Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb33Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb34Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb34Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb34Dg: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb35Dg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dgButton = Rb35Dg.Name Then

    '                findElement.setStatus(findElement.shortName, "DG")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DGRD"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DGRD"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb35Dg: " & ex.Message)

    '    End Try

    'End Sub
    ''//END DG BUTTON

    ''//DN BUTTON

    'Private Sub Rb1Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rb1Dn.Click

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb1Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb1Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb2Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb2Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb2Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb3Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb3Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb3Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb4Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb4Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb4Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb5Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb5Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb5Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb6Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb6Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb6Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb7Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb7Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb7Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb8Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb8Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb8Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb9Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb9Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb9Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb10Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb10Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb10Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb11Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb11Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb11Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb12Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb12Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb12Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb13Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb13Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb13Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb14Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb14Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb14Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb15Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb15Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb15Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb16Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb16Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb16Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb17Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb17Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb17Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb18Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb18Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb18Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb19Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb19Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb19Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb20Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb20Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb20Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb21Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb21Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb21Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb22Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb22Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb22Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb23Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb23Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb23Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb24Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb24Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb24Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb25Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb25Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb25Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb26Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb26Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb26Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb27Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb27Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb27Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb28Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb28Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb28Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb29Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb29Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb29Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb30Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb30Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb30Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb31Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb31Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb31Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb32Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb32Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb32Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb33Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb33Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb33Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb34Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb34Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb34Dn: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb35Dn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.dnButton = Rb35Dn.Name Then

    '                findElement.setStatus(findElement.shortName, "D")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is DOWN"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is DOWN"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb35Dn: " & ex.Message)

    '    End Try

    'End Sub
    ''//END DN BUTTON

    ''//NA BUTTON

    'Private Sub Rb1Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rb1Na.Click

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb1Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb1Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb2Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb2Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb2Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb3Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb3Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb3Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb4Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb4Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb4Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb5Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb5Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb5Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb6Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb6Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb6Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb7Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb7Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb7Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb8Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb8Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb8Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb9Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb9Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb9Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb10Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb10Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb10Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb11Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb11Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb11Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb12Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb12Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb12Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb13Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb13Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb13Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb14Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb14Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb14Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb15Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb15Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb15Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb16Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb16Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb16Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb17Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb17Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb17Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb18Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb18Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb18Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb19Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb19Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb19Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb20Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb20Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb20Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb21Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb21Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb21Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb22Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb22Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb22Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb23Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb23Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb23Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb24Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb24Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb24Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb25Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb25Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb25Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb26Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb26Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb26Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb27Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb27Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb27Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb28Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb28Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb28Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb29Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb29Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb29Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb30Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb30Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb30Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb31Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb31Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb31Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb32Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb32Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb32Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb33Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb33Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb33Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb34Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb34Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb34Na: " & ex.Message)

    '    End Try

    'End Sub

    'Private Sub Rb35Na_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        For Each findElement As CElement In elementArray

    '            If findElement.naButton = Rb35Na.Name Then

    '                findElement.setStatus(findElement.shortName, "O")

    '                If Me.txtLogEntry.Text = "" Then
    '                    Me.txtLogEntry.Text = findElement.shortName + " is NA"
    '                Else : Me.txtLogEntry.Text = Me.txtLogEntry.Text + ";" + findElement.shortName + " is NA"
    '                End If

    '                'Debug.Print(findElement.elementName + " is " + findElement.getStatus)

    '                Exit For

    '            End If

    '        Next

    '    Catch ex As Exception

    '        MsgBox("A program error has occurred in Rb35Na: " & ex.Message)

    '    End Try

    'End Sub

    Private Sub clearDataButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clearDataButton.Click

        oWB.Application.ScreenUpdating = False

        For Each ws1 As Excel.Worksheet In oWB.Worksheets

            'Debug.Print(ws1.Name)
            ws1.Select()

            If ws1.Name = "AWS STABILITY" Or ws1.Name = "ACS" Then

                'Debug.Print("We aren't going to do anything to this sheet: " + ws1.Name)


            Else 'Debug.Print("Deleting stuff in: " + ws1.Name)

                'ws1.Range("A3", "AQ200").ClearContents()
                ws1.Range("A3:AQ200").ClearContents()
                ws1.Range("A3:A3").Select()

            End If

        Next

        oWB.Application.ScreenUpdating = True
        ws = oWB.Worksheets("LogData")
        ws.Activate()

    End Sub
End Class
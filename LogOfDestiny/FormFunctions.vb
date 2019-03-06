Imports Microsoft.Office.Interop
Imports Microsoft.Office.Core
Imports Microsoft.Office.Interop.Excel

Module FormFunctions

    Dim iRow As Long

    Dim SVari

    Dim tempTime As DateTime

    Sub submitFormData()

        Try

            frmLogEntry.ws.Activate()

            'find first empty row in database
            iRow = frmLogEntry.ws.Range("A1", "A2") _
                .End(Excel.XlDirection.xlDown).Offset(1, 0).Row

            'Enter text and Break status
            'frmLogEntry.ws.Cells(iRow, 1).NumberFormat = "h:mm:ss;@"
            frmLogEntry.ws.Cells(iRow, 1).NumberFormat = "m/d/yy h:mm:ss;@"
            'm/d/yy h:mm;@

            If frmLogEntry.exerciseStart = True Then

                exerciseRun()

            Else : exerciseBreak()

            End If

            'clear the data
            frmLogEntry.ws.Range("A3:AM200").Sort(Key1:=frmLogEntry.ws.Range("A3"), Order1:=Excel.XlSortOrder.xlAscending, Header:=Excel.XlYesNoGuess.xlGuess, _
                OrderCustom:=1, MatchCase:=False, Orientation:=Excel.XlSortOrientation.xlSortColumns, _
                DataOption1:=Excel.XlSortDataOption.xlSortNormal)

            frmLogEntry.txtLogEntry.Text = ""

        Catch ex As Exception

            MsgBox("You forgot to SETUP the EXCEL Spreadsheet")

        End Try

        ''COMMENT IN the entire section below to remove exception handling
        'frmLogEntry.ws.Activate()

        ''find first empty row in database
        'iRow = frmLogEntry.ws.Range("A1", "A2") _
        '    .End(Excel.XlDirection.xlDown).Offset(1, 0).Row

        ''Enter text and Break status
        'frmLogEntry.ws.Cells(iRow, 1).NumberFormat = "h:mm:ss;@"

        'If frmLogEntry.exerciseStart = True Then

        '    exerciseRun()

        'Else : exerciseBreak()

        'End If

        ''clear the data
        'frmLogEntry.ws.Range("A3:AM200").Sort(Key1:=frmLogEntry.ws.Range("A3"), Order1:=Excel.XlSortOrder.xlAscending, Header:=Excel.XlYesNoGuess.xlGuess, _
        '    OrderCustom:=1, MatchCase:=False, Orientation:=Excel.XlSortOrientation.xlSortColumns, _
        '    DataOption1:=Excel.XlSortDataOption.xlSortNormal)

        'frmLogEntry.txtLogEntry.Text = ""

        'For i = 3 To iRow

        '    If frmLogEntry.ws.Cells(i, 1).Value > 0 Then

        '        MsgBox("Cell A" & i & " is not formated correctly.")
        '        'Debug.Print(frmLogEntry.ws.Cells(i, 1).Value - frmLogEntry.ws.Cells(i - 1, 1).Value)
        '        Debug.Print(frmLogEntry.ws.Cells(i, 1).NumberFormat.ToString)

        '    End If

        'Next

    End Sub

    Sub checkDST(ByVal manualTime)

        Try

            'If manualTime entry is present
            If manualTime Then

                'Conduct RollOver Test
                Dim rollOverTest
                Dim rollOverOccurred As Boolean
                'Dim tempTime As DateTime = DateAndTime.DateString & "  " & frmLogEntry.manualTimeEntryBox.Text

                rollOverTest = DateAndTime.DateString & "  " & frmLogEntry.manualTimeEntryBox.Text

                If rollOverTest < Date.Now Then

                    rollOverOccurred = True

                    tempTime = Date.UtcNow.Date & "  " & frmLogEntry.manualTimeEntryBox.Text

                Else : tempTime = DateAndTime.DateString & "  " & frmLogEntry.manualTimeEntryBox.Text

                End If

                If frmLogEntry.DstYes.Checked = True Then

                    'COMPILE Changes
                    frmLogEntry.ws.Cells(iRow, 1).Value = tempTime - TimeSpan.FromHours(4)
                    'frmLogEntry.ws.Cells(iRow, 1).Value = tempTime - TimeSpan.FromHours(8)

                    'if clock rollover occured
                    If rollOverOccurred = True Then

                        frmLogEntry.ws.Cells(iRow, 2).Value = Date.UtcNow.Date & "  " & frmLogEntry.manualTimeEntryBox.Text

                        'Else - clock rollover did not occure
                    Else : frmLogEntry.ws.Cells(iRow, 2).Value = DateAndTime.DateString & "  " & frmLogEntry.manualTimeEntryBox.Text

                    End If

                ElseIf frmLogEntry.DstYes.Checked = False Then

                    'COMPILE Changes
                    frmLogEntry.ws.Cells(iRow, 1).Value = tempTime - TimeSpan.FromHours(5)
                    'frmLogEntry.ws.Cells(iRow, 1).Value = tempTime - TimeSpan.FromHours(8)

                    'if clock rollover occured
                    If rollOverOccurred = True Then

                        frmLogEntry.ws.Cells(iRow, 2).Value = Date.UtcNow.Date & "  " & frmLogEntry.manualTimeEntryBox.Text

                        'Else - clock rollover did not occure
                    Else : frmLogEntry.ws.Cells(iRow, 2).Value = DateAndTime.DateString & "  " & frmLogEntry.manualTimeEntryBox.Text

                    End If

                End If

            Else

                frmLogEntry.ws.Cells(iRow, 2).Value = Date.UtcNow

            End If

        Catch ex As Exception

            MsgBox("A program error has occurred in checkDST: " & ex.Message)

        End Try

    End Sub

    Sub enterTime()

        Try

            frmLogEntry.ws.Cells(iRow, 1).NumberFormat = "h:mm:ss;@"

            If frmLogEntry.manualTimeEntryBox.Text = "" Then

                frmLogEntry.ws.Cells(iRow, 1).Value = Now

                checkDST(False)

            Else : checkDST(True)

                frmLogEntry.manualTimeEntryBox.Text = ""

            End If

        Catch ex As Exception

            MsgBox("A program error has occurred in enterTime: " & ex.Message)

        End Try

    End Sub

    Sub exerciseBreak()

        Try

            enterTime()

            frmLogEntry.ws.Cells(iRow, 3).Value = frmLogEntry.txtLogEntry.Text

            'For i = 5 To 13

            '    frmLogEntry.ws.Cells(iRow, i).Value = "B"

            'Next

            'For i = 16 To 41

            '    frmLogEntry.ws.Cells(iRow, i).Value = "B"

            'Next

            For Each element As CElement In frmLogEntry.elementArray

                If element.shortName = "ORTS Detect" Then

                Else : frmLogEntry.ws.Cells(iRow, element.VCell) = "B"
                End If

            Next

        Catch ex As Exception

            MsgBox("A program error has occurred in exerciseBreak: " & ex.Message)

        End Try

    End Sub

    Sub exerciseRun()

        Try

            enterTime()

            frmLogEntry.ws.Cells(iRow, 3).Value = frmLogEntry.txtLogEntry.Text

            Config.dependents()

            For Each element As CElement In frmLogEntry.elementArray
                frmLogEntry.ws.Cells(iRow, element.VCell) = element.getStatus()
                'Debug.Print(element.shortName + " is " + element.getStatus())
            Next

            'ORTS Detection
            For Each ortsElement As CElement In frmLogEntry.elementArray
                If ortsElement.shortName = "ORTS Detect" Then
                    ortsElement.setStatus("ORTS Detect", "")
                    Exit For
                End If
            Next

            frmLogEntry.OrtsDetectYes.Checked = False
            frmLogEntry.OrtsDetectNo.Checked = False

        Catch ex As Exception

            MsgBox("A program error has occurred in exerciseRun: " & ex.Message)

        End Try

    End Sub

End Module
